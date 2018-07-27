using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagoEfectivo.Api.Demo.Models.Entity
{
    public class CipGenerate
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string TransactionCode { get; set; }
        public string UserEmail { get; set; }
    }
}
