using Connectify.Models.Data;
using Connectify.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Connectify.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LiveSearch(string Username)
        {
            Db db = new Db();
            List<LiveSearchVM> users = db.Users.Where(x => x.UserName.Contains(Username) && x.UserName != User.Identity.Name).ToArray().Select(x => new LiveSearchVM(x)).ToList();
            return Json(users,JsonRequestBehavior.AllowGet);
        }

	}
}