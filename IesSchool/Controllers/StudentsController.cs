using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentService _studentService;
        private IFileService _ifileService;
        public StudentsController(IStudentService studentService, IFileService ifileService)
        {
            _studentService = studentService;
            _ifileService = ifileService;
        }
        [HttpGet]
        public IActionResult GetStudents([FromQuery]StudentSearchDto studentSearchHelperDto)
        {
            try
            {
                var all = _studentService.GetStudents(studentSearchHelperDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: api/<StudentsController>
        [ResponseCache(Duration = 800)]
        [HttpGet]
        public IActionResult GetStudentHelper()
        {
            try
            {
                var all = _studentService.GetStudentHelper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        // GET api/<StudentsController>/5
        [HttpGet]
        public IActionResult GetStudentById(int studentId)
        {
            try
            {
                var all = _studentService.GetStudentById(studentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult PostStudent(StudentDto studentDto)
        {
            try
            {
                var all = _studentService.AddStudent(studentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<StudentsController>/5
        [HttpPut]
        public IActionResult PutStudent(StudentDto studentDtoe)
        {
            try
            {
                var all = _studentService.EditStudent(studentDtoe);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete]
        public IActionResult DeleteStudent(int studentId)
        {
            try
            {
                var all = _studentService.DeleteStudent(studentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddStudentHistoricalSkills(List<StudentHistoricalSkillDto> studentHistoricalSkillDto)
        {
            try
            {
                var all = _studentService.AddStudentHistoricalSkill(studentHistoricalSkillDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteStudentHistoricalSkill(int studentHistoricalSkillId)
        {
            try
            {
                var all = _studentService.DeleteStudentHistoricalSkill(studentHistoricalSkillId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddStudentAttachment(StudentAttachmentDto studentAttachmentDto)
        {
            try
            {
                var all = _studentService.AddStudentAttachment(studentAttachmentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult PutStudentAttachment(StudentAttachmentDto studentAttachmentDto)
        {
            try
            {
                var all = _studentService.EditStudentAttachment(studentAttachmentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteStudentAttachment(int studentAttachmentId)
        {
            try
            {
                var all = _studentService.DeleteStudentAttachment(studentAttachmentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                var all = _ifileService.UploadFile(file);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult AddStudentPhone(PhoneDto PhoneDto)
        {
            try
            {
                var all = _studentService.AddStudentPhone(PhoneDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult PutStudentPhone(PhoneDto phoneDto)
        {
            try
            {
                var all = _studentService.EditStudentPhone(phoneDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteStudentPhone(int phoneId)
        {
            try
            {
                var all = _studentService.DeleteStudentPhone(phoneId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IsSuspended(int studentId, bool isSuspended)
        {
            try
            {
                var all = _studentService.IsSuspended(studentId, isSuspended);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
