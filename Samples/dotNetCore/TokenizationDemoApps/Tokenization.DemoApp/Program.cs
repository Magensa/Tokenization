using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Tokenization.ServiceFactory;
using Tokenization.UIFactory;
using Tokenization.UIFactory.Interfaces;


namespace Tokenization.DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<ITokenizationUIFactory, TokenizationUIFactory>();
            services.AddSingleton<ICreateTokenWebServiceClient, CreateTokenWebServiceClient>();
            services.AddSingleton<IRedeemTokenWebServiceClient, RedeemTokenWebServiceClient>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ITokenizationUIFactory uiFactory = serviceProvider.GetService<ITokenizationUIFactory>();

            while (true)
            {
                try
                {
                    Console.WriteLine("Please Select an option or service operation");
                    Console.WriteLine("Enter Option number (1: CreateTokens, 2: RedeemToken)");
                    var keyInfo = Console.ReadKey();
                    Console.WriteLine();

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.D1:
                            uiFactory.ShowUI(TokenizationUI.CREATETOKENS);
                            break;
                        case ConsoleKey.D2:
                            uiFactory.ShowUI(TokenizationUI.REDEEMTOKEN);
                            break;
                    }
                    bool decision = Confirm("Would you like to Continue with other Request");
                    if (decision)
                        continue;
                    else
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static bool Confirm(string title)
        {
            ConsoleKey response;
            do
            {
                Console.Write($"{ title } [y/n] ");
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            return (response == ConsoleKey.Y);
        }

    }
}
