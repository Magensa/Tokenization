using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TokenV2SampleCode.DTOs;

namespace TokenV2SampleCode.UIHandler
{
    public class TokenV2SampleCodeUIHandler : ITokenV2SampleCodeUIHandler
    {
        private static Tuple<int, string, string, string>[] operations;

        public int SelectOption(Tuple<int, string, string, string>[] Operations)
        {
            operations = Operations;

            Console.Clear();
            Console.WriteLine();
            Array.ForEach(operations, t => { Console.WriteLine(string.Format("  {0}.  {1}", t.Item1, t.Item2)); });
            Console.WriteLine();
            Console.Write("Select an option: ");
            var optionString = Console.ReadLine();
            int option = -1;
            if (Int32.TryParse(optionString, out option))
            {
                Console.WriteLine();
                Console.WriteLine("* => Required");
                return option;
            }
            return this.SelectOption(operations);
        }
        
        public AuthenticationHeaderValue SetCredential()
        {
            Console.WriteLine();
            AuthenticationHeaderValue tokenV2AuthHeader = null;
            while (null == tokenV2AuthHeader)
            {
                Console.Write("CustomerCode *: ");
                var customerCode = Console.ReadLine();
                Console.Write("Username *: ");
                var username = Console.ReadLine();
                Console.Write("Password *: ");
                var pwd = Console.ReadLine();
                if (false == string.IsNullOrWhiteSpace(customerCode) &&
                    false == string.IsNullOrWhiteSpace(username) &&
                    false == string.IsNullOrWhiteSpace(pwd))
                {
                    if (AuthenticationHeaderValue.TryParse("Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}/{1}:{2}", customerCode, username, pwd))), out AuthenticationHeaderValue authHeaderValue))
                        tokenV2AuthHeader = authHeaderValue;
                }

                if (null == tokenV2AuthHeader)
                    Console.WriteLine("Incorrect credential! Please re-enter credential.");
            }
            return tokenV2AuthHeader;
        }
        
        public string BuildRequest(int Option)
        {
            string requestString = null;
            Console.WriteLine();
            switch (Option)
            {
                case 1:
                    requestString = BuildCreateTokenRequest();
                    break;
                case 2:
                    requestString = BuildRedeemTokenRequest();
                    break;
                case 3:
                    requestString = BuildCreateStoredTokenRequest();
                    break;
                case 4:
                    requestString = BuildRedeemStoredTokenRequest();
                    break;
                case 5:
                    requestString = BuildCreatePaymentTokenRequest();
                    break;
                case 6:
                    requestString = BuildCreateInvoiceTokenRequest();
                    break;
                case 7:
                    requestString = BuildCreateContainerRequest();
                    break;
                case 8:
                    requestString = BuildGetContainerRequest();
                    break;
                case 9:
                    requestString = BuildListContainerRequest();
                    break;
                default:
                    break;
            }
            return requestString;
        }
        public bool Confirm(string Question)
        {
            ConsoleKey response;
            do
            {
                Console.WriteLine();
                Console.Write($"{ Question } [Y/N]: ");
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            return (response == ConsoleKey.Y);
        }


        private string BuildCreateTokenRequest()
        {
            string requestString = null;
            try
            {
                var request = new CreateTokenRequest();
                Console.WriteLine("For TokenDataInput, use single or double quotes without escape chars, like\n{\"TokenDataTypeID\": \"1\",\"PlainText\": \"Token Data\"}");
                request.TokenDataInput = Read_Mandatory_String_Input("TokenDataInput");
                request.ValidUntilUTC = Read_Mandatory_DateTime_Input("ValidUntilUTC");

                request.TokenName = Read_Optional_String_Input("TokenName");
                request.MiscData = Read_Optional_String_Input("MiscData");
                request.CustomerTranRef = Read_Optional_String_Input("CustomerTranRef");

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building CreateTokenRequest: " + e.Message);
            }
            return requestString;
        }
        private string BuildRedeemTokenRequest()
        {
            string requestString = null;
            try
            {
                var request = new RedeemTokenRequest
                {
                    Token = Read_Mandatory_String_Input("Token"),

                    CustomerTranRef = Read_Optional_String_Input("CustomerTranRef")
                };

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building RedeemTokenRequest: " + e.Message);
            }
            return requestString;
        }
        private string BuildCreateStoredTokenRequest()
        {
            string requestString = null;
            try
            {
                var request = new CreateStoredTokenRequest();
                Console.WriteLine("For TokenDataInput, use single or double quotes without escape chars, like\n{\"TokenDataTypeID\": \"1\",\"PlainText\": \"Token Data\"}");
                request.TokenDataInput = Read_Mandatory_String_Input("TokenDataInput");
                request.ValidUntilUTC = Read_Mandatory_DateTime_Input("ValidUntilUTC");
                request.ContainerName = Read_Mandatory_String_Input("ContainerName");

                request.TokenKey = Read_Optional_String_Input("TokenKey");
                request.TokenName = Read_Optional_String_Input("TokenName");
                request.MiscData = Read_Optional_String_Input("MiscData");
                request.CustomerTranRef = Read_Optional_String_Input("CustomerTranRef");

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building CreateStoredTokenRequest: " + e.Message);
            }
            return requestString;
        }
        private string BuildRedeemStoredTokenRequest()
        {
            string requestString = null;
            try
            {
                var request = new RedeemStoredTokenRequest
                {
                    ContainerName = Read_Mandatory_String_Input("ContainerName"),
                    TokenKey = Read_Mandatory_String_Input("TokenKey"),

                    CustomerTranRef = Read_Optional_String_Input("CustomerTranRef")
                };

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building RedeemStoredTokenRequest: " + e.Message);
            }
            return requestString;
        }
        private string BuildCreatePaymentTokenRequest()
        {
            string requestString = null;
            try
            {
                var request = new CreatePaymentTokenRequest
                {
                    PAN = Read_Mandatory_String_Input("PAN"),
                    ExpirationDate = Read_Mandatory_String_Input("ExpirationDate"),
                    ValidUntilUTC = Read_Mandatory_DateTime_Input("ValidUntilUTC"),

                    BillingZIP = Read_Optional_String_Input("BillingZip"),
                    CVV = Read_Optional_String_Input("CVV"),
                    TokenName = Read_Optional_String_Input("TokenName"),
                    MiscData = Read_Optional_String_Input("MiscData"),
                    TransactionSourceID = Read_Optional_String_Input("TransactionSourceID"),
                    CustomerTranRef = Read_Optional_String_Input("CustomerTranRef")
                };

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building CreatePaymentTokenRequest: " + e.Message);
            }
            return requestString;
        }
        private string BuildCreateInvoiceTokenRequest()
        {
            string requestString = null;
            try
            {
                var request = new CreateInvoiceTokenRequest
                {
                    CompanyName = Read_Mandatory_String_Input("CompanyName"),
                    Amount = Read_Mandatory_String_Input("Amount"),
                    ValidUntilUTC = Read_Mandatory_DateTime_Input("ValidUntilUTC"),

                    TaxAmount = Read_Optional_String_Input("TaxAmount"),
                    TaxPercent = Read_Optional_String_Input("TaxPercent"),
                    TransactionReferenceNumber = Read_Optional_String_Input("TransactionReferenceNumber"),
                    ProcessorName = Read_Optional_String_Input("ProcessorName"),
                    AdditionalInfo = Read_KVPs_Input("AdditionalInfo"),

                    LogoURL = Read_Optional_String_Input("LogoURL"),
                    PrimaryColor = Read_Optional_String_Input("PrimaryColor"),
                    SecondaryColor = Read_Optional_String_Input("SecondaryColor"),
                    CompanyNameFontColor = Read_Optional_String_Input("CompanyNameFontColor"),
                    TermsAndTransactionInfo = Read_Optional_String_Input("TermsAndTransactionInfo "),

                    TokenName = Read_Optional_String_Input("TokenName"),
                    MiscData = Read_Optional_String_Input("MiscData"),
                    TransactionSourceID = Read_Optional_String_Input("TransactionSourceID"),
                    CustomerTranRef = Read_Optional_String_Input("CustomerTranRef")
                };

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building CreateInvoiceTokenRequest: " + e.Message);
            }
            return requestString;
        }
        private string BuildCreateContainerRequest()
        {
            string requestString = null;
            try
            {
                var request = new CreateContainerRequest
                {
                    ContainerName = Read_Mandatory_String_Input("ContainerName"),
                    TokenKeyTypeID = Read_Mandatory_Int_Input("TokenKeyTypeID"),

                    AutoGenKeyMask = Read_Optional_String_Input("AutoGenKeyMask"),
                    AutoGenKeyPlaceholderTypeID = Read_Optional_Int_Input("AutoGenKeyPlaceholderTypeID"),
                    AutoGenKeyPlaceholderChar = Read_Optional_String_Input("AutoGenKeyPlaceholderChar"),
                    AutoGenKeyPadChar = Read_Optional_String_Input("AutoGenKeyPadChar"),
                    UserGenKeyRegex = Read_Optional_String_Input("UserGenKeyRegex"),
                    CustomerTranRef = Read_Optional_String_Input("CustomerTranRef")
                };

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building CreateContainerRequest: " + e.Message);
            }
            return requestString;
        }
        private string BuildGetContainerRequest()
        {
            string requestString = null;
            try
            {
                var request = new GetContainerRequest
                {
                    ContainerName = Read_Mandatory_String_Input("ContainerName"),

                    CustomerTranRef = Read_Optional_String_Input("CustomerTranRef")
                };

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building GetContainerRequest: " + e.Message);
            }
            return requestString;
        }
        private string BuildListContainerRequest()
        {
            string requestString = null;
            try
            {
                var request = new ListContainersRequest
                {
                    CustomerTranRef = Read_Optional_String_Input("CustomerTranRef")
                };

                requestString = JsonConvert.SerializeObject(request, Formatting.Indented);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred when building ListContainersRequest: " + e.Message);
            }
            return requestString;
        }

        private static string Read_Mandatory_String_Input(string question)
        {
            Console.Write($"{question} *: ");
            var ans = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ans))
            {
                return Read_Mandatory_String_Input(question);
            }
            return ans;
        }
        private static string Read_Optional_String_Input(string question)
        {
            Console.Write($"{question}: ");
            var ans = Console.ReadLine();
            return ans;
        }
        private static DateTime Read_Mandatory_DateTime_Input(string question)
        {
            Console.Write($"{question} *: ");
            var ans = Console.ReadLine();
            if (false == DateTime.TryParse(ans, out DateTime dt))
            {
                return Read_Mandatory_DateTime_Input(question);
            }
            return dt;
        }
        private static KeyValuePair<string, string>[] Read_KVPs_Input(string question)
        {
            var noOfKeys = Read_Mandatory_Int_Input($"Enter # of key-value pairs for {question}: ");
            var result = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < noOfKeys; i++)
            {
                var key = Read_Optional_String_Input("key");
                var val = Read_Optional_String_Input("value");
                result.Add(new KeyValuePair<string, string>(key, val));
            }
            return result.ToArray();
        }
        private static int Read_Mandatory_Int_Input(string question)
        {
            Console.Write($"{question} *: ");
            var ans = Console.ReadLine();
            if (false == Int32.TryParse(ans, out int i))
            {
                return Read_Mandatory_Int_Input(question);
            }
            return i;
        }
        private static int Read_Optional_Int_Input(string question)
        {
            Console.Write($"{question}: ");
            var ans = Console.ReadLine();
            Int32.TryParse(ans, out int i);
            return i;
        }
    }
}