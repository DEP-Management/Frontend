using System.ComponentModel.DataAnnotations;

namespace DEP_Blazor_WASM.Entities.RequestModels
{
    public class ChangePasswordRequest
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Nuværende adgangskode er påkrævet")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ny adgangskode er påkrævet")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ny adgangskode er påkrævet")]
        public string NewPasswordCheck { get; set; } = string.Empty;
    }
}
