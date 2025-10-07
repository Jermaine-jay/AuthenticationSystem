using System.ComponentModel;

namespace AuthSystem.Domain.Enum
{
    public enum UserType
    {
        [Description("User")]
        User = 1,

        [Description("Admin")]
        Admin,

        [Description("Super Admin")]
        SuperAdmin
    }
}
