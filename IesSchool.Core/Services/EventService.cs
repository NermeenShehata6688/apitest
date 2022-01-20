using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class EventService : IEventService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        private IFileService _ifileService;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext, IFileService ifileService)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
            _ifileService = ifileService;
        }
        public ResponseDto GetEventHelper()
        {
            try

            {
                EventHelper eventHelper = new EventHelper()
                {
                    AllDepartments = _uow.GetRepository<Department>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                    AllTeachers = _uow.GetReadOnlyRepository<User>().GetList((x => new User  {Id=  x.Id ,Name= x.Name }), x => x.IsDeleted != true && x.IsTeacher == true, null,null, 0, 1000000, true),
                    AllStudents = _uow.GetRepository<Student>().GetList((x => new Student { Id = x.Id, Name = x.Name, NameAr = x.NameAr }),x => x.IsDeleted != true , x => x.OrderBy(c => c.Name), null, 0, 1000000, true),
                    AllEventTypes = _uow.GetRepository<EventType>().GetList(null, x => x.OrderBy(c => c.Name), null, 0, 1000000, true),
                   // AllEvents = _uow.GetRepository<Event>().GetList(x=> x.IsDeleted!=true, x => x.OrderBy(c => c.Name), null, 0, 1000000, true),
                };
                var mapper = _mapper.Map<EventHelperDto>(eventHelper);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }

            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetEvents()
        {
            try
            {
                var allEvents = _uow.GetRepository<Event>().GetList(x => x.IsDeleted != true, null, x=> x.Include(x=>x.Department).Include(x => x.EventType), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<EventDto>>(allEvents);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetEventById(int eventId)
        {
            try
            {
                var oEvent = _uow.GetRepository<Event>().Single(x => x.Id == eventId && x.IsDeleted != true, null,
                    x=> x.Include(x=> x.EventTeachers).ThenInclude(x => x.Teacher)
                    .Include(x => x.EventStudents).ThenInclude(x => x.Student)
                    .Include(x => x.EventStudents).ThenInclude(x => x.EventStudentFiles));
                var mapper = _mapper.Map<EventGetDto>(oEvent);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddEvent(EventDto oEventDto)
        {
            try
            {
                oEventDto.IsDeleted = false;
                oEventDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Event>(oEventDto);
                _uow.GetRepository<Event>().Add(mapper);
                _uow.SaveChanges();
                oEventDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Event Added  Seccessfuly", Data = oEventDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditEvent(EventDto oEventDto)
        {
            try
            {
                if (oEventDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from Event_Teacher where EventId={oEventDto.Id}";
                    // $"delete from Event_Student where EventId={oEventDto.Id}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);
                    var mapper = _mapper.Map<Event>(oEventDto);
                    _uow.GetRepository<Event>().Update(mapper);
                    _uow.SaveChanges();
                    oEventDto.Id = mapper.Id;
                    transaction.Commit();
                    return new ResponseDto { Status = 1, Message = "Event Updated Seccessfuly", Data = oEventDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null", Data = oEventDto };

                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteEvent(int eventId)
        {
            try
            {
                Event oEvent = _uow.GetRepository<Event>().Single(x => x.Id == eventId);
                oEvent.IsDeleted = true;
                oEvent.DeletedOn = DateTime.Now;

                _uow.GetRepository<Event>().Update(oEvent);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Event Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto IsPublished(int eventId, bool isPublished)
        {
            try
            {
                Event oEvent = _uow.GetRepository<Event>().Single(x => x.Id == eventId);
                oEvent.IsPublished = isPublished;
                _uow.GetRepository<Event>().Update(oEvent);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Event Is Published State Has Changed" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetEventTypes()
        {
            try
            {
                var allEventTypes = _uow.GetRepository<EventType>().GetList();
                var mapper = _mapper.Map<PaginateDto<EventTypeDto>>(allEventTypes);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetEventTypeById(int eventTypeId)
        {
            try
            {
                var eventType = _uow.GetRepository<EventType>().Single(x => x.Id == eventTypeId, null);
                var mapper = _mapper.Map<EventTypeDto>(eventType);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddEventType(EventTypeDto eventTypeDto)
        {
            try
            {
                var mapper = _mapper.Map<EventType>(eventTypeDto);
                _uow.GetRepository<EventType>().Add(mapper);
                _uow.SaveChanges();
                eventTypeDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "EventType Added  Seccessfuly", Data = eventTypeDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditEventType(EventTypeDto eventTypeDto)
        {
            try
            {
                var mapper = _mapper.Map<EventType>(eventTypeDto);
                _uow.GetRepository<EventType>().Update(mapper);
                _uow.SaveChanges();
                eventTypeDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "EventType Updated Seccessfuly", Data = eventTypeDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteEventType(int eventTypeId)
        {
            try
            {
                var cmd = $"delete from EventType where Id={eventTypeId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "EventType Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetEventStudentsByEventId(int eventId)
        {
            try
            {
                var allEventStudents = _uow.GetRepository<EventStudent>().GetList(x=> x.EventId== eventId, null, x => x.Include(x => x.EventStudentFiles), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<EventStudentDto>>(allEventStudents);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetEventStudentById(int eventStudentId)
        {
            try
            {
                var eventStudent = _uow.GetRepository<EventStudent>().Single(x => x.Id == eventStudentId, null, x => x.Include(x => x.EventStudentFiles));
                var mapper = _mapper.Map<EventStudentDto>(eventStudent);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddEventStudent(List<EventStudentDto>  eventStudentDto)
        {
            try
            {
                List<EventStudent> eventStudent = new List<EventStudent>();
                foreach (var item in eventStudentDto)
                    eventStudent.Add(new EventStudent { EventId = item.EventId, StudentId = item.StudentId });

                _uow.GetRepository<EventStudent>().Add(eventStudent);
                _uow.SaveChanges();
                //eventStudentDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Event Student Added  Seccessfuly", Data = eventStudentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditEventStudent(List<EventStudentDto> eventStudentDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from Event_Student where EventId={eventStudentDto.FirstOrDefault().EventId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                List<EventStudent> eventStudent = new List<EventStudent>();

                foreach (var item in eventStudentDto)
                    eventStudent.Add(new EventStudent { EventId = item.EventId, StudentId = item.StudentId });

                _uow.GetRepository<EventStudent>().Update(eventStudent);
                _uow.SaveChanges();
                transaction.Commit();
                return new ResponseDto { Status = 1, Message = "Event Student Updated Seccessfuly", Data = eventStudentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteEventStudent(int eventStudentId)
        {
            try
            {
                var cmd = $"delete from Event_Student where Id={eventStudentId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "EventType Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetEventTeachers()
        {
            try
            {
                var allEventTeachers = _uow.GetRepository<EventTeacher>().GetList(null, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<EventTeacherDto>>(allEventTeachers);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetEventTeacherById(int eventTeacherId)
        {
            try
            {
                var eventTeacher = _uow.GetRepository<EventTeacher>().Single(x => x.Id == eventTeacherId , null);
                var mapper = _mapper.Map<EventTeacherDto>(eventTeacher);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddEventTeacher(List<EventTeacherDto> eventTeacherDto)
        {
            try
            {
                List<EventTeacher> eventTeacher = new List<EventTeacher>();
                foreach (var item in eventTeacherDto)
                    eventTeacher.Add(new EventTeacher { EventId = item.EventId, TeacherId = item.TeacherId });

                _uow.GetRepository<EventTeacher>().Add(eventTeacher);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Event Teacher Added  Seccessfuly", Data = eventTeacherDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditEventTeacher(List<EventTeacherDto> eventTeacherDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from Event_Teacher where EventId={eventTeacherDto.FirstOrDefault().EventId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                List<EventTeacher> eventTeacher = new List<EventTeacher>();

                foreach (var item in eventTeacherDto)
                    eventTeacher.Add(new EventTeacher { EventId = item.EventId, TeacherId = item.TeacherId });

                _uow.GetRepository<EventTeacher>().Update(eventTeacher);
                _uow.SaveChanges();
                transaction.Commit();
                return new ResponseDto { Status = 1, Message = "Event Teacher Updated Seccessfuly", Data = eventTeacherDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteEventTeacher(int eventTeacherId)
        {
            try
            {
                var cmd = $"delete from Event_Teacher where Id={eventTeacherId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Event Teacher Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetEventAttachementByEventId(int eventId)
        {
            try
            {
                var oLeventAttachement = _uow.GetRepository<EventAttachement>().GetList(x => x.EventId == eventId, null);
                var mapper = _mapper.Map <PaginateDto<EventAttachementDto>>(oLeventAttachement);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddEventAttachement(EventAttachementDto eventAttachementDto)
        {
            try
            {
                //change File to binary
                using var transaction = _iesContext.Database.BeginTransaction();
                List<EventAttachement> eventAttachement = new List<EventAttachement>();
                //foreach (var item in eventAttachementDto)
                //{
                //    EventAttachmentBinary eventAttachmentBinary = new EventAttachmentBinary();
                //    //if (item.Id == 0)
                    //{
                        //if (item.file!=null)
                        //{
                        //    MemoryStream ms = new MemoryStream();
                        //    item.file.CopyTo(ms);
                        //    eventAttachmentBinary = new EventAttachmentBinary();
                        //    eventAttachmentBinary.FileBinary = ms.ToArray();
                        //    ms.Close();
                        //    ms.Dispose();

                        //   // upload file in local directory
                        //    var result = _ifileService.UploadFile(item.file);
                        //}
                        //eventAttachement.Add(new EventAttachement
                        //{
                        //    EventId = item.EventId,
                        //    Name = item.Name,
                        //    Description = item.Description,
                        //    Date = item.Date,
                        //    IsPublished = item.IsPublished,
                        //    FileName = item.FileName,
                        //    EventAttachmentBinary = eventAttachmentBinary
                        //});
                    //}
                //}
                //_uow.GetRepository<EventAttachement>().Add(eventAttachement);
                //_uow.SaveChanges();
                transaction.Commit();

                return new ResponseDto { Status = 1, Message = "Event Attachements Added  Seccessfuly", Data = eventAttachementDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditEventAttachement(List<EventAttachementDto> eventAttachementDto)
        {
            try
            {
                /// not use this func
                //        using var transaction = _iesContext.Database.BeginTransaction();
                //        List<EventAttachement> eventAttachement = new List<EventAttachement>();

                //        foreach (var item in eventAttachementDto)
                //            eventAttachement.Add(new EventAttachement
                //            {
                //                EventId = item.EventId,
                //                Name = item.Name,
                //                Description = item.Description,
                //                Date = item.Date,
                //                IsPublished = item.IsPublished,
                //                FileName = item.FileName,
                //            });
                //        var mapper = _mapper.Map<List<EventAttachement>>(eventAttachementDto);
                //        _uow.GetRepository<EventAttachement>().Update(mapper);
                //        _uow.SaveChanges();
                //        transaction.Commit();
                return new ResponseDto { Status = 1, Message = "Event Attachement Updated Seccessfuly", Data = eventAttachementDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteEventAttachement(int eventAttachementId)
        {
            try
            {
                var cmd = $"delete from EventAttachement where Id={eventAttachementId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Event Attachement Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto AddEventStudentFiles(IFormFile file, List<EventStudentFileDto> eventStudentFileDto)
        {
            try
            {
                List<EventStudentFile> eventStudentFile = new List<EventStudentFile>();
                foreach (var item in eventStudentFileDto)
                    eventStudentFile.Add(new EventStudentFile
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Date = item.Date,
                        EventStudentId=item.EventStudentId,
                        IsPublished = item.IsPublished,
                        FileName = item.FileName,
                    });

                _uow.GetRepository<EventStudentFile>().Add(eventStudentFile);
                return new ResponseDto { Status = 1, Message = "Event Student File Added  Seccessfuly", Data = eventStudentFileDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditEventStudentFiles(List<EventStudentFileDto> eventStudentFileDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from EventStudentFiles where EventStudentId={eventStudentFileDto.FirstOrDefault().EventStudentId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                List<EventStudentFile> eventStudentFile = new List<EventStudentFile>();

                foreach (var item in eventStudentFileDto)
                    eventStudentFile.Add(new EventStudentFile
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Date = item.Date,
                        EventStudentId = item.EventStudentId,
                        IsPublished = item.IsPublished,
                        FileName = item.FileName,
                    });

                _uow.GetRepository<EventStudentFile>().Update(eventStudentFile);
                _uow.SaveChanges();
                transaction.Commit();
                return new ResponseDto { Status = 1, Message = "Event Student File Updated Seccessfuly", Data = eventStudentFileDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteEventStudentFile(int eventStudentFileId)
        {
            try
            {
                var cmd = $"delete from EventStudentFiles where Id={eventStudentFileId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Event Student File Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}