namespace Tech.Domain.Requests
{
    public sealed record TokenRequest
    {
        public string Name { get; init; }
        public string Password { get; init; }

    }
}
