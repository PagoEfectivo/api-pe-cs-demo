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
        public DateTimeOffset DateExpiry { get; set; }
        public string AdminEmail { get; set; }
        public string PaymentConcept { get; set; }
        public string AdditionalData { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserUbigeo { get; set; }
        public string UserCountry { get; set; }
        public string UserDocumentType { get; set; }
        public string UserDocumentNumber { get; set; }
        public string UserPhone { get; set; }
        public string UserCodeCountry { get; set; }
    }
}
