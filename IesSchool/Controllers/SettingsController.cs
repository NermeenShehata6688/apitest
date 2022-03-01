using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private ISettingService _settingService;
        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// This is method summary I want displayed
        /// </summary>
        [HttpGet]
        public IActionResult GetSetting()
        {
            try
            {
                var all = _settingService.GetSetting();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult PutSetting(SettingDto settingDto)
        {
            try
            {
                var all = _settingService.EditSetting(settingDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult AboutUs()
        {
            try
            {
                var all = _settingService.AboutUs();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult AddAboutUs(SettingDto settingDto)
        {
            try
            {
                var all = _settingService.AddAboutUs(settingDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
