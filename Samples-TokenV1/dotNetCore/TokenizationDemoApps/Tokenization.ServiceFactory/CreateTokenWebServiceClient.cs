using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using Tokenization.Dtos;

namespace Tokenization.ServiceFactory
{
    public class CreateTokenWebServiceClient : ICreateTokenWebServiceClient
    {
        private IConfiguration _config;
        public Uri Host { get; private set; }
        public string CertificateFileName { get; private set; }
        public string CertificatePassword { get; private set; }

        public CreateTokenWebServiceClient(IConfiguration config)
        {
            _config = config;
            Host = new Uri(_config.GetValue<string>(Constants.CREATETOKENSERVICE_URL));
            CertificateFileName = null;
            CertificatePassword = null;
        }

        public CreateTokensResponseDto CallTokenWebService(CreateTokensRequestDto request)
        {
            CreateTokensResponseDto response = new CreateTokensResponseDto();
            int? statusCode = null;
            string pageContent = string.Empty;

            try
            {
                string soapAction = "http://www.magensa.net/Token/ITokenService/CreateTokens";
                string soapBody = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tok=""http://www.magensa.net/Token/"" xmlns:tok1=""http://schemas.datacontract.org/2004/07/TokenWS.Core"" xmlns:sys=""http://schemas.datacontract.org/2004/07/System.Collections.Generic"">
  <soapenv:Header/>
  <soapenv:Body>
    <tok:CreateTokens>
      <tok:CreateTokensRequest>
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
        <tok1:MiscData>{request.MiscData}</tok1:MiscData>
        <tok1:NumberOfTokens>{request.NumberOfTokens}</tok1:NumberOfTokens>
        <tok1:TokenData>{request.TokenData}</tok1:TokenData>
        <tok1:TokenName>{request.TokenName}</tok1:TokenName>
        <tok1:ValidUntilUTC>{request.ValidUntilUTC}</tok1:ValidUntilUTC>
      </tok:CreateTokensRequest>
    </tok:CreateTokens>
  </soapenv:Body>
</soapenv:Envelope>";


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
