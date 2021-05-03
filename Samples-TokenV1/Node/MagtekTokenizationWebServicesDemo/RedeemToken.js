const fs = require("fs");
const https = require("https");
const axios = require("axios");

class RedeemToken {
    constructor(customerTransactionId, customerCode, userName, passWord, token) {
        this.customerTransactionId = customerTransactionId;
        this.customerCode = customerCode;
        this.userName = userName;
        this.passWord = passWord;
        this.token = token;
    }

    async CallWebService(webServiceUrl, certificateFilePath=null, certificatePassword=null) {

        try {

            let xml = `<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tok="http://www.magensa.net/Token/" xmlns:tok1="http://schemas.datacontract.org/2004/07/TokenWS.Core" xmlns:sys="http://schemas.datacontract.org/2004/07/System.Collections.Generic">
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
          <tok1:CustomerCode>${this.customerCode}</tok1:CustomerCode>
          <tok1:Password>${this.passWord}</tok1:Password>
          <tok1:Username>${this.userName}</tok1:Username>
        </tok1:Authentication>
        <tok1:CustomerTransactionID>${this.customerTransactionId}</tok1:CustomerTransactionID>
        <tok1:Token>${this.token}</tok1:Token>
      </tok:RedeemTokenRequest>
    </tok:RedeemToken>
  </soapenv:Body>
</soapenv:Envelope>`;

            let agent = null;

            if (certificateFilePath) {
                agent = new https.Agent({
                    rejectUnauthorized: false,
                    strictSSL: false,
                    pfx: fs.readFileSync(certificateFilePath),
                    passphrase: certificatePassword
                });
            }

            let response = await axios({
                url: webServiceUrl,
                method: "post",
                httpsAgent: agent,
                data: xml,
                headers: {
                    "Content-Type": "text/xml;charset=utf-8",
                    "Content-Length": xml.length,
                    "SOAPAction": "http://www.magensa.net/Token/ITokenService/RedeemToken"
                }
            });

            return { statuscode: response.status, data: response.data };
        }
        catch (error) {
            if (error.response) {
                //The request was made and the server responded with a
                //status code that falls out of the range of 2xx
                return { statuscode: error.response.status, data: error.response.data };
            } else {
                //Something happened in setting up the request and triggered an Error
                throw error;
            }
        }
    }
}

module.exports = RedeemToken;












