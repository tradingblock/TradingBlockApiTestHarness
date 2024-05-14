using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Newtonsoft.Json;

using TradingBlockApiTestHarness.DTO.Auth;

namespace TradingBlockApiTestHarness
{
    internal class TokenManager
    {
        /// <summary>
        /// API key given to you by TradingBlock
        /// </summary>
        private static readonly string _apiKey = ConfigurationManager.AppSettings["Apikey"];
        /// <summary>
        /// Entity, or bearer, given to you by TradingBlock. This often will be your company name.
        /// </summary>
        private static readonly string _entity = ConfigurationManager.AppSettings["Bearer"];

        /// <summary>
        /// Username of the actual end user/client that is attempting to authenticate via your application to TradingBlock's API
        /// </summary>
        private static readonly string _userName = ConfigurationManager.AppSettings["EndUser_Username"];
        /// <summary>
        /// Password of the actual end user/client that is attempting to authenticate via your application to TradingBlock's API
        /// </summary>
        private static readonly string _password = ConfigurationManager.AppSettings["EndUser_Password"];
        /// <summary>
        /// Encryption algorithm to use. Currently supported: "HS256", "HS384", "HS512"
        /// </summary>
        private static readonly string _alg = ConfigurationManager.AppSettings["alg"];

        public TokenRequest CreateTokenRequest()
        {
            string header = GenerateHeader();
            string payload = GeneratePayload();
            string signature = GenerateSignature(header, payload);

            TokenRequest request = new TokenRequest(string.Join(".", header, payload, signature));

            Console.WriteLine("TokenRequest object created with JWS: " + request.JWS);

            return request;
        }

        private string GenerateHeader()
        {
            Header header = new Header() { typ = "JWT", alg = _alg }; //256 or 512
            string str = JsonConvert.SerializeObject(header);

            byte[] byteHeader = Encoding.UTF8.GetBytes(str);

            string retStr = Base64UrlEncode(byteHeader);

            return retStr;
        }

        private static string GeneratePayload()
        {
            //This will be the actual end user/client that is attempting to authenticate via your application to TradingBlock's API
            EndUserInfo endUser = new EndUserInfo { username = _userName, password = _password };

            string jsonEndUser = JsonConvert.SerializeObject(endUser);

            string strEncrypted = AESEncrypt(jsonEndUser);

            Payload payload = new Payload { EndUser = strEncrypted, Entity = _entity, Timestamp = DateTime.Now };
            string str = JsonConvert.SerializeObject(payload);

            byte[] bytePayload = Encoding.UTF8.GetBytes(str);

            return Base64UrlEncode(bytePayload);
        }

        private static string GenerateSignature(string header, string payload)
        {
            string toBeSigned = $"{header}.{payload}";

            return HashEncrypt(toBeSigned);
        }

        private static string Base64UrlEncode(byte[] base64EncodedBytes)
        {
            string temp = Convert.ToBase64String(base64EncodedBytes);
            StringBuilder sb = new StringBuilder(temp.Length);
            for (int i = 0; i < temp.Length; ++i)
            {
                char ch = temp[i];
                switch (ch)
                {
                    case '=':
                        break;
                    case '/':
                        sb.Append('_');
                        break;
                    case '+':
                        sb.Append('-');
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        private static string HashEncrypt(string strToEncrypt)
        {
            byte[] byteToEncrypt = Encoding.UTF8.GetBytes(strToEncrypt);
            byte[] encryptionKey = Encoding.UTF8.GetBytes(_apiKey);
            byte[] byteEncrypted;
            using (HashAlgorithm algo = GetAlgorithm(encryptionKey))
            {
                byteEncrypted = algo.ComputeHash(byteToEncrypt);
            }
            string strEncrypted = Base64UrlEncode(byteEncrypted);

            return strEncrypted;
        }

        private static HashAlgorithm GetAlgorithm(byte[] encryptionKey)
        {
            switch (_alg)
            {
                case "HS256":
                    return new HMACSHA256(encryptionKey);
                case "HS384":
                    return new HMACSHA384(encryptionKey);
                case "HS512":
                    return new HMACSHA512(encryptionKey);
                default:
                    throw new NotImplementedException();
            }
        }

        private static string AESEncrypt(string strToEncrypt)
        {
            byte[] byteToEncrypt = Encoding.UTF8.GetBytes(strToEncrypt);
            byte[] byteEncrypted;
            using (MemoryStream plainText = new MemoryStream(byteToEncrypt))
            {
                using (MemoryStream encryptedData = Encryption.Encrypt(_apiKey, plainText))
                {
                    byteEncrypted = encryptedData.ToArray();
                }
            }
            string strEncrypted = Base64UrlEncode(byteEncrypted);

            return strEncrypted;
        }

        private class Header
        {
            public string typ;
            public string alg;
        }

        private class Payload
        {
            /// <summary>
            /// Entity/bearer name provided to you by TradingBlock
            /// </summary>
            public string Entity;
            /// <summary>
            /// Encrypted JSON object (EndUserInfo) containing client's username and password
            /// </summary>
            public string EndUser;
            /// <summary>
            /// Current time. This is used because TokenRequests are only considered valid for 15 seconds from creation. This is prevent replay attacks.
            /// </summary>
            public DateTime Timestamp;
        }

        private class EndUserInfo
        {
            public string username;
            public string password;
        }
    }
}
