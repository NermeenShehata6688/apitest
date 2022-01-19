using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImportExcelsToSqlController : ControllerBase
    {
        private IImportExcelToSqlService _importExcelToSqlService;
        public ImportExcelsToSqlController(IImportExcelToSqlService importExcelToSqlService)
        {
            _importExcelToSqlService = importExcelToSqlService;
        }
        [HttpPost]
        public IActionResult ImportAreasExcel(IFormFile file)
        {
            try
            {
                var all=_importExcelToSqlService.ImportAreasExcel(file);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult ImportStrandsExcel(IFormFile file)
        {
            try
            {
                var all = _importExcelToSqlService.ImportStrandsExcel(file);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult ImportSkillsExcel(IFormFile file)
        {
            try
            {
                var all = _importExcelToSqlService.ImportSkillsExcel(file);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
