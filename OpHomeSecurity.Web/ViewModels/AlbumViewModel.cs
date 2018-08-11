using OpHomeSecurity.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpHomeSecurity.Web.ViewModels
{
    public class AlbumViewModel
    {

        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }

        public Guid AlbumId { get; set; }

        public bool IsActive { get; set; }

        [Display(Name ="Featured Album?")]
        public bool IsFeatured { get; set; }

        public List<AlbumModel> Albums { get; set; }
    }
}