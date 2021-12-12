using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IepsController : ControllerBase
    {
        private IIepService _iepService;
        public IepsController(IIepService iepService)
        {
            _iepService = iepService;
        }
        [HttpGet]
        public IActionResult GetIepsHelper()
        {
            try
            {
                var all = _iepService.GetIepsHelper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: api/<IepsController>
        [HttpGet]
        public IActionResult GetIeps()
        {
            try
            {
                var all = _iepService.GetIeps();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET api/<IepsController>/5
        [HttpGet]
        public IActionResult GetIepById(int iepId)
        {
            try
            {
                var all = _iepService.GetIepById(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // POST api/<IepsController>
        [HttpPost]
        public IActionResult PostIep(IepDto iepDto)
        {
            try
            {
                var all = _iepService.AddIep(iepDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // PUT api/<IepsController>/5
        [HttpPut]
        public IActionResult PutIep(IepDto iepDto)
        {
            try
            {
                var all = _iepService.EditIep(iepDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // DELETE api/<IepsController>/5
        [HttpDelete]
        public IActionResult DeleteIep(int iepId)
        {
            try
            {
                var all = _iepService.DeleteIep(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/<IepsController>
        [HttpGet]
        public IActionResult GetGoals()
        {
            try
            {
                var all = _iepService.GetGoals();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<IepsController>/5
        [HttpGet]
        public IActionResult GetGoalById(int goalId)
        {
            try
            {
                var all = _iepService.GetGoalById(goalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<IepsController>
        [HttpPost]
        public IActionResult PostGoal(GoalDto goalDto)
        {
            try
            {
                var all = _iepService.AddGoal(goalDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<IepsController>/5
        [HttpPut]
        public IActionResult PutGoal(GoalDto goalDto)
        {
            try
            {
                var all = _iepService.EditGoal(goalDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<IepsController>/5
        [HttpDelete]
        public IActionResult DeleteGoal(int goalId)
        {
            try
            {
                var all = _iepService.DeleteGoal(goalId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetIepParamedicalServices()
        {
            try
            {
                var all = _iepService.GetIepParamedicalServices();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetIepParamedicalServiceById(int iepParamedicalServiceId)
        {
            try
            {
                var all = _iepService.GetIepParamedicalServiceById(iepParamedicalServiceId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto)
        {
            try
            {
                var all = _iepService.AddIepParamedicalService(iepParamedicalServiceDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto)
        {
            try
            {
                var all = _iepService.EditIepParamedicalService(iepParamedicalServiceDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteIepParamedicalService(int iepParamedicalServiceId)
        {
            try
            {
                var all = _iepService.DeleteIepParamedicalService(iepParamedicalServiceId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
