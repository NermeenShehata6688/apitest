using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IFileService _fileService;
        public UsersController(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }
        [ResponseCache(Duration = 800)]
        [HttpGet]
        public IActionResult GetUserHelper()
        {
            try
            {
                var all = _userService.GetUsersHelper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult GetUsers([FromQuery] UserSearchDto userSearchDto)
        {
            try
            {
                var all = _userService.GetUsers( userSearchDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            try
            {
                var all = _userService.GetAllTeachers();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetAllTherapists()
        {
            try
            {
                var all = _userService.GetAllTherapists();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetTeacherAssignedStudents(int teacherId )
        {
            try
            {
                var all = _userService.GetTeacherAssignedStudents(teacherId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetTherapistAssignedStudents(int therapistId)
        {
            try
            {
                var all = _userService.GetTherapistAssignedStudents(therapistId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetTherapistParamedicalServices(int therapistId)
        {
            try
            {
                var all = _userService.GetTherapistParamedicalServices(therapistId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<UsersController>/5
        [HttpGet]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var all = _userService.GetUserById(userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult PostUser(UserDto userDto)
        {
            try
            {
                var all = _userService.AddUser(userDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public IActionResult PutUser(UserDto userDto)
        {
            try
            {
                var all = _userService.EditUser(userDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                var all = _userService.DeleteUser(userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetUserAttachmentById(int userAttachmentId)
        {
            try
            {
                var all = _userService.GetUserAttachmentById(userAttachmentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult AddUserAttachment([FromForm]  UserAttachmentDto userAttachmentDto)
        {

            var file = Request.Form.Files[0];

            //changeFile to binary
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            UserAttachmentBinaryDto userAttachmentBinaryDto = new UserAttachmentBinaryDto();
            userAttachmentBinaryDto.FileBinary = ms.ToArray();
            ms.Close();
            ms.Dispose();

            var result= _fileService.UploadFile(file);
            //complateFilePat to save it local
            //var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            ////var uploadPathWithfileName = Path.Combine(uploadPath, fileName);
            //using (var fileStream = new FileStream(uploadPathWithfileName, FileMode.Create))
            //{
            //    file.CopyTo(fileStream);
            //    userAttachmentDto.FileName = fileName;
            //}

            userAttachmentDto.FileName = result.FileName;

            //userAttachmentDto.Name 

            try
            {
                var all = _userService.AddUserAttachment(file ,userAttachmentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult PutUserAttachment(UserAttachmentDto userAttachmentDto)
        {
            try
            {
                var all = _userService.EditUserAttachment(userAttachmentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteUserAttachment(int userAttachmentId)
        {
            try
            {
                var all = _userService.DeleteUserAttachment(userAttachmentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IsSuspended(int userId, bool isSuspended)
        {
            try
            {
                var all = _userService.IsSuspended(userId, isSuspended);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
