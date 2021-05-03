namespace Tokenization.Dtos
{
    public class CreateTokensRequestDto
    {
        public string CustomerCode { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string CustomerTransactionID { get; set; }
        public string MiscData { get; set; }
        public int NumberOfTokens { get; set; }
        public string TokenData { get; set; }
        public string TokenName { get; set; }
        public string ValidUntilUTC { get; set; }
    }
}
