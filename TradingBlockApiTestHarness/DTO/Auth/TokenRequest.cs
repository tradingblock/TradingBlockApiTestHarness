namespace TradingBlockApiTestHarness.DTO.Auth
{
    public class TokenRequest
    {
        public string JWS { get; }

        public TokenRequest(string jws)
        {
            JWS = jws;
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), " - JWS:", JWS);
        }
    }
}
