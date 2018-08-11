using OpHomeSecurity.Web.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.Models
{
    public class GalleryModel
    {
        public List<tAlbum> Albums { get; set; }

        public List<tImage> Images { get; set; }
    }
}