namespace Tech.Domain.Requests
{
    public sealed record GameRequest
    {
        public string Name { get; init; }
        public string Category { get; init; }
        public string Platform { get; init; }
        public DateTime ReleaseDate { get; init; }
    }
}
