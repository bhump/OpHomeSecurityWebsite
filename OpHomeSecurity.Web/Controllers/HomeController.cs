using OpHomeSecurity.Web.Database;
using OpHomeSecurity.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpHomeSecurity.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DatabaseService service = new DatabaseService();

            var results = service.GetImages();

            GalleryViewModel model = new GalleryViewModel();
            model.Albums = results.Where(a => a.IsFeatured == true).ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}