using System;
using System.Collections.Generic;
using System.Text;

namespace TokenV2SampleCode.DTOs
{
    public class CreateStoredTokenRequest
    {
        public string CustomerTranRef { get; set; }

        public string ContainerName { get; set; }
        public string TokenKey { get; set; }
        public string TokenDataInput { get; set; }
        public DateTime ValidUntilUTC { get; set; }
        public string TokenName { get; set; }
        public string MiscData { get; set; }
    }
}