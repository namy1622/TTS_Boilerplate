using Abp.Application.Services.Dto;

namespace TTS_boilerplate.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

