using System.ComponentModel.DataAnnotations;

namespace CyberQuiz.Shared.DTOs
{
    // För inloggad användare som vill byta e-post
    // CurrentPassword krävs för extra säkerhet
    public class ChangeEmailDto
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; } = string.Empty;

        [Required]
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
