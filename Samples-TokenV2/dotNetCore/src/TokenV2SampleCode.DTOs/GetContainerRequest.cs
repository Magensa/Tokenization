using System;
using System.Collections.Generic;
using System.Text;

namespace TokenV2SampleCode.DTOs
{
    public class GetContainerRequest
    {
        public string CustomerTranRef { get; set; }

        public string ContainerName { get; set; }
    }
}