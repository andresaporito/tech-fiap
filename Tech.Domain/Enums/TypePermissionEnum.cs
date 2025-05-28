using System.ComponentModel;

namespace Tech.Domain.Enums
{
    public enum TypePermissionEnum
    {
        [Description("Administrator")]
        Admin = 1,
        [Description("User default")]
        User = 2
    }
}
