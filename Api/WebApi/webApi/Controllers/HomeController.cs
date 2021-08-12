using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using webApi.Models;
using webApi.Models.Entities;
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
            return Ok("Api");
        }
        #region Get-SearchForUser
        [Route("Search")]
        [HttpGet]
        public IActionResult SearchUser(string userfullname)
        {
            contactContext db = new contactContext();

            if (string.IsNullOrEmpty(userfullname))
            {
                return Ok("User Not Found");
            }
            var search = db.Users.Where(p => p.FullName.Contains(userfullname) || p.Phone.Contains(userfullname) || p.Email.Contains(userfullname)).SingleOrDefault();
            string[] Name = search.FullName.Split(':');
            return Ok($"User Found : {Name[0]}\n Number:{search.Phone}");
        }

        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
