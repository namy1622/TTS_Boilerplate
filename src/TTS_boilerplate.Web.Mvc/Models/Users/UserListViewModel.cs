using System.Collections.Generic;
using TTS_boilerplate.Roles.Dto;

namespace TTS_boilerplate.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
