using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel;

namespace MyApp.Models
{
    public class ImageUploadModel
    {
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get;set; }
    }
}
