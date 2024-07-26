namespace Application.Features.Auth.Constants
{
    public static class AuthBusinessMessage
    {
        public const string AuthenticatorCodeSubject = "You have to enter code for login - RentACar";
        public const string InvalidAuthenticatorCode = "Invlaid code.";
        public const string UserNotFound = "User not found.";
        public const string InvlaidPassword = "User email or password is invlaid";
        public const string ClickOnBelowLinkToVerifyEmail = "Please click below the link and verify your email.";
        public const string RefreshTokenNotFound = "Refresh token not found";
        public const string RefreshTokenNotActive = "Refresh Token not active";
        public const string UserHasAuthenticator = "User has authenticator";
        public const string UserEmailAlreadyExists = "Given email address has been already registered.";
        public const string UserEmailAuthenticatorNotFound = "It does not exist in the user's e-mail query that needs to be confirmed.";
        public const string UserOtpAuthenticatorNotFound = "It does not exist in the users's otp query that need to be confirmed.";
        public const string VerifyEmail = "Verify your email";
        public const string CouldntFindChildToken = "Couldn't find child token for this current token.";
        public const string ExternalLoginCredentialsWrong = "Your external login credentials wrong";
        public const string UserPermaBanned = "Your account is permanently banned.";


        public static string AuthenticatorCodeTextBody(string authenticatorCode)
            => $"Your Two factor authcode: {authenticatorCode.Substring(startIndex: 0, length: 3)} {authenticatorCode.Substring(3)}";
    }
}
