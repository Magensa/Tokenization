# Introduction 

The repository contains a client application for Magensa's Token V2 operations

    1. Create Token
    2. Redeem Token
    3. Create StoredToken
    4. Redeem StoredToken
    5. Create PaymentToken
    6. Create InvoiceToken
    7. Create Container
    8. Get Container
    9. List Containers

 
# Clone the repository
    1. Navigate to the main page of the  **repository**
    2. Under the **repository** name, click  **Clone**
    3. Use any Git Client (e.g.: GitBash, Git Hub for windows, Source tree ) to  **clone**  the  **repository**  using HTTPS

Note: reference [Cloning a Github Repository](https://help.github.com/en/articles/cloning-a-repository)


# Getting Started

    1.  Install .net core 3.1 LTS

        - Client app requires dotnet core 3.1 LTS


    2.  Software dependencies (following NUGET packages are automatically installed when we open and run the project), please recheck and add the references from NUGET

		- Microsoft.Extensions.Configuration.Json
		- Microsoft.Extensions.DependencyInjection
		- Newtonsoft.Json


###Latest releases
- Initial release with all commits and changes as on Jan 25th 2021


# Build and Test

 From the cmd, navigate to the cloned folder and go to "TokenV2_DotNetCore/src"
    
 Run the following commands
    
 ```dotnet clean TokenV2SampleCode.sln```

 ```dotnet build TokenV2SampleCode.sln```


 Navigate from command prompt to TokenV2SampleCode.App folder and run below command

 ```dotnet run --project TokenV2SampleCode.App.csproj```

 This should open the application running in console.
