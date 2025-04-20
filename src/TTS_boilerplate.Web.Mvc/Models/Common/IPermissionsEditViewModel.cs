using System.Collections.Generic;
using TTS_boilerplate.Roles.Dto;

namespace TTS_boilerplate.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}