using OpHomeSecurity.Web.Database;
using OpHomeSecurity.Web.Models;
using OpHomeSecurity.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpHomeSecurity.Web.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            DatabaseService service = new DatabaseService();

            var results = service.GetImages();

            GalleryViewModel model = new GalleryViewModel();
            model.Albums = results;

            return View(model);
        }

        [Authorize]
        public ActionResult Manage()
        {
            DatabaseService service = new DatabaseService();

            AlbumViewModel model = new AlbumViewModel();
            model.Albums = service.GetAlbums();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAlbum(FormCollection collection)
        {
            DatabaseService service = new DatabaseService();

            var albumName = collection["AlbumName"].ToString();
            var isFeatured = collection["IsFeatured"];

            bool isFeaturedBool = false;

            if(isFeatured == "true,false")
            {
                isFeaturedBool = true;   
            }
            else
            {
                isFeaturedBool = false;
            }

            bool isSuccess = service.CreateAlbum(albumName, isFeaturedBool);

            return RedirectToAction("Manage", "Gallery");
        }

        [Authorize]
        public ActionResult ManageImages()
        {
            DatabaseService service = new DatabaseService();

            ImagesViewModel model = new ImagesViewModel();
            model.Images = service.GetImages(Request.QueryString["albumId"]);

            return View(model);
        }

        [HttpPost]
        public ActionResult AddImages(FormCollection collection, IEnumerable<HttpPostedFileBase> images)
        {
            DatabaseService service = new DatabaseService();

            var albumId = collection["albumId"].ToString();

            var imageList = new List<ImageModel>();

            foreach (var image in images)
            {
                ImageModel model = new ImageModel();
                model.ContentLength = image.ContentLength;
                model.ImageName = image.FileName;
                model.InputStream = image.InputStream;
                model.AlbumId = new Guid(albumId);

                imageList.Add(model);
            }

            bool isSuccess = service.CreateImage(imageList);

            return RedirectToAction("ManageImages", new { albumId = albumId });
        }

        [HttpPost]
        public ActionResult RemoveImage(Guid imageId)
        {
            DatabaseService service = new DatabaseService();

            var isSuccess = service.DeleteImage(imageId);

            var albumId = Request.QueryString["albumId"];

            if (isSuccess)
            {
                return Json(new { Response = "Success" });
            }
            else
            {
                return Json(new { Response = "Error" });
            }
        }

        [HttpPost]
        public ActionResult RemoveAlbum(Guid albumId)
        {
            DatabaseService service = new DatabaseService();

            var isSuccess = service.DeleteAlbum(albumId);

            if(isSuccess)
            {
                return Json(new { Response = "Success" });
            }
            else
            {
                return Json(new { Response = "Error" });
            }
        }
    }
}