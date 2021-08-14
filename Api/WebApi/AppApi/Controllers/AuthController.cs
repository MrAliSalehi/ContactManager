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
namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Authentication With Jwt Method And Simple Static Identify
        /// </summary>
        /// <param name="info">just pass it a admin:admin as username/password </param>
        /// <returns>returns status code:)</returns>
        [HttpPost]
        public async Task<IActionResult> AuthenUser([FromBody] Identify info)
        {
            if (ModelState.IsValid)
            {
                if (info.Username.ToLower() is "admin" && info.Password.ToLower() is "admin")
                {
                    #region Setup,Config,Create And Return Token

                    #region Setup
                    SecretKeyGen newKey = new SecretKeyGen();
                    var CreateSecret = newKey.SecretKey;
                    var Credential = new SigningCredentials(CreateSecret, SecurityAlgorithms.HmacSha256);
                    #endregion

                    #region Config
                    var TokenConfig = new JwtSecurityToken
                        (
                        signingCredentials: Credential,
                        claims: new List<Claim>() {
                            new Claim(ClaimTypes.Name, info.Username),
                            new Claim(ClaimTypes.Role, "admin")
                        },
                        expires: DateTime.Now.AddMinutes(20),
                        issuer: PublicSettings.ApiAddress.First()
                        );

                    #endregion

                    #region Create
                    var Token = new JwtSecurityTokenHandler().WriteToken(TokenConfig);
                    #endregion

                    #region Return Token
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
