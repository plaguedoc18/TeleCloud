using ITUniver.TeleCloud.Core;
using ITUniver.TeleCloud.WebCloud.Models;
using ITUniver.TeleCloud.WebCloud.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCloud.WebCloud.Controllers
{
    //[Authorize]
    public class FilesController : Controller
    {
        private Disk action { get; set; }
        private NHFileRepository FilesRepository { get; set; }
        private NHUserRepository UserRepository { get; set; }
        public FilesController()
        {
            var connString = ConfigurationManager.ConnectionStrings["TeleCloud"];
            action = new Disk(@"C:\ituniver\TeleCloud\shron\");
            FilesRepository = new NHFileRepository();
            UserRepository = new NHUserRepository();
        }
        public ActionResult Files(FileItemModel model)
        {
            var items = FilesRepository.Find("");
            return View(items);
        }
        public ActionResult Delete(FileItemModel model)
        {
            var DeleteFile = $"[Id] = {model.Id}";
            if (UserRepository.Delete(DeleteFile))
            {
                return RedirectToAction("List");
            }
            ModelState.AddModelError("", "Не получилось удалить");
            return RedirectToAction("List");
        }
        private User CurrUser()
        {
            return UserRepository.Find($" where [Login]='{User.Identity.Name}'").FirstOrDefault();
        }
        public ActionResult List()
        {
            var files = FilesRepository.Find().Where(x => x.IdUser.Id == CurrUser().Id);
            return View(files);
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult Search(MSearchView model)
        {
            var files = FilesRepository.Find().Where(x => (x.NameFile == model.SearchField) && (x.IdUser.Id == CurrUser().Id));
            return PartialView("_Search", files);
        }
    }
}