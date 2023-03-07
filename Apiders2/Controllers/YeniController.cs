using Apiders2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apiders2.Controllers
{
    public class YeniController : ApiController
    {
        public List<YeniTablo> getItems()
        {
            POSDBEntities context = new POSDBEntities();
            return context.YeniTablo.ToList();
        }

        
    }
}