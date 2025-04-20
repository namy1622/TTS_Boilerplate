using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.EntityFrameworkCore;
using TTS_boilerplate.Models;
using Task = TTS_boilerplate.Models.Task;

namespace TTS_boilerplate.Tests
{
    class TestDataBuilder
    {
        private readonly TTS_boilerplateDbContext _context;

        public TestDataBuilder(TTS_boilerplateDbContext context)
        {
            _context = context;
        }

        public void Build()
        {

            var neo = new Person("Jane Smith");
            _context.People.Add(neo);
            _context.SaveChanges();

            _context.Tasks.AddRange(
                new Models.Task("Follow hihii"),
                new Task("Clean your room") { State = TaskState.Completed }
                );
        }
    }
}
