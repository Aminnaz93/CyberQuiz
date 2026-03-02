

using Microsoft.AspNetCore.Identity;

namespace CyberQuiz.API.Controllers
{


    public class UserManagementController
    {
        private readonly SignInManager signInManager;

        public UserManagementController(SignInManager SignInManager)
        {
            signInManager = SignInManager;
        }
    }
}
