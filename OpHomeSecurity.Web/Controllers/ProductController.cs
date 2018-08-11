using OpHomeSecurity.Web.Database;
using OpHomeSecurity.Web.Models;
using OpHomeSecurity.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpHomeSecurity.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            DatabaseService service = new DatabaseService();

            ProductViewModel model = new ProductViewModel();
            model.Products = service.GetProducts();

            return View(model);
        }

        [Authorize]
        public ActionResult Manage()
        {
            DatabaseService service = new DatabaseService();

            ProductViewModel model = new ProductViewModel();
            model.Products = service.GetProducts();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateProduct(FormCollection collection, HttpPostedFileBase productImage)
        {
            DatabaseService service = new DatabaseService();

            var name = collection["Name"];
            var description = collection["Description"];

            var product = new ProductModel();
            product.Name = name;
            product.Description = description;

            var image = new ImageModel();

            if (productImage != null)
            {
                image.ContentLength = productImage.ContentLength;
                image.ImageName = productImage.FileName;
                image.InputStream = productImage.InputStream;
            }

            var isSuccess = service.CreateProduct(product, image);

            if(isSuccess)
            {
                return RedirectToAction("Manage", "Product");
            }

            return RedirectToAction("Manage", "Product");
        }

        [HttpPost]
        public ActionResult RemoveProduct(Guid productId)
        {
            DatabaseService service = new DatabaseService();

            bool isSuccess = service.DeleteProduct(productId);

            if (isSuccess)
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