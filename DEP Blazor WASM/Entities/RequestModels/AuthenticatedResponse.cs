namespace DEP_Blazor_WASM.Entities.RequestModels
{
    public class AuthenticatedResponse
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime PasswordExpiryDate { get; set; }
    }
}
