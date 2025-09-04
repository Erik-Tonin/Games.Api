namespace FIAP.Games.Domain.Entities
{
    public class JwtSettings
    {
        public string ChaveSecreta { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
    }
}
