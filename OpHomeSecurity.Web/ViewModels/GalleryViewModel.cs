using OpHomeSecurity.Web.Database;
using OpHomeSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.ViewModels
{
    public class GalleryViewModel
    {
        public List<tAlbum> Albums { get; set; }
    }
}