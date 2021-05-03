using System;
using System.Collections.Generic;
using System.Text;

namespace TokenV2SampleCode.DTOs
{
    public class CreateTokenRequest
    {
        public string CustomerTranRef { get; set; }

        public string TokenDataInput { get; set; }
        public DateTime ValidUntilUTC { get; set; }
        public string TokenName { get; set; }
        public string MiscData { get; set; }
    }
}