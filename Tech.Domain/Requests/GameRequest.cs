namespace Tech.Domain.Requests
{
    public sealed record GameRequest
    {
        public string Nome { get; init; }
        public string Categoria { get; init; }
        public string Plataforma { get; init; }
        public DateTime DataLancamento { get; init; }
    }
}
