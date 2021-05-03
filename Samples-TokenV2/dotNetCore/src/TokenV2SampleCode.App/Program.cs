using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TokenV2SampleCode.UIHandler;

namespace TokenV2SampleCode.App
{
    class Program
    {
        private static AuthenticationHeaderValue tokenV2AuthHeader;
        private static readonly HttpClient httpClient = new HttpClient();

        private static IConfigurationRoot config;
        private static ITokenV2SampleCodeUIHandler tokenV2SampleCodeUIHandler;

        private static readonly Tuple<int, string, string, string>[] operations =
            {
                new Tuple<int, string, string, string>(0, "Set/Reset Credential", null, null),
                new Tuple<int, string, string, string>(1, "Create Token", "TOKENv2_CREATE_URL_PREFIX", "/api/Token/create"),
                new Tuple<int, string, string, string>(2, "Redeem Token", "TOKENv2_REDEEM_URL_PREFIX", "/api/Token/redeem"),
                new Tuple<int, string, string, string>(3, "Create StoredToken", "TOKENv2_CREATE_URL_PREFIX", "/api/StoredToken/create"),
                new Tuple<int, string, string, string>(4, "Redeem StoredToken", "TOKENv2_REDEEM_URL_PREFIX", "/api/StoredToken/redeem"),
                new Tuple<int, string, string, string>(5, "Create PaymentToken", "TOKENv2_CREATE_URL_PREFIX", "/api/PaymentToken/create"),
                new Tuple<int, string, string, string>(6, "Create InvoiceToken", "TOKENv2_CREATE_URL_PREFIX", "/api/InvoiceToken/create"),
                new Tuple<int, string, string, string>(7, "Create Container", "TOKENv2_CREATE_URL_PREFIX", "/api/Container/create"),
                new Tuple<int, string, string, string>(8, "Get Container", "TOKENv2_CREATE_URL_PREFIX", "/api/Container/get"),
                new Tuple<int, string, string, string>(9, "List Containers", "TOKENv2_CREATE_URL_PREFIX", "/api/Container/list"),
            };


        static async Task Main(string[] args)
        {
            config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();


            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<ITokenV2SampleCodeUIHandler, TokenV2SampleCodeUIHandler>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            tokenV2SampleCodeUIHandler = serviceProvider.GetService<ITokenV2SampleCodeUIHandler>();

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            while (true)
            {
                try
                {
                    var option = tokenV2SampleCodeUIHandler.SelectOption(operations);

                    switch (option)
                    {
                        case 0:
                            tokenV2AuthHeader = tokenV2SampleCodeUIHandler.SetCredential();
                            continue;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            await MakeServiceCall(option);
                            break;
                        default:
                            Console.WriteLine(" <== Not an option!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine("Unexpected error occurred with exception details:");
                    var exception = e;
                    var indent = "  ";
                    while (null != exception)
                    {
                        Console.WriteLine(indent + exception.Message);
                        exception = exception.InnerException;
                        indent += "  ";
                    }
                }
                if (tokenV2SampleCodeUIHandler.Confirm("Would you like to continue? "))
                    continue;
                else
                    break;
            }
        }

        static async Task MakeServiceCall(int Option)
        {
            tokenV2AuthHeader ??= tokenV2SampleCodeUIHandler.SetCredential();
            httpClient.DefaultRequestHeaders.Authorization = tokenV2AuthHeader;

            var urlEndpoint = config[operations[Option].Item3];
            var urlResource = operations[Option].Item4;
            var url = urlEndpoint + urlResource;

            var requestString = tokenV2SampleCodeUIHandler.BuildRequest(Option);


            // Service call
            var response = await httpClient.PostAsync(url, new StringContent(requestString, Encoding.UTF8, "application/json"));


            Console.WriteLine();
            Console.WriteLine("--- REQUEST ---");
            Console.WriteLine("{0} {1} HTTP/{2}", response?.RequestMessage?.Method, response?.RequestMessage?.RequestUri, response?.RequestMessage?.Version);
            foreach (var header in response?.RequestMessage?.Headers)
                Console.WriteLine("{0}: {1}", header.Key, string.Join(',', header.Value));
            Console.WriteLine();
            Console.WriteLine(requestString);
            Console.WriteLine("--- RESPONSE ---");
            Console.WriteLine("HTTP/{0} {1} {2}", response?.Version, (int)response?.StatusCode, response?.ReasonPhrase);
            foreach (var header in response?.Headers)
                Console.WriteLine("{0}: {1}", header.Key, string.Join(',', header.Value));
            Console.WriteLine();
            if (null != response?.Content)
            {
                var responseString = await response?.Content?.ReadAsStringAsync();
                if (false == string.IsNullOrWhiteSpace(responseString))
                    Console.WriteLine(TryFormatJson(responseString));
            }
            Console.WriteLine("----------------");
        }

        private static string TryFormatJson(string json)
        {
            try
            {
                dynamic parsedJson = JsonConvert.DeserializeObject(json);
                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            }
            catch (Exception)
            {
                return json;
            }
        }
    }
}