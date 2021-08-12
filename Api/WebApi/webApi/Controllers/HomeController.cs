using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using webApi.Models;
using webApi.Models.Entities;
using Newtonsoft.Json;
using webApi.JsonHelpers;
using System.Net.Http;
using System.Net;

namespace webApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Ok("Hi Api");
        }
        #region Get-SearchForUser
        [Route("Search")]
        [HttpGet]
        public IActionResult SearchUser(string userfullname)
        {
            if (string.IsNullOrEmpty(userfullname))
            {
                return Ok("User Not Found");
            }
            using (var db = new contactContext())
            {
                var search = db.Users.Where(p => p.FullName.Contains(userfullname) || p.Phone.Contains(userfullname) || p.Email.Contains(userfullname)).SingleOrDefault();
                JsonParser jp = new();
                return Ok(jp.DataToJson(search));
            }
        }
        #endregion

        #region Post-AddUser
        [Route("Add")]
        [HttpPost]
        public HttpStatusCode AddNewUser(User usr)
        {
            using (contactContext db = new())
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

        #region DefaultErrorHandler
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
