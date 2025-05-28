using Tech.Domain.Enums;

namespace Tech.Domain.Requests
{
    public sealed record UsersRequest
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public TypePermissionEnum Permission { get; set; }

    }
}
