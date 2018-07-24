using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagoEfectivo.Api.Demo.Models.Entity
{
    public class AuthenticateResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public AuthenticateResponseData Data { get; set; }
        public string url { get; set; }

    }
}
