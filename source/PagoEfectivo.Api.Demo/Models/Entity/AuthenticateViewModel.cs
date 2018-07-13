using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagoEfectivo.Api.Demo.Models.Entity
{
    public class AuthenticateViewModel
    {
        public int IdService { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
