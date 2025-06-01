namespace Tech.Domain.Entities
{
    public class UserGame
    {
        public int UserId { get; set; }
        public Users User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public DateTime AcquisitionDate { get; set; } = DateTime.UtcNow;
    }

}
