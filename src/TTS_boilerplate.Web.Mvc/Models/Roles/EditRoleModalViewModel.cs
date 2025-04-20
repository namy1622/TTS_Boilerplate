using Abp.AutoMapper;
using TTS_boilerplate.Roles.Dto;
using TTS_boilerplate.Web.Models.Common;

namespace TTS_boilerplate.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
