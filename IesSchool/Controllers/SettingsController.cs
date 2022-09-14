using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private ISettingService _settingService;
        private IFileService _ifileService;
        public SettingsController(ISettingService settingService, IFileService ifileService)
        {
            _settingService = settingService;
            _ifileService = ifileService;
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

        [HttpPost]
        public IActionResult UploadImage()
        {
            try
            {
                //var modelData = JsonConvert.DeserializeObject<StudentDto>(Request.Form["student"]);
                if (Request.Form.Files.Count() > 0)
                {
                    var file = Request.Form.Files[0];
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    var ImageBinary = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                    var result = _ifileService.SaveBinary(file.FileName, ImageBinary);
                    var FullPath = result.virtualPath;
                    var path = new
                    {
                        imageUrl = FullPath,
                    };
                    return Ok(path);
                }
                else
                {
                    return Ok("");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
