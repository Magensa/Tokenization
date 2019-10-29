using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using Tokenization.Dtos;

namespace Tokenization.ServiceFactory
{
    public class RedeemTokenWebServiceClient : IRedeemTokenWebServiceClient
    {
        private IConfiguration _config;
        public Uri Host { get; private set; }
        public string CertificateFileName { get; private set; }
        public string CertificatePassword { get; private set; }
        public RedeemTokenWebServiceClient(IConfiguration config)
        {
            _config = config;
            Host = new Uri(_config.GetValue<string>(Constants.REDEEMTOKENSERVICE_URL));
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            CertificateFileName = Path.Combine(dir, _config.GetValue<string>(Constants.CERTIFICATE_FILENAME));
            CertificatePassword = _config.GetValue<string>(Constants.CERTIFICATE_PASSWORD);
        }


        public RedeemTokenResponseDto CallRedeemTokenWebService(RedeemTokenRequestDto request)
        {
            RedeemTokenResponseDto response = new RedeemTokenResponseDto();
            int? statusCode = null;
            string pageContent = string.Empty;
            string soapBody = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tok=""http://www.magensa.net/Token/"" xmlns:tok1=""http://schemas.datacontract.org/2004/07/TokenWS.Core"" xmlns:sys=""http://schemas.datacontract.org/2004/07/System.Collections.Generic"">
  <soapenv:Header/>
  <soapenv:Body>
    <tok:RedeemToken>
      <tok:RedeemTokenRequest>
        <tok1:AdditionalRequestData>
          <sys:KeyValuePairOfstringstring>
            <sys:key></sys:key>
            <sys:value></sys:value>
          </sys:KeyValuePairOfstringstring>
        </tok1:AdditionalRequestData>
        <tok1:Authentication>
          <tok1:CustomerCode>{request.CustomerCode}</tok1:CustomerCode>
          <tok1:Password>{request.Password}</tok1:Password>
          <tok1:Username>{request.Username}</tok1:Username>
        </tok1:Authentication>
        <tok1:CustomerTransactionID>{request.CustomerTransactionID}</tok1:CustomerTransactionID>
        <tok1:Token>{request.Token}</tok1:Token>
      </tok:RedeemTokenRequest>
    </tok:RedeemToken>
  </soapenv:Body>
</soapenv:Envelope>
";


            try
            {
                string soapAction = "http://www.magensa.net/Token/ITokenService/RedeemToken";

                MagensaSOAPClient soapClient = new MagensaSOAPClient(host: Host, certificateFileName: CertificateFileName, certificatePassword: CertificatePassword);
                HttpWebResponse webResponse = soapClient.CallWebService(soapAction, soapBody);
                Stream responseStream = webResponse.GetResponseStream();
                using (StreamReader sr = new StreamReader(responseStream))
                {
                    response.StatusCode = (int)webResponse.StatusCode;
                    response.PageContent = sr.ReadToEnd();
                }
                responseStream.Close();
                webResponse.Close();
            }
            catch (WebException ex)
            {
                HttpStatusCode sCode = ((HttpWebResponse)ex.Response).StatusCode;
                response.StatusCode = (int)sCode;
                response.PageContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                response.StatusCode = null;
                response.PageContent = ex.Message;
            }
            return response;
        }

    }
}
