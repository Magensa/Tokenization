using System;
using System.Collections.Generic;
using System.Text;

namespace TokenV2SampleCode.DTOs
{
    public class RedeemTokenRequest
    {
        public string CustomerTranRef { get; set; }

        public string Token { get; set; }
    }
}