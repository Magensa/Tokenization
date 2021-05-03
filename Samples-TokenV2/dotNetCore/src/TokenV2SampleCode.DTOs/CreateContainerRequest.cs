using System;
using System.Collections.Generic;
using System.Text;

namespace TokenV2SampleCode.DTOs
{
    public class CreateContainerRequest
    {
        public string CustomerTranRef { get; set; }

        public string ContainerName { get; set; }
        public int TokenKeyTypeID { get; set; }
        public string AutoGenKeyMask { get; set; }
        public int? AutoGenKeyPlaceholderTypeID { get; set; }
        public string AutoGenKeyPlaceholderChar { get; set; }
        public string AutoGenKeyPadChar { get; set; }
        public string UserGenKeyRegex { get; set; }
    }
}