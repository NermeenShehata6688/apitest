using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogCommentsController : ControllerBase
    {
        private ILogCommentService _logCommentService;
        public LogCommentsController(ILogCommentService  logCommentService)
        {
            _logCommentService  = logCommentService;
        }
        [HttpGet]
        public IActionResult GetStudentLogComments(int studentId)
        {
            try
            {
                var all = _logCommentService.GetStudentLogComments(studentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetIEPLogComments(int iepId)
        {
            try
            {
                var all = _logCommentService.GetIEPLogComments(iepId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostLogComment(LogCommentDto logCommentDto)
        {
            try
            {
                var all = _logCommentService.AddLogComment(logCommentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteLogComment(int logCommentId)
        {
            try
            {
                var all = _logCommentService.DeleteLogComment(logCommentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
