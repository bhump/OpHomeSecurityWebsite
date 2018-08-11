using OpHomeSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.ViewModels
{
    public class TestimonialViewModel
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Testimonial { get; set; }

        public List<TestimonialModel> Testimonials { get; set; }
    }
}