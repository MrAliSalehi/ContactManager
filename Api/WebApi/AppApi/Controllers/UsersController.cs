using AppApi.DbManagement;
using AppApi.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AppApi.DbManagement.Interfaces;
namespace AppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        #region dependency-Injection
        /// <summary>
        /// Inject Logger
        /// Inject Interface 
        /// </summary>
        private readonly ILogger<UsersController> _logger;
        private IUserRepository userRepository;
        public UsersController(ILogger<UsersController> logger, IUserRepository _userRepository)
        {
            _logger = logger;
            userRepository = _userRepository;
        }
        #endregion

        #region GetAllUsers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await userRepository.GetAll());
        }
        #endregion

        #region GET-SearchForUser
        [HttpGet("{data}")]
        public async Task<IActionResult> SearchUser([FromRoute] string data)
        {
            #region Ref
            JsonParser jp = new();
            string result = "";
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
                foreach (var user in search)
                {
                    result += $"{jp.DataToJson(user)},\n";
                }
                Request.HttpContext.Response.Headers.Add("H-Count", $"{search.Count}");
                return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.OK };
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
                    return new ObjectResult($"{result}") { StatusCode = (int)HttpStatusCode.OK };
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
                    var result = userRepository.UpdateUser(user);
                    return new ObjectResult($"{result}") { StatusCode = (int)HttpStatusCode.OK };
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
