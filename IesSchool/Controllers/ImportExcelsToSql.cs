using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImportExcelsToSql : ControllerBase
    {
        private IImportExcelToSqlService _importExcelToSqlService;
        public ImportExcelsToSql(IImportExcelToSqlService importExcelToSqlService)
        {
            _importExcelToSqlService = importExcelToSqlService;
        }
        [HttpPost]
        public IActionResult ImportStrandsExcel(IFormFile file)
        {
            try
            {
                var all=_importExcelToSqlService.ImportStrandsExcel(file);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
