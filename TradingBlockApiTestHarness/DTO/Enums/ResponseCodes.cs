namespace TradingBlockApiTestHarness.DTO.Enums
{
    /// <summary>
    /// All API requests will return an error code in the response
    /// </summary>
    public enum enumResponseCode
    {
        #region 0-99 GENERIC
        /// <summary>
        /// Request was successful
        /// </summary>
        Success = 0,
        /// <summary>
        /// One or more request parameters are invalid for this type of request
        /// </summary>
        InvalidRequestParameters = 98,
        /// <summary>
        /// Generic failure response for request
        /// </summary>
        GenericError = 99,
        #endregion 0-99 GENERIC

        #region 100-199 LOGIN ONLY (GETTOKEN())
        /// <summary>
        /// Username or password supplied when logging in is incorrect
        /// </summary>
        IncorrectUserOrPass = 100,
        /// <summary>
        /// Password supplied could not be decrypted
        /// </summary>
        FailedToDecryptUserPass = 101,
        /// <summary>
        /// Message supplied could not be decrypted
        /// </summary>
        FailedToDecryptMessage = 102,
        /// <summary>
        /// Entity supplied is incorrect
        /// </summary>
        IncorrectEntity = 103,
        /// <summary>
        /// Timestamp in login request is expired (avoids replay attacks)
        /// </summary>
        ExpiredRequest = 104,
        /// <summary>
        /// Could not parse payload into correct parts
        /// </summary>
        FailedToParsePayload = 105,
        /// <summary>
        /// Message supplied was null
        /// </summary>
        MessageIsNULL = 106,
        /// <summary>
        /// Could not split message into required parts
        /// </summary>
        FailedToSplitMessage = 107,
        /// <summary>
        /// Could not decode header in JWS
        /// </summary>
        FailedToDecodeHeader = 108,
        /// <summary>
        /// Could not decode payload in JWS
        /// </summary>
        FailedToDecodePayload = 109,
        /// <summary>
        /// Too many failed login attempts
        /// </summary>
        TooManyFailedLoginAttempts = 110,
        /// <summary>
        /// End user's password does not meet our current security requirements; password needs to be changed
        /// </summary>
        EndUserPasswordDoesNotMeetSecurityRequirements = 111,
        /// <summary>
        /// End user's password is a temporary password; password needs to be changed
        /// </summary>
        EndUserPasswordIsTemporaryAndNeedsToBeChanged = 112,
        /// <summary>
        /// End user's password is expired; password needs to be changed
        /// </summary>
        EndUserPasswordIsExpired = 113,
        /// <summary>
        /// End user has no security questions on file; they need to create security questions
        /// </summary>
        EndUserHasNoSecurityQuestions = 114,
        #endregion 100-199 LOGIN ONLY (GETTOKEN())

        #region 200-299 USER/ACCOUNT/SUBACCOUNT RELATED (typically shared by multiple of the other below categories)
        /// <summary>
        /// User is not authorized for this given request. May be because user does not have access to the requested account, or another related reject
        /// </summary>
        NotAuthorizedForGivenRequest = 200,
        /// <summary>
        /// User does not have visibility to see any accounts
        /// </summary>
        UserHasAccessToZeroAccounts = 201,
        /// <summary>
        /// No account that the user has access to has an API agreement accepted on file
        /// </summary>
        NoAccountFoundWithApiAgreementAccepted = 202,
        /// <summary>
        /// Specified account in the request does not have an API agreement accepted on file
        /// </summary>
        AccountDoesNotHaveApiAgreement = 203,
        /// <summary>
        /// User has visibility to multiple accounts, but no specific account was specified in the request; we need to know which account they intend the request to be for
        /// </summary>
        MustSpecifySpecificAccountId = 204,
        /// <summary>
        /// Account has multiple subaccounts but no specific subaccount was specified in the request; we need to know which subaccount they intend the request to be for
        /// </summary>
        MustSpecifySpecificSubaccountId = 205,
        /// <summary>
        /// Subaccount specified in the request does not belong to the given account supplied in the request
        /// </summary>
        SubaccountIsNotOwnedBySpecifiedAccount = 206,
        /// <summary>
        /// There is a new agreement on file that the client needs to accept; client can log into tradingblock.com and accept the new agreement
        /// </summary>
        AccountNeedsToAcceptUpdatedAgreement = 207,
        /// <summary>
        /// End user has no more security questions available because of incorrect answers
        /// </summary>
        EndUserHasNoMoreSecurityQuestions = 208,
        /// <summary>
        /// End user has failed to answer the security question correctly
        /// </summary>
        EndUserFailedTheSecurityQuestion = 209,
        #endregion 200-299 USER/ACCOUNT/SUBACCOUNT RELATED (typically shared by multiple of the other below categories)

        #region 300-399 ORDER PLACEMENT
        /// <summary>
        /// New order pre-validation failed; one or more order properties is invalid
        /// or not set on the given order request
        /// </summary>
        OrderPrevalidationFailed = 300,
        /// <summary>
        /// Account and/or sub-account does not have sufficient funds and/or
        /// trade would create an unauthorized strategy
        /// </summary>
        MarginCheckFailed = 301,
        /// <summary>
        /// Supplied symbol for the given order is invalid
        /// </summary>
        OrderSymbolValidationFailed = 302,
        /// <summary>
        /// OrderId supplied is invalid
        /// </summary>
        InvalidOrderId = 303,
        /// <summary>
        /// Order could not be found by the given OrderId
        /// </summary>
        OrderNotFound = 304,
        /// <summary>
        /// Order validation resulted in one or more warnings. If you choose to proceed with the order,
        /// set BypassWarnings property to true.
        /// </summary>
        OrderValidationWarning = 305,
        #endregion 300-399 ORDER PLACEMENT

        #region 400-499 QUOTES
        /// <summary>
        /// User is not allowed to receive any quotes because they have not completed the Market Data Questionnaire, or their questionnaire is expired
        /// </summary>
        UserMustCompleteMarketDataQuestionnaire = 400,
        #endregion 400-499 QUOTES
  
        #region 500-599 CASHIERING
        /// <summary>
        /// The requested ACH relationship was not found.
        /// </summary>
        AchRelationshipNotFound = 500,
        /// <summary>
        /// The requested ACH relationship is in a State that is invalid for the requested operation.
        /// </summary>
        AchRelationshipIsInInvalidStateForOperation = 501,
        /// <summary>
        /// The Approval Method specified in the operation does not match the Approval Method of the ACH relationship. Only MicroDeposit is currently supported.
        /// </summary>
        AchRelationshipApprovalMethodMismatch = 502,
        /// <summary>
        /// The requested transfer beneficiary was not found.
        /// </summary>
        TransferBeneficiaryNotFound = 503,
        /// <summary>
        /// The requested transfer intermediary was not found.
        /// </summary>
        TransferIntermediaryNotFound = 504,
        /// <summary>
        /// The requested transfer recipient bank was not found.
        /// </summary>
        TransferRecipientBankNotFound = 505,
        /// <summary>
        /// The requested transfer was not found.
        /// </summary>
        TransferNotFound = 506,
        /// <summary>
        /// The requested transfer is in a State that is invalid for the requested operation.
        /// </summary>
        TransferIsInInvalidStateForOperation = 507,
        /// <summary>
        /// The Cashiering request failed validation.
        /// </summary>
        CashieringRequestFailedValidation = 508,
        /// <summary>
        /// The transfer beneficiary Mechanism specified in the operation does not match the Mechanism of the transfer.
        /// </summary>
        TransferBeneficiaryMechanismMismatch = 509,
        /// <summary>
        /// The ACH relationship referenced by the transfer is in a State that is invalid for the requested operation
        /// </summary>
        TransferAchRelationshipIsInInvalidStateForOperation = 510,
        /// <summary>
        /// The Apex Sentinel service failed the requested operation
        /// </summary>
        ApexSentinelServiceFailedTheOperation = 550,
        #endregion 500-599 CASHIERING
 
        #region 600-699 USERS
        /// <summary>
        /// The user name failed validation or or is already in use
        /// </summary>
        UserNameIsInvalidOrAlreadyExists = 600,
        #endregion 600-699 USERS
    }
}
