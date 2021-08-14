using AppApi.Jwt;
using AppApi.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        public AuthController(ILogger<AuthController> _logger)
        {
            logger = _logger;
        }
        /// <summary>
        /// Authentication With Jwt Method And Simple Static Identify
        /// </summary>
        /// <param name="info">just pass it an admin:admin as username/password </param>
        /// <returns>returns status code:)</returns>
        [HttpPost]
        public IActionResult AuthenUser([FromBody] Identify info)
        {
            if (ModelState.IsValid)
            {
                if (info.Username.ToLower() is "admin" && info.Password.ToLower() is "admin")
                {
                    #region Setup,Config,Create And Return Token

                    #region Setup
                    SecretKeyGen newKey = new SecretKeyGen();
                    var CreateSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecretKeyFromWebApiItsVerySecret"));
                    var Credential = new SigningCredentials(CreateSecret, SecurityAlgorithms.HmacSha256);
                    #endregion

                    #region Config
                    //var TokenConfig = new JwtSecurityToken
                    //    (
                    //    signingCredentials: Credential,
                    //    claims: new List<Claim>() {
                    //        new Claim(ClaimTypes.Name, info.Username),
                    //        new Claim(ClaimTypes.Role, "admin")
                    //    },
                    //    expires: DateTime.Now.AddMinutes(20),
                    //    issuer: PublicSettings.ApiAddress.First()
                    //    );

                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name,info.Username),
                    new Claim(ClaimTypes.NameIdentifier ,info.Username),
                    new Claim (JwtRegisteredClaimNames.Nbf , new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim (JwtRegisteredClaimNames.Exp , new DateTimeOffset(DateTime.Now).AddDays(1).ToUnixTimeSeconds().ToString())
                    };

                    var tokensett = new JwtSecurityToken(new JwtHeader(
                        new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecretKeyFromWebApiItsVerySecret")),
                            SecurityAlgorithms.HmacSha256)),
                        new JwtPayload(claims));
                    #endregion

                    #region Create
                    var Token = new JwtSecurityTokenHandler().WriteToken(tokensett);
                    #endregion

                    #region Return Token
                    logger.LogInformation($"{Token}");
                    return Ok(new { token = Token });
                    #endregion

                    #endregion
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
