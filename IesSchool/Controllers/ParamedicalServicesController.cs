using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParamedicalServicesController : ControllerBase
    {
        private IParamedicalServicesService _paramedicalServicesService;
        public ParamedicalServicesController(IParamedicalServicesService paramedicalServicesService)
        {
            _paramedicalServicesService = paramedicalServicesService;
        }


        // GET: api/<ParamedicalServicesController>
        [HttpGet]
        public IActionResult GetParamedicalServices()
        {
            try
            {
                var all = _paramedicalServicesService.GetParamedicalServices();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<ParamedicalServicesController>/5
        [HttpGet]
        public IActionResult GetParamedicalServiceById(int paramedicalServiceIid)
        {
            try
            {
                var all = _paramedicalServicesService.GetParamedicalServiceById(paramedicalServiceIid);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<ParamedicalServicesController>
        [HttpPost]
        public IActionResult PostParamedicalService(ParamedicalServiceDto paramedicalService)
        {
            try
            {
                var all = _paramedicalServicesService.AddParamedicalService(paramedicalService);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<ParamedicalServicesController>/5
        [HttpPut]
        public IActionResult PutParamedicalService(ParamedicalServiceDto paramedicalService)
        {
            try
            {
                var all = _paramedicalServicesService.EditParamedicalService(paramedicalService);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<ParamedicalServicesController>/5
        [HttpDelete]
        public IActionResult DeleteParamedicalService(int paramedicalServiceIid)
        {
            try
            {
                var all = _paramedicalServicesService.DeleteParamedicalService(paramedicalServiceIid);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
