using Abp.Authorization;
using TTS_boilerplate.Authorization.Roles;
using TTS_boilerplate.Authorization.Users;

namespace TTS_boilerplate.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
