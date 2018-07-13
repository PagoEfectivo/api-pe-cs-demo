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

    public class AuthenticateResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public AuthenticateResponseData Data { get; set; }
        public string url { get; set; }

    }
}
