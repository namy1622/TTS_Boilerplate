using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTS_boilerplate.Controllers;
using TTS_boilerplate.LookupAppService;
using TTS_boilerplate.Models;
using TTS_boilerplate.TaskAppService;
using TTS_boilerplate.TaskAppService.Dto;
using TTS_boilerplate.Web.Models.Tasks;

namespace TTS_boilerplate.Web.Controllers
{
    public class TasksController : TTS_boilerplateControllerBase
    {
        private readonly ITaskAppService _taskAppService;
        private readonly ILookupAppService _lookupAppService;

        public TasksController(
            ITaskAppService taskAppService,
            ILookupAppService lookupAppService)
        {
            _taskAppService = taskAppService;
            _lookupAppService = lookupAppService;
        }

        public async Task<ActionResult> Index(GetAllTasksInput input)
        {
            var output = await _taskAppService.GetAll(input);
            var model = new IndexViewModel(output.Items)
            {
                SelectedTaskState = input.State
            };

            return View(model);
        }

        //-----------------
        public async Task<ActionResult> Create()
        {
            var peopleSelectListItems = (await _lookupAppService.GetPeopleComboboxItems()).Items
                .Select(p => p.ToSelectListItem())
                .ToList();

            peopleSelectListItems.Insert(0, new SelectListItem { Value = string.Empty, Text = L("Unassigned"), Selected = true });

            return View(new CreateTaskViewModel(peopleSelectListItems));
        }
    }

   
}
