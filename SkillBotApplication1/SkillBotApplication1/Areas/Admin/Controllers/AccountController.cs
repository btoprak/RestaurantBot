using SkillBotApplication1.Areas.Admin.Attributes;
using SkillBotApplication1.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkillBotApplication1.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Login()
        {
            if (SessionHelper.User != null)
                return RedirectToAction("Index", "Bookings");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (SessionHelper.User != null)
                return RedirectToAction("Index", "Bookings");

            if (ModelState.IsValid)
            {
                if (SettingsHelper.Username == model.Username &&
                    SettingsHelper.Password == model.Password)
                {
                    SessionHelper.User = new SkillBotApplication1.Models.UserSessionModel()
                    {
                        Username = model.Username
                    };

                    return RedirectToAction("Index", "Bookings");
                }
            }
            return View(model);
        }

        [Admin]
        public ActionResult Logout()
        {
            SessionHelper.Clear();
            return RedirectToAction("Login");
        }
    }
}