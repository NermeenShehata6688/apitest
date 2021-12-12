using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
     private  ILocationService _locationService;
       public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }


        // GET: api/<CountriesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var all = _locationService.AllCountries();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //State
        [HttpGet]
        public IActionResult GetStates()
        {
            try
            {
                var all = _locationService.AllStates();
                return Ok(all);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult GetStatesWithCities()
        {
            try
            {
                var all = _locationService.AllStatesWithCities();
                return Ok(all);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Cities
        [HttpGet]
        public IActionResult GetCities()
        {
            try
            {
                var all = _locationService.AllCities();
                return Ok(all);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult GetCitiesByStateId(int stateId)
        {
            try
            {
                var all = _locationService.GetCitiesByStateId(stateId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult  PostCountry(CountryDto country)
        {
            try
            {
                var all = _locationService.AddCountry(country);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult PostState(StateDto state)
        {
            try
            {
                var all = _locationService.AddState(state);
                return Ok(all);
            }
            catch (Exception)
            {

                throw;
            } 
        }
        [HttpPost]
        public IActionResult PostCity(CityDto city)
        {
            try
            {
                var all = _locationService.AddCity(city);
                return Ok(all);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteCountry(int countryId)
        {
            try
            {
                var all = _locationService.DeleteCountry(countryId);
                return Ok(all);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteState(int stateId)
        {
            try
            {
                var all = _locationService.DeleteState(stateId);
                return Ok(all);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteCity(int cityId)
        {
            try
            {
                var all = _locationService.DeleteCity(cityId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult EditCountry(CountryDto country)
        {
            try
            {
                var all = _locationService.UpdateCountry(country);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult EditState(StateDto state)
        {
            try
            {
                var all = _locationService.UpdateState(state);
                return Ok(all);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut]
        public IActionResult EditCity(CityDto city)
        {
            try
            {
                var all = _locationService.UpdateCity(city);
                return Ok(all);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
