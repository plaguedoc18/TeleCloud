using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ITUniver.TeleCloud.WebCloud.Models;
using System.Web.Security;
using ITUniver.TeleCloud.WebCloud.Repositories;
using System.Configuration;
using System.IO;
using System.Web.Hosting;

namespace ITUniver.TeleCloud.WebCloud.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private NHUserRepository UserRepository { get; set; }
        public AccountController()
        {
            var connString = ConfigurationManager.ConnectionStrings["TeleCalc"];
            UserRepository = new NHUserRepository();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ошибка авторизации");
                return View(model);
            }
            var user = UserRepository
                .Find($"where [Login] = N'{model.Login}' AND password = N'{model.Password}' ")
                .FirstOrDefault();
            if (user == null)
            {
                // Если записи нет, то ошибка и заново
                ModelState.AddModelError("", "Ошибка авторизации");
                return View(model);
            }
            FormsAuthentication.SetAuthCookie(model.Login, true);
            return RedirectToAction("List", "Files");
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User model)
        {
            if (UserRepository.Save(model))
            {
                DirectoryInfo dirnew = new DirectoryInfo(HostingEnvironment.MapPath(($"~/Chanilishe/{model.Login}/")));
                dirnew.Create();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Не получилось сохранить");
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}