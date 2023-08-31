using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PFMBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PFMBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IConfiguration _configuration;

            public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _configuration = configuration;
            }


            [HttpPost]
            [Route("register")]
            public async Task<IActionResult> Register(RegisterViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userExists = await _userManager.FindByEmailAsync(model.Email);

                if (userExists != null)
                {
                    return BadRequest(new { message = "User already exists with this email." });
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfilePictureUrl = null, 
                    CurrencyPreference = null, 
                    TotalBalance = 0, 
                    FinancialGoals = new List<FinanceGoal>(), 
                    Accounts = new List<AccountModel>(),
                    Transactions = new List<TransactionModel>(),
                    TransactionCategories = new List<CategoryModel>()
                };


                if (string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest(new { message = "Password cannot be null or empty." });
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { message = "User successfully registered and signed in." });
                }

                return BadRequest(result.Errors);
            }

            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> Login([FromBody] LoginModel userModel)
            {
                if (!ModelState.IsValid || string.IsNullOrEmpty(userModel.Password) || string.IsNullOrEmpty(userModel.Email))
                {
                    return BadRequest(ModelState);
                }
                var user = await _userManager.FindByEmailAsync(userModel.Email);

                if (user == null)
                {
                    return BadRequest("User does not exist");
                }

                if (!await _userManager.CheckPasswordAsync(user, userModel.Password))
                {
                    return BadRequest("Invalid password");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                };

                var token = GenerateJwtToken(claims);

                return Ok(new { Token = token });

            }

            [HttpGet]
            [Route("profile")]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            public async Task<IActionResult> GetProfile()
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var profile = new
                {
                    user.Id,
                    user.Email,
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    user.ProfilePictureUrl, 
                    user.CurrencyPreference, 
                    user.TotalBalance, 
                };

                return Ok(profile);
            }



            private string GenerateJwtToken(List<Claim> claims)
            {
                var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? _configuration["Jwt:Key"];
                var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? _configuration["Jwt:Issuer"];

                if (string.IsNullOrEmpty(jwtKey))
                {
                    throw new ArgumentNullException("Jwt:Key cannot be null or empty in configuration.");
                }

                if (string.IsNullOrEmpty(issuer))
                {
                    throw new ArgumentNullException("Jwt:Issuer cannot be null or empty in configuration.");
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: issuer,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            

    }
}