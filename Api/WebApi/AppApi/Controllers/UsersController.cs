using AppApi.DbManagement;
using AppApi.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using webApi.Models.Entities;

namespace AppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private contactContext db;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, contactContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hi api");
        }
        #region POST-SearchForUser
        [HttpGet("{data}")]
        public IActionResult SearchUser(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return Ok("User Not Found");
            }
            
            using (db)
            {
                var search = db.Users.Where(p => p.FullName.Contains(data) || p.Phone.Contains(data) || p.Email.Contains(data)).ToList();
                JsonParser jp = new();
                string result = "";
                foreach (var user in search)
                {
                    result += $"{jp.DataToJson(user)},\n";
                }
                return Ok(result);
            }
        }
        #endregion

        #region POST-AddUser
        [HttpPost]
        public HttpStatusCode AddNewUser(User usr)
        {
            using (db)
            {
                if (usr.FullName is null)
                {
                    return HttpStatusCode.NoContent;
                }
                else
                {
                    if (!db.Users.Any(p => p.Phone == usr.Phone || p.FullName == usr.FullName))
                    {
                        db.Users.Add(usr);
                        db.SaveChanges();
                        return HttpStatusCode.OK;
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }

                }
            }
        }
        #endregion

        #region Put-UpdateUser
        [HttpPut]
        public HttpStatusCode UpdateUser(User user, string OldName)
        {
            if (user.FullName is not null && OldName is not null)
            {
                using (db)
                {
                    if (db.Users.Any(p => p.FullName == OldName))
                    {
                        db.Users.Update(user);
                        db.SaveChanges();
                    }
                }
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
