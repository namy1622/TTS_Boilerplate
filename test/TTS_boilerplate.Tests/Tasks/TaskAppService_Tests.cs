using Abp.Runtime.Validation;
using Microsoft.AspNetCore.Components.Web;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;
using TTS_boilerplate.Sessions;
using TTS_boilerplate.TaskAppService;
using TTS_boilerplate.TaskAppService.Dto;
using Xunit;

namespace TTS_boilerplate.Tests.Tasks
{
    public class TaskAppService_Tests : TTS_boilerplateTestBase
    {
        private readonly ITaskAppService _iTaskAppService;


        public TaskAppService_Tests()
        {

            _iTaskAppService = Resolve<ITaskAppService>();
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_All_Tasks()
        {
            // Act
            var output = await _iTaskAppService.GetAll(new GetAllTasksInput());

            //Assert
            output.Items.Count.ShouldBe(2);

            output.Items.Count(t => t.AssignedPersonName != null).ShouldBe(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_Filtered_Tasks()
        {
            //Act
            var output = await _iTaskAppService.GetAll(new GetAllTasksInput { State = TaskState.Open });

            //Assert
            output.Items.ShouldAllBe(t => t.State == TaskState.Open);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Create_New_Task_With_Title()
        {
            await _iTaskAppService.Create(new CreateTaskInput
            {
                Title = "Newly created task #1"
            });
        }
    
        [Fact]
        public async System.Threading.Tasks.Task Should_Create_New_Task_With_Title_And_Assigned_Person()
        {
            var neo = UsingDbContext(context => context.People.Single(p => p.Name == "Neo"));

            await _iTaskAppService.Create(new CreateTaskInput
            {
                Title = "Newly createed task #1",
                AssignedPersonId = neo.Id
            });

            UsingDbContext(context =>
            {
                var task1 = context.Tasks.FirstOrDefault(t => t.Title == "Newly created task #1");
                task1.ShouldNotBeNull();
                task1.AssignedPersonId.ShouldBe(neo.Id);
            });
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Not_Create_New_Task_Without_Title()
        {
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _iTaskAppService.Create(new CreateTaskInput
                {
                    Title = null
                });
            });
        }
    }
}
