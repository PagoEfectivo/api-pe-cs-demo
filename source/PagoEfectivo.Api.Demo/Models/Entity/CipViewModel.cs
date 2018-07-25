using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagoEfectivo.Api.Demo.Models.Entity
{
    public class CipViewModel
    {
        public List<CipData> Data { get; set; }
        public AuthenticateViewModel Authenticate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public string TransactionCode { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset DateExpiry { get; set; }
        public Int64 Cip { get; set; }
    }
    public class CipData
    {
        public long Cip { get; set; }
    }

}
