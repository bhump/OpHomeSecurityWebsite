using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.Models
{
    public class ImageModel
    {
        public Guid? AlbumId { get; set; }

        public string AlbumName { get; set; }

        public Guid ImageId { get; set; }

        public string ImageName { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsFeatured { get; set; }

        public int ContentLength { get; set; }

        public Stream InputStream { get; set; }
    }
}