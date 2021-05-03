using System;
using System.Collections.Generic;
using System.Text;

namespace TokenV2SampleCode.DTOs
{
    public class CreatePaymentTokenRequest
    {
        public string CustomerTranRef { get; set; }

        public string PAN { get; set; }
        public string ExpirationDate { get; set; }
        public string BillingZIP { get; set; }
        public string CVV { get; set; }
        public string TransactionSourceID { get; set; }
        public DateTime ValidUntilUTC { get; set; }
        public string TokenName { get; set; }
        public string MiscData { get; set; }
    }
}