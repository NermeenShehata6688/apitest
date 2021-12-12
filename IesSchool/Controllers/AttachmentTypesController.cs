using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttachmentTypesController : ControllerBase
    {
        private IAttachmentTypeService _attachmentTypeService;
        public AttachmentTypesController(IAttachmentTypeService attachmentTypeService)
        {
            _attachmentTypeService = attachmentTypeService;
        }
        // GET: api/<AttachmentTypesController>
        [HttpGet]
        public IActionResult GetAttachmentTypes()
        {
            try
            {
                var all = _attachmentTypeService.GetAttachmentTypes();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<AttachmentTypesController>/5
        [HttpGet]
        public IActionResult GetAttachmentTypeById(int attachmentTypeId)
        {
            try
            {
                var all = _attachmentTypeService.GetAttachmentTypeById(attachmentTypeId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<AttachmentTypesController>
        [HttpPost]
        public IActionResult PostAttachmentType(AttachmentTypeDto attachmentTypeDto)
        {
            try
            {
                var all = _attachmentTypeService.AddAttachmentType(attachmentTypeDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<AttachmentTypesController>/5
        [HttpPut]
        public IActionResult PutAttachmentType(AttachmentTypeDto attachmentTypeDto)
        {
            try
            {
                var all = _attachmentTypeService.EditAttachmentType(attachmentTypeDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<AttachmentTypesController>/5
        [HttpDelete]
        public IActionResult DeleteAttachmentType(int attachmentTypeId)
        {
            try
            {
                var all = _attachmentTypeService.DeleteAttachmentType(attachmentTypeId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
