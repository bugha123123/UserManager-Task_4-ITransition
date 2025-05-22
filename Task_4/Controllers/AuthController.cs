using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task_4.DTO;
using Task_4.DTO;
using Task_4.Interface;

namespace Task_4.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult signin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> signin(signinDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var success = await _authService.LoginAsync(dto);
            if (success)
            {
                return RedirectToAction("Index", "Home"); 
            }

            TempData["BlockUserError"] = "Your account has been blocked. Please contact support for assistance.";
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(dto);
        }

        [HttpGet]
        public IActionResult signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> signup(signupDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var (success, error) = await _authService.RegisterAsync(dto);
            if (success)
            {
                TempData["SuccessMessage"] = "Account created successfully! Please sign in.";
                return RedirectToAction("signin", "Auth");
            }
            

            // Show specific errors from Identity or DB
            ModelState.AddModelError(string.Empty, error ?? "Registration failed.");
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> signout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("signin", "Auth");
        }

    }
}
