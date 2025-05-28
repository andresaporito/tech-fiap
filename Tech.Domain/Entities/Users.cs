using Tech.Domain.Enums;

namespace Tech.Domain.Entities
{
    public sealed class Users : EntityBase
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public TypePermissionEnum Permission { get; private set; }

        public Users()
        {

        }
        public Users(string name, string password, string email, TypePermissionEnum permission)
        {
            Name = name;
            Password = password;
            Email = email;
            Permission = permission;
        }

        public Users Update(string name, string password, string email, TypePermissionEnum permission)
        {
            Name = name;
            Password = password;
            Email = email;
            Permission = permission;

            return this;
        }
    }
}
