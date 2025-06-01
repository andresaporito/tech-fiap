namespace Tech.Domain.Responses
{
    public class GameResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;

        public static GameResponse FromEntity(Entities.Game game)
        {
            return new GameResponse
            {
                Id = game.Id,
                Name = game.Name,
                Category = game.Category,
                Platform = game.Platform,
                ReleaseDate = game.ReleaseDate.ToString("yyyy-MM-dd")
            };
        }
    }
}
