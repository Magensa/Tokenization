#!/usr/bin/env python3.7
import OpenSSL.crypto
import tempfile

class PfxToPemUtility:


    def Convert(self,pfx_path, pfx_password):
        self.tempFileName = None
        try:
            t_pem = tempfile.NamedTemporaryFile(suffix='.pem', delete=False) 
            self.tempFileName = t_pem.name
            pfx = open(pfx_path, 'rb').read()
            p12 = OpenSSL.crypto.load_pkcs12(pfx, pfx_password)
            t_pem.write(OpenSSL.crypto.dump_privatekey(OpenSSL.crypto.FILETYPE_PEM,p12.get_privatekey()))
            t_pem.write(OpenSSL.crypto.dump_certificate(OpenSSL.crypto.FILETYPE_PEM,p12.get_certificate()))
            ca = p12.get_ca_certificates()
        
            if ca is not None:
                for cert in ca:
                    t_pem.write(OpenSSL.crypto.dump_certificate(OpenSSL.crypto.FILETYPE_PEM,cert))
        
            t_pem.close()
        except:
            raise
