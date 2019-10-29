using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Tokenization.ServiceFactory
{
    internal class MagensaSOAPClient
    {
        public Uri Host { get; set; }
        public string CertificateFileName { get; set; }
        public string CertificatePassword { get; set; }

        public MagensaSOAPClient(Uri host, string certificateFileName = null, string certificatePassword = null)
        {
            Host = host;
            CertificateFileName = certificateFileName;
            CertificatePassword = certificatePassword;
        }

        private (bool isRequired, bool fileFound) IsRequiredCertificate()
        {
            if (string.IsNullOrEmpty(CertificateFileName))
            {
                return (false, false);
            }
            else
            {
                return File.Exists(CertificateFileName) ? (true, true) : (true, false);
            }
        }

        public HttpWebResponse CallWebService(string soapAction, string postData)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Host);
            req.PreAuthenticate = true;
            req.AllowAutoRedirect = true;

            (bool isRequired, bool fileFound) = IsRequiredCertificate();
            if ((isRequired) && (fileFound))
            {
                X509Certificate2 certificate = new X509Certificate2(CertificateFileName, CertificatePassword, X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
                ServicePointManager.CheckCertificateRevocationList = false;
                ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                ServicePointManager.Expect100Continue = true;
                req.ClientCertificates.Add(certificate);
            }

            if (isRequired && !fileFound)
            {
                throw new FileNotFoundException("Certificate File Not Found.Please Contact Your System Administrator ");
            }

            req.Method = "POST";
            req.ContentType = "text/xml;charset=utf-8";
            byte[] postDataBytes = Encoding.UTF8.GetBytes(postData);
            req.ContentLength = postDataBytes.Length;
            req.Headers.Add("SOAPAction", soapAction);
            Stream postStream = req.GetRequestStream();
            postStream.Write(postDataBytes, 0, postDataBytes.Length);
            postStream.Flush();
            postStream.Close();
            return (HttpWebResponse)req.GetResponse();
        }
    }
}
