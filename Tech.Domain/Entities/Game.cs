namespace Tech.Domain.Entities
{
    public sealed class Game : EntityBase
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Platform { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        public ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();

        public Game()
        {
        }

        public Game(string name, string category, string platform, DateTime releaseDate)
        {
            Name = name;
            Category = category;
            Platform = platform;
            ReleaseDate = releaseDate;
        }

        public void Update(string name, string category, string platform, DateTime releaseDate)
        {
            Name = name;
            Category = category;
            Platform = platform;
            ReleaseDate = releaseDate;
        }
    }
}
