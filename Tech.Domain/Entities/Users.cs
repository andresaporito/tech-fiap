using Tech.Domain.Enums;

namespace Tech.Domain.Entities
{
    public sealed class Users : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public TypePermissionEnum Permission { get; private set; }

        public ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();

        public Users()
        {
        }

        public Users(string name, string email, string password, TypePermissionEnum permission)
        {
            Name = name;
            Email = email;
            Password = password;
            Permission = permission;
        }

        public void Update(string name, string email, string password, TypePermissionEnum permission)
        {
            Name = name;
            Email = email;
            Password = password;
            Permission = permission;
        }
    }
}
