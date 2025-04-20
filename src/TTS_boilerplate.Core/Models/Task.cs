using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Models
{
    [Table("AppTasks")]
    public class Task : Entity, IHasCreationTime
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        public Task()
        {
            CreationTime = Clock.Now;
            State = TaskState.Open;
        }

        //---
        [ForeignKey(nameof(AssignedPersonId))]
        public Person AssignedPerson { get; set; }

        //guid: (global unique indentifier) 1 kdl mã định danh duy nhất trên toàn càu(thường được dùng làm khóa chính or ngoại) 
        public Guid? AssignedPersonId { set; get; }

        //---

        public Task(string title, string description = null, Guid? assignedPersonid = null)
            : this()
        {
            Title = title;
            Description = description;

            AssignedPersonId = assignedPersonid;
        }
    }

    public enum TaskState : byte
    {
        Open = 0,
        Completed = 1
    }
}

