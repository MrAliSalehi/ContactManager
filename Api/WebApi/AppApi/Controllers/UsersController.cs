using AppApi.DbManagement;
using AppApi.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using AppApi.DbManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Text.Json;
namespace AppApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        #region dependency-Injection
        /// <summary>
        /// Inject Logger
        /// Inject Interface 
        /// </summary>
        private readonly ILogger<UsersController> logger;
        private IUserRepository userRepository;
        public string logmsg { get; set; }

        public UsersController(ILogger<UsersController> _logger, IUserRepository _userRepository)
        {
            logger = _logger;
            userRepository = _userRepository;
            
        }
        #endregion
        #region GetAllUsers
        [HttpGet]
        [ResponseCache(Duration = 100)]
        public async Task<IActionResult> Get()
        {
            logmsg = "somthing on get happend";
            logger.LogInformation(logmsg);
            return new ObjectResult(await userRepository.GetAll());
        }
        #endregion

        #region GET-SearchForUser
        [HttpGet("{data}")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchUser([FromRoute] string data,string token)
        {
            #region Ref
            JsonParser jp = new();
            //string result = "";
            #endregion
            
            #region Content Control
            if (!ModelState.IsValid)
            {
                return new ObjectResult($"No Data :\n{ModelState}") { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            #endregion

            #region Find And Return Data
            var search = await userRepository.Search(data);

            if (search.Count is 0)
            {
                return new ObjectResult("User Not Found") { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            else
            {
                var json = JsonConvert.SerializeObject(search);
                Request.HttpContext.Response.Headers.Add("H-Count", $"{search.Count}");
                return new ObjectResult(json) { StatusCode = (int)HttpStatusCode.OK };
            }

            #endregion
        }
        #endregion

        #region POST-AddUser
        [HttpPost]
        public async Task<IActionResult> AddNewUser([FromBody] User usr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {

                if (!await userRepository.IsExists(usr))
                {
                    var result = await userRepository.AddUser(usr);
                    return new ObjectResult($"Added") { StatusCode = (int)HttpStatusCode.OK };
                }
                else
                {
                    return new ObjectResult($"User Or PhoneNumber Is Already Exists") { StatusCode = (int)HttpStatusCode.BadRequest };
                }
            }

        }
        #endregion

        #region PUT-UpdateUser
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                if (await userRepository.IsExists(new User() { Id = id }))
                {
                    var result = await userRepository.UpdateUser(user);
                    return new ObjectResult($"modified") { StatusCode = (int)HttpStatusCode.OK };
                }
                else
                {
                    return new ObjectResult($"User Not Exists") { StatusCode = (int)HttpStatusCode.BadRequest };
                }
            }
            else
            {
                return new ObjectResult($"Invalid Data:\n{ModelState}") { StatusCode = (int)HttpStatusCode.BadRequest };
            }
        }
        #endregion

        #region DELETE-RemoveUser
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeleteUser([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                await userRepository.RemoveUser(id);
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }
        #endregion
    }
}
