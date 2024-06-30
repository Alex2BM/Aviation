using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FinalProjectAviation.DTO;
using FinalProjectAviation.Models;
using FinalProjectAviation.Services;
using FinalProjectAviation.Data;

namespace FinalProjectAviation.Controllers
{
    public class UserController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;

        public UserController(IApplicationService applicationService)
            : base()
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserSignupDTO userSignupDTO)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState.Values)
                {
                    foreach (var error in entry.Errors)
                    {
                        ErrorArray!.Add(new Error("", error.ErrorMessage, ""));
                    }
                }
                ViewData["ErrorArray"] = ErrorArray;
                return View(); 
            }

            try
            {
                await _applicationService.UserService.SignUpUserAsync(userSignupDTO);
                return RedirectToAction("Login", "User");
            }
            catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
                ViewData["ErrorArray"] = ErrorArray;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal principal = HttpContext.User;
            if (principal.Identity!.IsAuthenticated)
            {
                if (principal.FindFirst(ClaimTypes.Role)!.Value == "Pilot")
                {
                    return RedirectToAction("Index", "Pilot");
                }
                else if (principal.FindFirst(ClaimTypes.Role)!.Value == "Passenger")
                {
                    return RedirectToAction("Index", "Passenger");
                }
                else if (principal.FindFirst(ClaimTypes.Role)!.Value == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDTO credentials)
        {
            var user = await _applicationService.UserService.VerifyAndGetUserAsync(credentials);
            if (user == null)
            {
                ViewData["ValidateMessage"] = "Error: User/Password not found ";
                return View();
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, credentials.Username!),
                new Claim(ClaimTypes.Role, user.UserRole!.ToString()!)
            };

            ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new()
            {
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

            if (user.UserRole == UserRole.Pilot)
            {
                return RedirectToAction("Index", "Pilot");
            }
            else if (user.UserRole == UserRole.Passenger)
            {
                return RedirectToAction("Index", "Passenger");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}
