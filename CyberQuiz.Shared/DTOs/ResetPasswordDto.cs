using System.ComponentModel.DataAnnotations;

namespace CyberQuiz.Shared.DTOs
{
    // För ej inloggad användare som återställer lösenord via e-post
    // ResetToken skickas till användaren via e-post och används för att verifiera identiteten
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string ResetToken { get; set; } = string.Empty;

        [Required]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
