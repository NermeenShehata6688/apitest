using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IEventService
    {
        public ResponseDto GetEventHelper();
        public ResponseDto GetEvents();
        public ResponseDto GetEventById(int eventId);
        public ResponseDto AddEvent(EventDto eventDto);
        public ResponseDto EditEvent(EventDto eventDto);
        public ResponseDto DeleteEvent(int eventId);
        public ResponseDto IsPublished(int eventId, bool isPublished);

        public ResponseDto GetEventTypes();
        public ResponseDto GetEventTypeById(int eventTypeId);
        public ResponseDto AddEventType(EventTypeDto eventTypeDto);
        public ResponseDto EditEventType(EventTypeDto eventTypeDto);
        public ResponseDto DeleteEventType(int eventTypeId);

        public ResponseDto GetEventStudentsByEventId(int eventId);
        public ResponseDto GetEventStudentById(int eventStudentId);
        public ResponseDto AddEventStudent(List<EventStudentDto> eventStudentDto);
        public ResponseDto EditEventStudent(List<EventStudentDto> eventStudentDto);
        public ResponseDto DeleteEventStudent(int eventStudentId);

        public ResponseDto GetEventTeachers();
        public ResponseDto GetEventTeacherById(int eventTeacherId);
        public ResponseDto AddEventTeacher(List<EventTeacherDto> eventTeacherDto);
        public ResponseDto EditEventTeacher(List<EventTeacherDto> eventTeacherDto);
        public ResponseDto DeleteEventTeacher(int eventTeacherId);

        public ResponseDto AddEventAttachement(List<EventAttachementDto> eventAttachementDto);
        public ResponseDto EditEventAttachement(List<EventAttachementDto> eventAttachementDto);
        public ResponseDto DeleteEventAttachement(int eventAttachementId);

        public ResponseDto AddEventStudentFiles(IFormFile file, List<EventStudentFileDto> eventStudentFiles);
        public ResponseDto EditEventStudentFiles(List<EventStudentFileDto> eventStudentFiles);
        public ResponseDto DeleteEventStudentFile(int eventStudentFileId);

    }
}
