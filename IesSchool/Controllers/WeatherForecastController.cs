using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        ILocationService locationService;
        IFileService fileService;
        public WeatherForecastController(IFileService fileService, ILocationService countryService)
        {
            this.fileService = fileService;
            this.locationService = countryService;
        }

      
   

        private static readonly string[] Summaries = new[]
        {
        "asd", "Bracing", "Chilly", "Cool", "Milsssd", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

         [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "GetWeatherForecasts")]
        public ResponseDto Get(int id)
        {
            var asd = this.locationService.AllCountries();
            return asd;
        }
        [HttpPost(Name = "UploadFile")]
        public IActionResult UploadFile(IFormFile formFile)
        {
            var res = this.fileService.UploadFile(formFile);
            return Ok(res);
        }


        //[HttpGet(Name = "GetAllCountries")]
        //public IEnumerable<ContryDto> GetAllCountries()
        //{
        //        var asd=this.countryService.AllCuntries();
        //    return asd;
        //}
    }
}