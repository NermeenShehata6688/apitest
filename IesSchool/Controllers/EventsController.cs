using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }


        // GET: api/<EventsController>
        [HttpGet]
        public IActionResult GetEvents()
        {
            try
            {
                var all = _eventService.GetEvents();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: api/<EventsController>
     
        [ResponseCache(Duration = 800)]
        [HttpGet]
        public IActionResult GetEventHelper()
        {
            try
            {
                var all = _eventService.GetEventHelper();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }      
        // GET api/<EventsController>/5
        [HttpGet]
        public IActionResult GetEventById(int eventId)
        {
            try
            {
                var all = _eventService.GetEventById(eventId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<EventsController>
        [HttpPost]
        public IActionResult PostEvent(EventDto eventDto)
        {
            try
            {
                var all = _eventService.AddEvent(eventDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<EventsController>/5
        [HttpPut]
        public IActionResult PutEvent(EventDto eventDto)
        {
            try
            {
                var all = _eventService.EditEvent(eventDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE api/<EventsController>/5
        [HttpDelete]
        public IActionResult DeleteEvent(int eventId)
        {
            try
            {
                var all = _eventService.DeleteEvent(eventId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult IsPublished(int eventId, bool isPublished)
        {
            try
            {
                var all = _eventService.IsPublished(eventId, isPublished);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetEventTypes()
        {
            try
            {
                var all = _eventService.GetEventTypes();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetEventTypeById(int eventTypeId)
        {
            try
            {
                var all = _eventService.GetEventTypeById(eventTypeId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostEventType(EventTypeDto eventTypeDto)
        {
            try
            {
                var all = _eventService.AddEventType(eventTypeDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutEventType(EventTypeDto eventTypeDto)
        {
            try
            {
                var all = _eventService.EditEventType(eventTypeDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteEventType(int eventTypeId)
        {
            try
            {
                var all = _eventService.DeleteEventType(eventTypeId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult GetEventStudentsByEventId(int eventId)
        {
            try
            {
                var all = _eventService.GetEventStudentsByEventId(eventId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostEventStudent(List<EventStudentDto> eventStudentDto)
        {
            try
            {
                var all = _eventService.AddEventStudent(eventStudentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult PutEventStudent(List<EventStudentDto> eventStudentDto)
        {
            try
            {
                var all = _eventService.EditEventStudent(eventStudentDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteEventStudent(int eventStudentId)
        {
            try
            {
                var all = _eventService.DeleteEventStudent(eventStudentId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public IActionResult GetEventAttachementByEventId(int eventId)
        {
            try
            {
                var all = _eventService.GetEventAttachementByEventId(eventId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostEventAttachement()
        {

            var modelData = JsonConvert.DeserializeObject<EventAttachementDto>(Request.Form["eventAttachement"]);
            if (Request.Form.Files.Count() > 0)
            {
                
                var all = _eventService.AddEventAttachement(modelData);
                return Ok(all);
            }
            else
            {
                var all = _eventService.AddEventAttachement(modelData);
                return Ok(all);
            }
            //try
            //{
            //    var all = _eventService.AddEventAttachement(eventAttachementDto);
            //    return Ok(all);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        [HttpDelete]
        public IActionResult DeleteEventAttachement(int eventAttachementId)
        {
            try
            {
                var all = _eventService.DeleteEventAttachement(eventAttachementId);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

      
        #region NotNeededNow
        //[HttpPost]
        //public IActionResult PostEventTeacher(List<EventTeacherDto> eventTeacherDto)
        //{
        //    try
        //    {
        //        var all = _eventService.AddEventTeacher(eventTeacherDto);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPut]
        //public IActionResult PutEventTeacher(List<EventTeacherDto> eventTeacherDto)
        //{
        //    try
        //    {
        //        var all = _eventService.EditEventTeacher(eventTeacherDto);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpDelete]
        //public IActionResult DeleteEventTeacher(int eventTeacherId)
        //{
        //    try
        //    {
        //        var all = _eventService.DeleteEventTeacher(eventTeacherId);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpPost]
        //public IActionResult PostEventStudentFiles(IFormFile file, [FromForm] List<EventStudentFileDto> eventStudentFileDtoDto)
        //{
        //    try
        //    {
        //        var all = _eventService.AddEventStudentFiles(file, eventStudentFileDtoDto);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpDelete]
        //public IActionResult DeleteEventStudentFiles(int eventStudentFileId)
        //{
        //    try
        //    {
        //        var all = _eventService.DeleteEventStudentFile(eventStudentFileId);
        //        return Ok(all);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion
    }
}
