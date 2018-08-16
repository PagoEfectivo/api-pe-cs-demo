using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagoEfectivo.Api.Demo.Models.Entity
{
    public class AuthenticateResponseData
    {
        public string Token { get; set; }
        public string CodeService { get; set; }
        public string TokenStart { get; set; }
        public string TokenExpires { get; set; }
    }
}
