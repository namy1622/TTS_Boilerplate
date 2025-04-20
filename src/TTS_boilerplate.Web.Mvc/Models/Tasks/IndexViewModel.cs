using Abp.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TTS_boilerplate.Models;
using TTS_boilerplate.TaskAppService.Dto;

namespace TTS_boilerplate.Web.Models.Tasks
{


    public class IndexViewModel
    {

        public TaskState? SelectedTaskState { get; set; }


        public IReadOnlyList<TaskListDto> Tasks { get; set; }
        public IndexViewModel(IReadOnlyList<TaskListDto> tasks)
        {
            Tasks = tasks;

            //SelectedTaskState = selectedTaskState;
        }

        public string GetTaskLabel(TaskListDto task)
        {
            switch (task.State)
            {
                case TaskState.Completed:
                    return "bg-success";
                case TaskState.Open:
                    return "bg-primary";
                default:
                    return "btn-ouline-primary";
            }
        }


        public List<SelectListItem> GetTasksStateSelectListItems(ILocalizationManager localizationManager)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = localizationManager.GetString(TTS_boilerplateConsts.LocalizationSourceName, "AllTasks"),
                    Value = "",
                    Selected = SelectedTaskState == null
                }
            };

            list.AddRange(Enum.GetValues(typeof(TaskState))
                .Cast<TaskState>()
                .Select(state =>
                    new SelectListItem
                    {
                        Text = localizationManager.GetString(TTS_boilerplateConsts.LocalizationSourceName, $"TaskState_{state}"),
                        Value = state.ToString(),
                        Selected = state == SelectedTaskState
                    }
                )
                );

            return list;
        }

    }






}
