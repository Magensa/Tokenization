#!/usr/bin/env python3.7
import requests
import os 
from typing import NamedTuple
from MagtekTokenizationWebService import pfxtopemutil

class CreateTokensRequest(NamedTuple):
    """Description Of CreateTokensRequest"""
    customerTransactionId :str
    customerCode :str
    password :str
    userName :str
    numberOfTokens :int
    tokenData :str
    tokenName :str
    validUntilUTC :str
    miscData :str
    


class CreateTokens:
    def __init__(self,createTokensRequest): 
        self.__createTokensRequest = createTokensRequest

    def CallService(self,webServiceUrl,certificateFileName,certificatePassword):
        soapRequest = f"""
        <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:tok='http://www.magensa.net/Token/' xmlns:tok1='http://schemas.datacontract.org/2004/07/TokenWS.Core' xmlns:sys='http://schemas.datacontract.org/2004/07/System.Collections.Generic'>
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
          <tok1:CustomerCode>{self.__createTokensRequest.customerCode}</tok1:CustomerCode>
          <tok1:Password>{self.__createTokensRequest.password}</tok1:Password>
          <tok1:Username>{self.__createTokensRequest.userName}</tok1:Username>
        </tok1:Authentication>
        <tok1:CustomerTransactionID>{self.__createTokensRequest.customerTransactionId}</tok1:CustomerTransactionID>
        <tok1:MiscData>{self.__createTokensRequest.miscData}</tok1:MiscData>
        <tok1:NumberOfTokens>{self.__createTokensRequest.numberOfTokens}</tok1:NumberOfTokens>
        <tok1:TokenData>{self.__createTokensRequest.tokenData}</tok1:TokenData>
        <tok1:TokenName>{self.__createTokensRequest.tokenName}</tok1:TokenName>
        <tok1:ValidUntilUTC>{self.__createTokensRequest.validUntilUTC}</tok1:ValidUntilUTC>
      </tok:CreateTokensRequest>
    </tok:CreateTokens>
  </soapenv:Body>
</soapenv:Envelope>        
        """

        headers = {
        "Content-Type": "text/xml;charset=utf-8",
        "Content-Length":  str(len(soapRequest)),
        "SOAPAction": "http://www.magensa.net/Token/ITokenService/CreateTokens"
        }
            
        response = None

        if ((certificateFileName is None) or (certificateFileName.strip() == "")):
            #send soap request without attaching certificate
            response = requests.post(webServiceUrl,data=soapRequest,headers=headers)
        else:
            util = pfxtopemutil.PfxToPemUtility()
            try:
                util.Convert(certificateFileName, certificatePassword) 
                response = requests.post(webServiceUrl, cert=util.tempFileName, data=soapRequest,headers=headers)
            except Exception as ex:
                print(ex)
            finally:
                if ((not util.tempFileName is None) and (os.path.exists(util.tempFileName))):
                    os.remove(util.tempFileName)
        return response
