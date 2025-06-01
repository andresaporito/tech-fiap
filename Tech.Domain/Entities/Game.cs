namespace Tech.Domain.Entities
{
    public sealed class Game : EntityBase
    {
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public string Plataforma { get; private set; }
        public DateTime DataLancamento { get; private set; }

        public ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();

        public Game()
        {
        }

        public Game(string nome, string categoria, string plataforma, DateTime dataLancamento)
        {
            Nome = nome;
            Categoria = categoria;
            Plataforma = plataforma;
            DataLancamento = dataLancamento;
        }

        public void Alterar(string nome, string categoria, string plataforma, DateTime dataLancamento)
        {
            Nome = nome;
            Categoria = categoria;
            Plataforma = plataforma;
            DataLancamento = dataLancamento;
        }
    }
}
