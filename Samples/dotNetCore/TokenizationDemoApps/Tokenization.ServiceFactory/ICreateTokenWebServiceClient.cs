using Tokenization.Dtos;

namespace Tokenization.ServiceFactory
{
    public interface ICreateTokenWebServiceClient
    {
        CreateTokensResponseDto CallTokenWebService(CreateTokensRequestDto request);
    }
}
