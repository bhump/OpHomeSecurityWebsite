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
    public class TestimonialController : Controller
    {
        // GET: Testimonial
        public ActionResult Index()
        {
            DatabaseService service = new DatabaseService();

            TestimonialViewModel model = new TestimonialViewModel();
            model.Testimonials = service.GetTestimonials();

            return View(model);
        }

        public ActionResult Manage()
        {
            DatabaseService service = new DatabaseService();

            TestimonialViewModel model = new TestimonialViewModel();
            model.Testimonials = service.GetTestimonials();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTestimonial(FormCollection collection )
        {
            var name = collection["Name"].ToString();
            var location = collection["Location"].ToString();
            var testimonial = collection["Testimonial"].ToString();

            TestimonialModel model = new TestimonialModel();
            model.ByName = name;
            model.Location = location;
            model.Testimonial = testimonial;

            DatabaseService service = new DatabaseService();

            var isSuccess = service.CreateTestimonial(model);

            return RedirectToAction("Manage", "Testimonial");
        }

        [HttpPost]
        public ActionResult DeleteTestimonial(Guid testimonialId)
        {
            DatabaseService service = new DatabaseService();

            var isSuccess = service.DeleteTestimonial(testimonialId);

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
        public ActionResult UpdateTestimonial(TestimonialModel model)
        {
            DatabaseService service = new DatabaseService();

            var isSuccess = service.UpdateTestimonial(model);

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