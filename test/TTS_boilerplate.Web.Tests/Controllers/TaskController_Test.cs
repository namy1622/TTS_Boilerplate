﻿using AngleSharp.Html.Parser;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;
using TTS_boilerplate.Web.Controllers;
using Xunit;

namespace TTS_boilerplate.Web.Tests.Controllers
{
    public class TaskController_Test : TTS_boilerplateWebTestBase
    {
        [Fact]
        public async System.Threading.Tasks.Task Should_Get_Tasks_By_State()
        {
            //Act

            var response = await GetResponseAsStringAsync(
                GetUrl<TasksController>(nameof(TasksController.Index), new
                {
                    state = TaskState.Open
                }
                )

                );



            ////Assert
            //response.ShouldNotBeNullOrWhiteSpace();


            // get tasks from database
            //var tasksInDatabase = await UsingDbContextAsync(async dbContext =>
            //{
            //    return await dbContext.Tasks
            //        .Where(t => t.State == TaskState.Open)
            //        .ToListAsync();
            //});

            //// Parse HTML response to check id tasks in the database are returned
            //var document = new HtmlParser().ParseDocument(response);
            //var listItems = document.QuerySelectorAll("#TaskList li");

            ////Check task count
            //listItems.Length.ShouldBe(tasksInDatabase.Count);

            ////Check id returned list items are same those in the datebase
            //foreach(var listItem in listItems)
            //{
            //    var header = listItem.QuerySelector(".list-group-ite-heading");
            //    var taskTitle = header.InnerHtml.Trim();
            //    tasksInDatabase.Any(t => t.Title == taskTitle).ShouldBeTrue();
            //}
        }
    }
}
