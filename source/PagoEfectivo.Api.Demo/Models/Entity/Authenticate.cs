using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagoEfectivo.Api.Demo.Models.Entity
{
    public class Authenticate
    {
        public string accessKey { get; set; }
        public int idService { get; set; }
        public string dateRequest { get; set; }
        public string hashString { get; set; }
    }
}
