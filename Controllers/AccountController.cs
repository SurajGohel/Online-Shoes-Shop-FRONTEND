using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using Online_Shoes_Shop.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Online_Shoes_Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Register(RegisterUserModel registerUserModel)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        ModelState.Clear();
        //    }
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel registerUserModel)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(registerUserModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Account/register", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Handle server-side errors and display them in ModelState
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Registration failed: {errorResponse}");
                }
            }
            // If ModelState is not valid or an error occurred, re-display the form with errors
            return View(registerUserModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Pass the model back to preserve user input
            }

            var jsonContent = JsonConvert.SerializeObject(model); // Directly serialize the model
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Account/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response JSON to extract the token
                var responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

                if (responseData != null && responseData.ContainsKey("token"))
                {
                    var token = responseData["token"];
                    var handler = new JwtSecurityTokenHandler();

                    if (handler.CanReadToken(token))
                    {
                        var jwtToken = handler.ReadJwtToken(token);

                        // Print claims for debugging
                        //foreach (var claim in jwtToken.Claims)
                        //{
                        //    Console.WriteLine($"{claim.Type}: {claim.Value}");
                        //}

                        var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                        var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                        var expiration = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
                        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                        // Create cookie options
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddMinutes(30) // Set expiration time as per token expiration
                        };

                        // Store claims in cookies
                        Response.Cookies.Append("userid", userId, cookieOptions);
                        Response.Cookies.Append("username", username, cookieOptions);
                        Response.Cookies.Append("role", role, cookieOptions);
                        Response.Cookies.Append("expiration", expiration, cookieOptions);
                        Response.Cookies.Append("email", email, cookieOptions);

                        if (role == "Admin")
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });

                        }
                        else
                        {
                            return RedirectToAction("Index", "Shoes");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid token format received.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Token not found in the response.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login failed. Please check your credentials and try again.");
            }

            return View(model);



        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Remove specific cookies
            if (HttpContext.Request.Cookies["role"] == "User")
            {
                Response.Cookies.Delete("userid");

                Response.Cookies.Delete("username");
                Response.Cookies.Delete("role");
                Response.Cookies.Delete("expiration");
                // Optionally, you can clear the authentication cookie if you are using an authentication framework
                HttpContext.SignOutAsync();

                // Redirect to the login page
                return RedirectToAction("Index", "Shoes");
            }
            
            return RedirectToAction("Index", "Shoes");
            
        }

        [HttpGet]
        public IActionResult IsUserLoggedIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new { isLoggedIn = true });
            }
            return Json(new { isLoggedIn = false });
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
