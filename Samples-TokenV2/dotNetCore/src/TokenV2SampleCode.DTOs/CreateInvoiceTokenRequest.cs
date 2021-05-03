using System;
using System.Collections.Generic;
using System.Text;

namespace TokenV2SampleCode.DTOs
{
    public class CreateInvoiceTokenRequest
    {
        public string CustomerTranRef { get; set; }


        public string CompanyName { get; set; }
        public string Amount { get; set; }
        public string TaxAmount { get; set; }
        public string TaxPercent { get; set; }
        public string TransactionReferenceNumber { get; set; }
        public string ProcessorName { get; set; }
        public KeyValuePair<string, string>[] AdditionalInfo { get; set; }

        public string LogoURL { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string CompanyNameFontColor { get; set; }
        public string TermsAndTransactionInfo { get; set; }


        public DateTime ValidUntilUTC { get; set; }
        public string TokenName { get; set; }
        public string MiscData { get; set; }
        public string TransactionSourceID { get; set; }
    }
}