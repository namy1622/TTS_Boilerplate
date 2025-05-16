using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.ExportFile
{
    public interface IExportFileAppService : IApplicationService
    {
        Task<byte[]> ExportToExcel<T>(List<T> data, string sheetName = "Sheet1");
    }
}
