using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecureApiWithAuthentication.Auth;
using SecureApiWithAuthentication.Authorization;
using SecureApiWithAuthentication.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecureApiWithAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController (JwtOtptions options ,AppDbcontext context): ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public  ActionResult<string> Authentication(AuthenticationModel model)
        {
            var user = context.Users.FirstOrDefault(x=>x.Name==model.UserName &&x.Password==model.Password );
            if (user == null)
            {
                return Unauthorized("UserName or Password Invalid Please try agai");
            }
            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenDiscriptor = new SecurityTokenDescriptor
            {
                Issuer = options.Issuer,
                Audience = options.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.signinKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString()),
                    new Claim(ClaimTypes.Email, "a@g.com")
                    
                })

            };
            var securityToken = TokenHandler.CreateToken(TokenDiscriptor);
            var accesstoken = TokenHandler.WriteToken(securityToken);
            return Ok(accesstoken);
        }

        [HttpPost]
        [Route("Registration")]
        public ActionResult<users>Registration(users user)
        {
            var model = new users
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };

            context.Add(model);
            context.SaveChanges();
            return Ok(model);
        }
    }

}
