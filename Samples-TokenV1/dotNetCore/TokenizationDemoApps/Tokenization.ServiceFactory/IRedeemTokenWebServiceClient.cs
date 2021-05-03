using Tokenization.Dtos;

namespace Tokenization.ServiceFactory
{
    public interface IRedeemTokenWebServiceClient
    {
        RedeemTokenResponseDto CallRedeemTokenWebService(RedeemTokenRequestDto request);
    }
}
