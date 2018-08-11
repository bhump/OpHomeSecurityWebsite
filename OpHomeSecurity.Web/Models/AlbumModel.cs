using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.Models
{
    public class AlbumModel
    {
        public string AlbumName { get; set; }

        public Guid AlbumId { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsFeatured { get; set; }
    }
}