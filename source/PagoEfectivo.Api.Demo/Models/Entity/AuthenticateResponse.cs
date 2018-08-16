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

    public class GenerateResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public GenerateCip Data { get; set; }

    }

    public class GenerateCip
    {
        public Int64 Cip { get; set; }
        public string Currency { get; set; }
        public decimal amount { get; set; }
        public string TransactionCode { get; set; }
        public string CipUrl { get; set; }
    }
}
