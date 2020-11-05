using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TradingBlockApiTestHarness
{
    public class Encryption
    {
        private static string generateIV()
        {
            DateTime currUTC = DateTime.UtcNow;
            string IV = "TBAPIENT" + currUTC.ToString("yyyyMMdd");

            if (IV.Length == 16)
                return IV;
            else
            {
                throw new CryptographicException("Error generating IV");
            }
        }
        private static MemoryStream ToStream(string str)
        {
            MemoryStream stream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(str);
                writer.Flush();
                stream.Position = 0;
            }

            return stream;
        }
        public static MemoryStream Decrypt(string password, Stream input)
        {

            // Check arguments.  
            if (input == null || input.Length <= 0)
            {
                throw new ArgumentNullException("Text size error.");
            }
            if (password.Length != 32)
            {
                throw new ArgumentNullException("Key size error.");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                var key = Encoding.UTF8.GetBytes(password);
                rijAlg.Key = key;
                var iv = Encoding.UTF8.GetBytes(generateIV());
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                MemoryStream output;
                try
                {
                    // Create the streams used for decryption.  

                    using (input)
                    {
                        using (var csDecrypt = new CryptoStream(input, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                                output = ToStream(plaintext);
                                return output;
                            }

                        }
                    }

                }
                catch
                {
                    plaintext = "keyError";
                    output = ToStream(plaintext);
                    return output;
                }
            }
        }

        public static MemoryStream Encrypt(string password, Stream input)
        {
            // Check arguments.  
            if (input == null || input.Length <= 0)
            {
                throw new ArgumentNullException("Text size error.");
            }
            if (password.Length != 32)
            {
                throw new ArgumentNullException("Text size error.");
            }

            byte[] encrypted;
            // Create a RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = Encoding.UTF8.GetBytes(password);

                var iv = Encoding.UTF8.GetBytes(generateIV());
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                StreamReader reader = new StreamReader(input);
                string text = reader.ReadToEnd();

                // Create the streams used for encryption.  
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.  
                            swEncrypt.Write(text);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            MemoryStream output = new MemoryStream(encrypted);
            return output;
        }
    }
}
