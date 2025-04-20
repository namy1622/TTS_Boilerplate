using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;

namespace TTS_boilerplate.LookupAppService
{
    public class LookupAppService : TTS_boilerplateAppServiceBase, ILookupAppService
    {
        private readonly IRepository<Person, Guid> _personRepository;
        private readonly IRepository<TTS_boilerplate.Models.Category> _categoryRepository;


        public LookupAppService(IRepository<Person, Guid> personRepository, IRepository<TTS_boilerplate.Models.Category> category)
        {
            _personRepository = personRepository;
            _categoryRepository = category;

        }

        public async Task<ListResultDto<ComboboxItemDto>> GetCategoryComboboxItem()
        {
            var category = await _categoryRepository.GetAllListAsync();

            Console.WriteLine(category);
             
            return new ListResultDto<ComboboxItemDto>(
                category.Select(p => new ComboboxItemDto(p.Id.ToString("D"), p.NameCategory)).ToList() );
        }

        public async Task<ListResultDto<ComboboxItemDto>> GetPeopleComboboxItems()
        {
            var people = await _personRepository.GetAllListAsync();
            return new ListResultDto<ComboboxItemDto>(
                people.Select(p => new ComboboxItemDto(p.Id.ToString("D"), p.Name)).ToList());
        }
    }
}
