using System.Collections.Generic;
using TTS_boilerplate.Roles.Dto;

namespace TTS_boilerplate.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
