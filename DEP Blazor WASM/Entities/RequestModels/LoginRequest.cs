using System.ComponentModel.DataAnnotations;

namespace DEP_Blazor_WASM.Entities.RequestModels
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Brugernavn er påkrævet")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password påkrævet")]
        public string Password { get; set; } = string.Empty;
    }
}
