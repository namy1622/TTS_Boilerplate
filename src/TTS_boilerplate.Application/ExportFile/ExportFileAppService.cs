using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.ExportFile
{
    public class ExportFileAppService : TTS_boilerplateAppServiceBase, IExportFileAppService
    {
        public ExportFileAppService()
        {

        }
        public async Task<byte[]> ExportToExcel<T>(List<T> data, string sheetName = "Sheet1")
        {
            using(var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(sheetName);

                // Lấy tất cả properties của class T
                var properties = typeof(T).GetProperties();

                // Thêm header
                for (int i = 0; i< properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i].Name;
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }
                // Thêm dữ liệu
                for (int row = 0; row < data.Count; row++)
                {
                    for(int col = 0; col < properties.Length; col++)
                    {
                        var value = properties[col].GetValue(data[row]);
                        if(value is DateTime dateTime)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dateTime.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            worksheet.Cells[row + 2, col + 1].Value = value;
                        }
                    }
                }
                // Tự động điều chỉnh độ rộng cột
                worksheet.Cells.AutoFitColumns();

                return await package.GetAsByteArrayAsync();
            }
            // Implement your export logic here
            
        }
    }

}
