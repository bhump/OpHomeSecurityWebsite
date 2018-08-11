using OpHomeSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.ViewModels
{
    public class ImagesViewModel
    {
        public Guid AlbumId { get; set; }

        public List<ImageModel> Images { get; set; }
    }
}