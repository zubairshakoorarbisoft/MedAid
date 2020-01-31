using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MedAidAPI.Areas.Identity.Data;
using MedAidAPI.ViewModels;

namespace MedAidAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<MedAidAPIUser> _userManager;


        public AuthController(UserManager<MedAidAPIUser> userManager)
        {
            _userManager = userManager;
        }



        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody]LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"));

                var token = new JwtSecurityToken(
                    issuer: "localhost",
                    audience: "localhost",
                    expires: DateTime.UtcNow.AddHours(720), // One Month Expiry Added
                    claims: claims,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                try
                {
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                catch (Exception ex)
                {
                    var message = ex;
                }
            }
            return Unauthorized();
        }
    }
}