using Microsoft.AspNetCore.Identity;

namespace CyberQuiz.BLL.Services
{
    public interface IUserService
    {
        //Home  Login
        Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure);

        //Login.razor
        Task<SignInResult> PasskeySignInAsync(string credentialJson);

        //Register.Razor
        Task<IdentityResult> CreateUserAsync(string email, string password);

        // Register.razor --> e-postbekräftelsetoken
        Task<string> GenerateEmailConfirmationTokenAsync(string email);

        // Register.razor -- > skickar bekräftelselänk
        Task SendConfirmationLinkAsync(string email, string confirmationLink);

        // Register.razor --> Loggar in en nyregistrerad användare direkt (om RequireConfirmedAccount = false)
        Task SignInAfterRegisterAsync(string email, bool isPersistent);

        // Kontrollerar om RequireConfirmedAccount är aktiverat, används av: Register.razor
        bool RequireConfirmedAccount { get; }

        // ChangePassword.razor  Emial.razor
        // Hämtar userId från ClaimsPrincipal, används av: ChangePassword.razor, Email.razor
        Task<string?> GetUserIdFromPrincipalAsync(System.Security.Claims.ClaimsPrincipal principal);

        // ChangePassword.razor --> Kontrollerar om användaren har ett lösenord satt
        Task<bool> HasPasswordAsync(string userId);

        // ChangePassword
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);

        // ChangePassword --> Uppdaterar sign-in-cookien efter lösenordsbyte
        Task RefreshSignInAsync(string userId);

        // Email-razor --> Hämtar nuvarande e-post för en användare
        Task<string?> GetEmailAsync(string userId);

        // Email.razor --> Kontrollerar om e-posten är bekräftad
        Task<bool> IsEmailConfirmedAsync(string userId);

        // Email.razor --> Genererar token för e-postbyte (OnValidSubmitAsync)
        Task<string> GenerateChangeEmailTokenAsync(string userId, string newEmail);

        // Email.razor --> Skickar bekräftelselänk för e-postbyte
        Task SendEmailChangeLinkAsync(string userId, string newEmail, string confirmationLink);

        // Email.razor --> Skickar verifieringsmail (om e-post ej bekräftad) (OnSendEmailVerificationAsync)
        Task SendEmailVerificationAsync(string userId, string verificationLink);

        // ChangeEmail --> Verifierar lösenord och byter e-post direkt
        Task<IdentityResult> ChangeEmailAsync(string userId, string currentPassword, string newEmail);
    }
}
