namespace Tech.Domain.Responses
{
    public class GameResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Plataforma { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
