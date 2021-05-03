using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace TokenV2SampleCode.UIHandler
{
    public interface ITokenV2SampleCodeUIHandler
    {
        int SelectOption(Tuple<int, string, string, string>[] Operations);
        AuthenticationHeaderValue SetCredential();
        string BuildRequest(int Option);
        bool Confirm(string Question);
    }
}