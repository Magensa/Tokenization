using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Tokenization.Dtos;
using Tokenization.ServiceFactory;
using Tokenization.UIFactory.Interfaces;

namespace Tokenization.UIFactory
{
    public class TokenizationUIFactory : ITokenizationUIFactory
    {
        IServiceProvider _serviceProvider;

        public TokenizationUIFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void ShowUI(TokenizationUI tokenizationUI)
        {
            if (tokenizationUI == TokenizationUI.CREATETOKENS)
            {
                ShowCreateTokensUI();
            }
            else if (tokenizationUI == TokenizationUI.REDEEMTOKEN)
            {
                ShowRedeemTokenUI();
            }
        }
        private void ShowCreateTokensUI()
        {
            CreateTokensRequestDto createTokenRequest = new CreateTokensRequestDto();
            try
            {
                Console.WriteLine("=====================Request building start======================");
                createTokenRequest.CustomerTransactionID = Read_String_Input("Please Enter Customer Transaction ID:", true);
                createTokenRequest.CustomerCode = Read_String_Input("Please enter the CustomerCode:", false);
                createTokenRequest.Username = Read_String_Input("Please enter the Username:", false);
                createTokenRequest.Password = Read_String_Input("Please enter the Password:", false);
                createTokenRequest.NumberOfTokens = Read_Int_Input("Please enter the Number of tokens:");
                createTokenRequest.TokenData = Read_LongString_Input("Please enter the TokenData:", true);
                createTokenRequest.TokenName = Read_String_Input("Please enter the TokenName:", true);
                createTokenRequest.ValidUntilUTC = Read_ValidUtc_Date("Enter ValidUntilUTC:", "Year(Ex:2025):-", "Month(Between 1-12)Ex:For 1 Enter 01 :-", "Day(Between 1-31)Ex: For 1 Enter 01:-", "Hour(Between 0-23)Ex: For 1 Enter 01:-", "Minute(Between 0-59)Ex: 1 Enter 01:-", "Seconds(Between 0-59)Ex:For 1 Enter 01:-");
                createTokenRequest.MiscData = Read_String_Input("Please Enter MiscData:", true);
                Console.WriteLine("=====================Request building End======================");
                var CreateTokensSvc = _serviceProvider.GetService<ICreateTokenWebServiceClient>();
                var tokenizationServiceResponseDto = CreateTokensSvc.CallTokenWebService(createTokenRequest);
                if (tokenizationServiceResponseDto != null)
                {
                    var response = tokenizationServiceResponseDto as CreateTokensResponseDto;
                    Console.WriteLine("=====================Response Start======================");
                    Console.WriteLine("Response:");
                    Console.Write(PrettyXml(response.PageContent) + "\n");
                    Console.WriteLine("=====================Response End======================");
                }
                else
                {
                    Console.WriteLine("Response is null, Please check with input values given and try again");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while CreateTokens : " + ex.Message.ToString());
            }
        }
        private void ShowRedeemTokenUI()
        {
            RedeemTokenRequestDto redeemTokenRequestDto = new RedeemTokenRequestDto();
            try
            {
                Console.WriteLine("=====================Request building start======================");
                redeemTokenRequestDto.CustomerTransactionID = Read_String_Input("Please Enter Customer Transaction ID:", true);
                redeemTokenRequestDto.CustomerCode = Read_String_Input("Please enter the CustomerCode:", false);
                redeemTokenRequestDto.Password = Read_String_Input("Please enter the Password:", false);
                redeemTokenRequestDto.Username = Read_String_Input("Please enter the Username:", false);
                redeemTokenRequestDto.Token = Read_LongString_Input("Please enter the TOKEN:", false);
                Console.WriteLine("=====================Request building End======================");

                var RedeemTokenSvc = _serviceProvider.GetService<IRedeemTokenWebServiceClient>();
                var tokenizationServiceResponseDto = RedeemTokenSvc.CallRedeemTokenWebService(redeemTokenRequestDto);
                if (tokenizationServiceResponseDto != null)
                {
                    var response = tokenizationServiceResponseDto as RedeemTokenResponseDto;
                    Console.WriteLine("=====================Response Start======================");
                    Console.WriteLine("Response:");
                    Console.Write(PrettyXml(response.PageContent) + "\n");
                    Console.WriteLine("=====================Response End======================");
                }
                else
                {
                    Console.WriteLine("Response is null, Please check with input values given and try again");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while RedeemToken : " + ex.Message.ToString());
            }
        }
        private static string Read_String_Input(string userMessage, bool isOptional)
        {
            Console.WriteLine(userMessage);
            var userInputVal = Console.ReadLine();
            if ((!isOptional) && string.IsNullOrWhiteSpace(userInputVal))
            {
                return Read_String_Input(userMessage, isOptional);
            }
            return userInputVal;
        }
        /// <summary>
        /// methods reads long input string. .net has a limitation for reading long strings from console
        /// </summary>
        /// <returns></returns>
        private static string Read_LongString_Input(string userMessage, bool isOptional)
        {
            Console.WriteLine(userMessage);
            byte[] inputBuffer = new byte[262144];
            Stream inputStream = Console.OpenStandardInput(262144);
            Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, inputBuffer.Length));
            string strInput = Console.ReadLine();
            if ((!isOptional) && string.IsNullOrWhiteSpace(strInput))
            {
                return Read_LongString_Input(userMessage, isOptional);
            }
            return strInput;
        }
        private static int Read_Int_Input(string userMessage)
        {
            Console.WriteLine(userMessage);
            var userInputVal = Console.ReadLine();
            if ((!string.IsNullOrWhiteSpace(userInputVal)) && (!userInputVal.All(char.IsDigit)))
            {
                Console.WriteLine("Invalid Input.");
                return Read_Int_Input(userMessage);
            }
            return int.Parse(userInputVal);
        }
        private static string Read_Year_Input(string userMessage)
        {
            Console.WriteLine(userMessage);
            var userInputVal = Console.ReadLine();
            if ((!string.IsNullOrWhiteSpace(userInputVal)) && (userInputVal.Length == 4) && userInputVal.All(char.IsDigit))
            {
                return userInputVal;
            }
            Console.WriteLine("Invalid Input.");
            return Read_Year_Input(userMessage);
        }
        public static bool Between(int num, int lower, int upper, bool inclusive = true)
        {
            return inclusive ? lower <= num && num <= upper : lower < num && num < upper;
        }
        private static string Read_Month_Input(string userMessage)
        {
            Console.WriteLine(userMessage);
            var userInputVal = Console.ReadLine();
            if ((!string.IsNullOrWhiteSpace(userInputVal)) && (userInputVal.Length == 2) && userInputVal.All(char.IsDigit) && Between(int.Parse(userInputVal), 1, 12))
            {
                return userInputVal;
            }
            Console.WriteLine("Invalid Input.");
            return Read_Month_Input(userMessage);
        }
        private static string Read_Day_Input(string userMessage)
        {
            Console.WriteLine(userMessage);
            var userInputVal = Console.ReadLine();
            if ((!string.IsNullOrWhiteSpace(userInputVal)) && (userInputVal.Length == 2) && userInputVal.All(char.IsDigit) && Between(int.Parse(userInputVal), 1, 31))
            {
                return userInputVal;
            }
            Console.WriteLine("Invalid Input.");
            return Read_Day_Input(userMessage);
        }
        private static string Read_Hour_Input(string userMessage)
        {
            Console.WriteLine(userMessage);
            var userInputVal = Console.ReadLine();
            if ((!string.IsNullOrWhiteSpace(userInputVal)) && (userInputVal.Length == 2) && userInputVal.All(char.IsDigit) && Between(int.Parse(userInputVal), 0, 23))
            {
                return userInputVal;
            }
            Console.WriteLine("Invalid Input.");
            return Read_Hour_Input(userMessage);

        }
        private static string Read_Minute_Input(string userMessage)
        {
            Console.WriteLine(userMessage);
            var userInputVal = Console.ReadLine();
            if ((!string.IsNullOrWhiteSpace(userInputVal)) && (userInputVal.Length == 2) && userInputVal.All(char.IsDigit) && Between(int.Parse(userInputVal), 0, 59))
            {
                return userInputVal;
            }
            Console.WriteLine("Invalid Input.");
            return Read_Minute_Input(userMessage);
        }
        private static string Read_Second_Input(string userMessage)
        {
            Console.WriteLine(userMessage);
            var userInputVal = Console.ReadLine();
            if ((!string.IsNullOrWhiteSpace(userInputVal)) && (userInputVal.Length == 2) && userInputVal.All(char.IsDigit) && Between(int.Parse(userInputVal), 0, 59))
            {
                return userInputVal;
            }
            Console.WriteLine("Invalid Input.");
            return Read_Second_Input(userMessage);
        }
        private static string Read_ValidUtc_Date(string userMessage, string yearlabel, string monthlabel, string daylabel, string hourlabel, string minutelabel, string secondslabel)
        {
            Console.WriteLine(userMessage);
            var year = Read_Year_Input(yearlabel);
            var month = Read_Month_Input(monthlabel);
            var day = Read_Day_Input(daylabel);
            var hour = Read_Hour_Input(hourlabel);
            var minute = Read_Minute_Input(minutelabel);
            var second = Read_Second_Input(secondslabel);

            return $"{year}-{month}-{day}T{hour}:{minute}:{second}Z";
        }
        public static bool IsValidXml(string xml)
        {
            try
            {
                XDocument.Parse(xml);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string PrettyXml(string xml)
        {
            if (IsValidXml(xml)) //print xml in beautiful format
            {
                var stringBuilder = new StringBuilder();
                var element = XElement.Parse(xml);
                var settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;
                settings.NewLineOnAttributes = true;
                using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                {
                    element.Save(xmlWriter);
                }
                return stringBuilder.ToString();
            }
            else
            {
                return xml;
            }
        }
    }
}

