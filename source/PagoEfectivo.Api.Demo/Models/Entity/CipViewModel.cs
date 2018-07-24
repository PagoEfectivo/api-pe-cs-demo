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
        public CipResponse CipResponse { get; set; }

    }
    public class CipData
    {
        public long Cip { get; set; }
    }

}
