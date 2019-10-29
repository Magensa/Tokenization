namespace Tokenization.Dtos
{
    public class RedeemTokenRequestDto
    {
        public string CustomerCode { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string CustomerTransactionID { get; set; }
        public string Token { get; set; }
    }
}
