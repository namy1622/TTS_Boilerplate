using System.ComponentModel.DataAnnotations;

namespace TTS_boilerplate.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}