using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        // GET: api/<DepartmentController>
        [HttpGet]
        public IActionResult GetDepartments()
        {
            try
            {
                var all = _departmentService.GetDepartments();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<DepartmentController>/5
        [HttpGet]
        public IActionResult GetDepartmentByID(int departmentId)
        {
            try
            {
                var all = _departmentService.GetDepartmentById(departmentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public IActionResult PostDepartment(DepartmentDto departmentDto)
        {
            try
            {
                var all = _departmentService.AddDepartment(departmentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<DepartmentController>/5
        [HttpPut]
        public IActionResult PutDepartment(DepartmentDto departmentDto)
        {
            try
            {
                var all = _departmentService.EditDepartment(departmentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete]
        public IActionResult DeleteDepartment(int departmentId)
        {
            try
            {
                var all = _departmentService.DeleteDepartment(departmentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}
