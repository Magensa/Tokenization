using System;
using System.Collections.Generic;
using System.Text;

namespace TokenV2SampleCode.DTOs
{
    public class RedeemStoredTokenRequest
    {
        public string CustomerTranRef { get; set; }

        public string ContainerName { get; set; }
        public string TokenKey { get; set; }
    }
}