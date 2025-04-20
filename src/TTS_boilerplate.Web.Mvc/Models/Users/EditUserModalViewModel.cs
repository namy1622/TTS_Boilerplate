using System.Collections.Generic;
using System.Linq;
using TTS_boilerplate.Roles.Dto;
using TTS_boilerplate.Users.Dto;

namespace TTS_boilerplate.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
