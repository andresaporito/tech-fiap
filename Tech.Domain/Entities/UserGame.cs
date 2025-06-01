namespace Tech.Domain.Entities
{
    public sealed class UserGame
    {
        public int UserId { get; private set; }
        public Users User { get; private set; }

        public int GameId { get; private set; }
        public Game Game { get; private set; }

        public DateTime AcquisitionDate { get; private set; } = DateTime.UtcNow;

        public UserGame(int userId, int gameId)
        {
            UserId = userId;
            GameId = gameId;
            AcquisitionDate = DateTime.UtcNow;
        }

        private UserGame() { }
    }
}
