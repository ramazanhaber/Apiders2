using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiders2.Models
{
    public class GenelModel
    {
        public bool success { get; set; } = true;
        public string message { get; set; } = "Başarılı";
        public object data { get; set; }

    }
}