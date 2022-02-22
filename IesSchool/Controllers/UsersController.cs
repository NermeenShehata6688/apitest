using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        //[ResponseCache(Duration = 800)]
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
        public IActionResult PostUser()
        {
            try
            {
                var modelData = JsonConvert.DeserializeObject<UserDto>(Request.Form["user"]);
                //var file = Request.Form.Files[0];

                if (Request.Form.Files.Count() > 0)
                {
                    var file = Request.Form.Files[0];
                    var all = _userService.AddUser(file , modelData);
                    return Ok(all.Result);

                }
                else
                {
                    var all = _userService.AddUser2(modelData);
                    return Ok(all.Result);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public IActionResult PutUser()
        {
            try
            {
                var modelData =JsonConvert.DeserializeObject<UserDto>(Request.Form["user"]);

                //var modelData =(UserDto)Newtonsoft.Json.JsonConvert.DeserializeObject<UserDto>(Request.Form["user"]);
                //var report = (HRDashboardViewModel)Newtonsoft.Json.JsonConvert.DeserializeObject(response, typeof(HRDashboardViewModel));
                //return report.data.Count.ToString();
                ////List<MyStok> myDeserializedObjList = (List<MyStok>)Newtonsoft.Json.JsonConvert.DeserializeObject(sc, typeof(List<MyStok>));
                //var file = Request.Form.Files[0];

                if (Request.Form.Files.Count() >0)
                {
                    var file = Request.Form.Files[0];
                    var all = _userService.EditUser(file, modelData);
                    return Ok(all);
                }
                else
                {
                    var all = _userService.EditUser2(modelData);
                    return Ok(all);
                }
            }
            catch (Exception ex)
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
        public IActionResult GetUserAttachmentByUserId(int userId)
        {
            try
            {
                var all = _userService.GetUserAttachmentByUserId(userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult AddUserAttachment()
        {
            try
            {
                var modelData = JsonConvert.DeserializeObject<UserAttachmentDto>(Request.Form["userAttachment"]);
                var file = Request.Form.Files[0];
                var all = _userService.AddUserAttachment(file, modelData);
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
        [HttpPut]
        public IActionResult IsActive(IsActiveDto isActiveDto)
        {
            try
            {
                var all = _userService.IsActive(isActiveDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult IsUserNameExist(string UserName, int? userId)
        {
            try
            {
                var all = _userService.IsUserNameExist(UserName, userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult IsUserCodeExist(string UserCode, int? userId)
        {
            try
            {
                var all = _userService.IsUserCodeExist(UserCode, userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult test(UserDto userDto)
        {
            try
            {
                //var all = _userService.AddUser(file, userDto);
                return Ok("");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetUserForProfileById(int userId)
        {
            try
            {
                var all = _userService.GetUserForProfileById(userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetAllParents([FromQuery]UserSearchDto userSearchDto)
        {
            try
            {
                var all = _userService.GetAllParents(userSearchDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult IsParentUserNameExist(string UserName, int? userId)
        {
            try
            {
                var all = _userService.IsParentUserNameExist(UserName, userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult IsParentEmailExist(string Email, int? userId)
        {
            try
            {
                var all = _userService.IsParentEmailExist(Email, userId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
