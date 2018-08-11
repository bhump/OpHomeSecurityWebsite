using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.Models
{
    public class TestimonialModel
    {
        public System.Guid TestimonialId { get; set; }

        public string Testimonial { get; set; }

        public Nullable<bool> Active { get; set; }

        public string ByName { get; set; }

        public string Location { get; set; }
    }
}