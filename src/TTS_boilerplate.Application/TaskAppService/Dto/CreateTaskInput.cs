using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TTS_boilerplate.Models;
//using System.Threading.Tasks;

namespace TTS_boilerplate.TaskAppService.Dto
{
    [AutoMapTo(typeof(Task))]
    public class CreateTaskInput
    {
        [Required]
        [StringLength(Task.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(Task.MaxTitleLength)]
        public string Description { set; get; }

        public Guid? AssignedPersonId { set; get; }
    }
}
