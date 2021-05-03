# Introduction 

The Repository Contains Demo Application For tokenization web service operations includes the below operations

    1. CreateTokens 
    2. RedeemToken
    
# Clone the repository
 1. Navigate to the main page of the  **repository**. 
 2. Under the  **repository**  name, click  **Clone** .
 3. Use any Git Client(eg.: GitBash, Git Hub for windows, Source tree ) to  **clone**  the  **repository**  using HTTPS.

Note: reference for  [Cloning a Github Repository](https://help.github.com/en/articles/cloning-a-repository)


# Getting Started

1.  Install .net core 2.2

    - Demo app requires dotnet core 2.2 is installed

2.  Software dependencies( The Following nuget packages are automatically installed when we open and run the project), please recheck and add the references from nuget
 

     Microsoft.Extensions.DependencyInjection

     Microsoft.Extensions.Configuration

     Microsoft.Extensions.Configuration.EnvironmentVariables

     Microsoft.Extensions.Configuration.Json
     
     Microsoft.Extensions.Configuration.Binder

***Note*** : RedeemToken operation in tokenization service is dependent on SSL Certificate.Test certificate is available in cloned folder.
    
3.	Latest releases

    - Initial release with all commits and changes as on October 18th 2019

# Build and Test

 Steps to Build and run Tokenization.DemoApp project ( .net core 2.2)

 From the cmd,  Navigate to the cloned folder and go to TokenizationDemoApps
    
 Run the following commands
    
 ```dotnet clean TokenizationDemoApps.sln```

 ```dotnet build TokenizationDemoApps.sln```

 Navigate from command prompt to Tokenization.DemoApp folder containing Tokenization.DemoApp.csproj and run below command

 ```dotnet run --project Tokenization.DemoApp.csproj```

 This should open the application running in console.

