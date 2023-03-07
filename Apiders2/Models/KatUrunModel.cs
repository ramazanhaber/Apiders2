using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiders2.Models
{
    public class KatUrunModel
    {
        public int id { get; set; }
        public string ad { get; set; }
        public int kategoriId { get; set; }
        public DateTime tarih { get; set; }
        public double fiyat { get; set; }
        public bool aktif { get; set; }

        
    }
}