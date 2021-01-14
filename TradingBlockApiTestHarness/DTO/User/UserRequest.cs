namespace TradingBlockApiTestHarness.DTO.User
{
    public class UserRequest
    {
        /// <summary>
        /// Types of User request
        /// </summary>
        public enum enumRequestType
        {
            Unknown = 0,
            /// <summary>
            /// Request for market data entitlements for the given user
            /// </summary>
            TryGetUserMarketDataEntitlement = 1,
            CreateUser = 2,
            GetUserRequirements = 3,
            GetProfile = 4,
            GetSecurityChallenge = 5,
            VerifySecurityChallenge = 6,
        }

        /// <summary>
        /// Type of this user request
        /// </summary>
        public enumRequestType RequestType { get; set; }

        public int UserId { get; set; }

        public CreateUserRequest CreateUser { get; set; }

        public CreateUserRequestSecurityChallenge SecurityChallenge { get; set; }
    
        public string ClientIPv4 { get; set; }
    }
}
