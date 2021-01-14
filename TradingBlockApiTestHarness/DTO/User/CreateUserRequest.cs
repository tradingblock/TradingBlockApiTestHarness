using System.Collections.Generic;

namespace TradingBlockApiTestHarness.DTO.User
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordSecret { get; set; }

        public string Email { get; set; }

        public List<CreateUserRequestSecurityChallenge> SecurityChallenges { get; set; }

        /// <summary>
        /// This only works in non-production environments
        /// </summary>
        public bool CreateInstantAccount { get; set; }
    }
}
