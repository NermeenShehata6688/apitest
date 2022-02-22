using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using IesSchool.InfraStructure.Paging;
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext, IFileService ifileService, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
            _ifileService = ifileService;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
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
                    AllEventTypes = _uow.GetRepository<EventType>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 1000000, true),
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
                    x=> x.Include(x=> x.EventAttachements)
                    .Include(x=> x.EventTeachers)
                    .Include(x => x.EventStudents).ThenInclude(x => x.Student)
                    .Include(x => x.EventStudents).ThenInclude(x => x.EventStudentFiles));
                var mapper = _mapper.Map<EventGetDto>(oEvent);

                if (mapper!=null)
                {
                    if (mapper.EventAttachements != null && mapper.EventAttachements.Count() > 0)
                    {
                        mapper.EventAttachements = GetFullPathAndBinaryIColliction(mapper.EventAttachements);
                    }
                    if (mapper.EventStudents != null && mapper.EventStudents.Count > 0)
                    {
                        foreach (var item in mapper.EventStudents)
                        {
                            if (item.EventStudentFiles != null)
                            {
                                item.EventStudentFiles = GetFullPathAndBinaryStudentFilesICollection(item.EventStudentFiles);
                            }
                        }
                    }
                }
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
        public ResponseDto IsPublished(IsPuplishedDto isPuplishedDto)
        {
            try
            {
                Event oEvent = _uow.GetRepository<Event>().Single(x => x.Id == isPuplishedDto.Id);
                oEvent.IsPublished = isPuplishedDto.IsPuplished;
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
                var allEventTypes = _uow.GetRepository<EventType>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true);
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
                EventType oEventType = _uow.GetRepository<EventType>().Single(x => x.Id == eventTypeId);
                oEventType.IsDeleted = true;
                oEventType.DeletedOn = DateTime.Now;

                _uow.GetRepository<EventType>().Update(oEventType);
                _uow.SaveChanges();
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
                var allEventStudents = _uow.GetRepository<EventStudent>().GetList(x=> x.EventId== eventId, null, x => x.Include(x => x.EventStudentFiles).Include(x => x.Student), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<EventStudentDto>>(allEventStudents);
                foreach (var item in mapper.Items)
                {
                    if (item.EventStudentFiles!= null&&item.EventStudentFiles.Count() > 0)
                    {
                        item.EventStudentFiles = GetFullPathAndBinaryStudentFilesICollection(item.EventStudentFiles);
                    }
                }
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
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

        public ResponseDto GetEventAttachementByEventId(int eventId)
        {
            try
            {
                var oEventAttachement = _uow.GetRepository<EventAttachement>().GetList(x => x.EventId == eventId, null,null, 0, 100000, true);
                var mapper = _mapper.Map <PaginateDto<EventAttachementDto>>(oEventAttachement);
                if (mapper.Items.Count()>0)
                {
                    var lstToSend = GetFullPathAndBinary(mapper);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = lstToSend };
                }
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddEventAttachement(int eventId, IFormFileCollection files)      
        {
            try
            {
                List<EventAttachementDto> eventAttachement = new List<EventAttachementDto>();
                EventAttachmentBinaryDto eventAttachmentBinary ;

                using var transaction = _iesContext.Database.BeginTransaction();
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        eventAttachmentBinary = new EventAttachmentBinaryDto();
                        eventAttachmentBinary.FileBinary = ms.ToArray();
                        ms.Close();
                        ms.Dispose();

                        var result = _ifileService.UploadFile(file);
                       // var result = _ifileService.SaveBinary(file.FileName, eventAttachmentBinary.FileBinary);

                        eventAttachement.Add(new EventAttachementDto
                        {
                            Id = 0,
                            EventId = eventId,
                            FileName = result.FileName,
                            FullPath = result.virtualPath,
                            EventAttachmentBinary = eventAttachmentBinary
                        });
                    }
                }
                var mapper = _mapper.Map<IEnumerable<EventAttachement>>(eventAttachement);
                _uow.GetRepository<EventAttachement>().Add(mapper);
                _uow.SaveChanges();
                transaction.Commit();

                return new ResponseDto { Status = 1, Message = "Event Attachements Added  Seccessfuly", Data = eventAttachement };
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

        public ResponseDto AddEventStudentAttachement(int eventId, int studentId ,IFormFileCollection? files)
        {
            try
            {
                if ((eventId > 0 && studentId > 0) && (eventId != null && studentId != null))
                {
                    EventStudent eventStudent;
                    List<EventStudentFile> eventStudentFile = new List<EventStudentFile>();
                    EventStudentFileBinary eventStudentFileBinary;

                    using var transaction = _iesContext.Database.BeginTransaction();
                    if (files != null && files.Count > 0)
                    {
                        foreach (var file in files)
                        {
                            if (file.Length > 0)
                            {
                                MemoryStream ms = new MemoryStream();
                                file.CopyTo(ms);
                                eventStudentFileBinary = new EventStudentFileBinary();
                                eventStudentFileBinary.FileBinary = ms.ToArray();
                                ms.Close();
                                ms.Dispose();

                                var result = _ifileService.UploadFile(file);
                                //var result = _ifileService.SaveBinary(file.FileName, eventStudentFileBinary.FileBinary);

                                eventStudentFile.Add(new EventStudentFile
                                {
                                    Id = 0,
                                    EventStudentId = 0,
                                    FileName = result.FileName,
                                    // FullPath = result.virtualPath,
                                    EventStudentFileBinary = eventStudentFileBinary
                                });
                            }

                        }
                        eventStudent = new EventStudent()
                        {
                            EventId = eventId,
                            StudentId = studentId,
                            EventStudentFiles = eventStudentFile,

                        };
                        _uow.GetRepository<EventStudent>().Add(eventStudent);
                        _uow.SaveChanges();
                        transaction.Commit();
                        return new ResponseDto { Status = 1, Message = "Event Student Attachements Added  Seccessfuly", Data = eventStudent };

                    }
                    else if (eventId > 0 && studentId > 0)
                    {
                        eventStudent = new EventStudent()
                        {
                            EventId = eventId,
                            StudentId = studentId,
                            // EventStudentFiles = eventStudentFile,

                        };
                        _uow.GetRepository<EventStudent>().Add(eventStudent);
                        _uow.SaveChanges();
                        transaction.Commit();
                        return new ResponseDto { Status = 1, Message = "Event Student Attachements Added  Seccessfuly", Data = eventStudent };
                    }
                }
                return new ResponseDto { Status = 1, Message = "null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto GetStudentAttatchmentByEventStudenId(int eventStudentId)
        {
            try
            {
                var oEventStudentFile = _uow.GetRepository<EventStudentFile>().GetList(x => x.EventStudentId == eventStudentId, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<EventStudentFileDto>>(oEventStudentFile);

                if (mapper.Items.Count() > 0)
                {
                    var lstToSend = GetFullPathAndBinaryStudentFiles(mapper);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = lstToSend };
                }
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddEventStudentAttatchmentsWithEventStudentId(int eventStudentId, IFormFileCollection files)
        {
            try
            {

                List<EventStudentFile> eventStudentFile = new List<EventStudentFile>();
                EventStudentFileBinary eventStudentFileBinary;

                using var transaction = _iesContext.Database.BeginTransaction();
                if (files.Count()>0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            MemoryStream ms = new MemoryStream();
                            file.CopyTo(ms);
                            eventStudentFileBinary = new EventStudentFileBinary();
                            eventStudentFileBinary.FileBinary = ms.ToArray();
                            ms.Close();
                            ms.Dispose();

                            var result = _ifileService.UploadFile(file);
                           // var result = _ifileService.SaveBinary(file.FileName, eventStudentFileBinary.FileBinary);


                            eventStudentFile.Add(new EventStudentFile
                            {
                                Id = 0,
                                EventStudentId= eventStudentId,
                                FileName = result.FileName,
                                EventStudentFileBinary = eventStudentFileBinary
                            });
                        }
                    }
                    _uow.GetRepository<EventStudentFile>().Add(eventStudentFile);
                    _uow.SaveChanges();
                    transaction.Commit();
                    return new ResponseDto { Status = 1, Message = "Event Student File Added  Seccessfuly", Data = eventStudentFile };

                }
                else
                return new ResponseDto { Status = 1, Message = "null" };
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
        private PaginateDto<EventAttachementDto> GetFullPathAndBinary(PaginateDto<EventAttachementDto> allEventAttachement)
        {
            try
            {
                if (allEventAttachement.Items.Count() > 0)
                {
                    foreach (var item in allEventAttachement.Items)
                    {
                        if (File.Exists("wwwRoot/tempFiles/" + item.FileName))
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                            item.FullPath = fullpath;
                        }
                        else
                        {
                            if (item != null && item.FileName != null)
                            {
                                var att = _uow.GetRepository<EventAttachmentBinary>().Single(x => x.Id == item.Id , null, null);
                                if (att.FileBinary != null)
                                {
                                    var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwRoot/tempFiles");
                                    if (!Directory.Exists(target))
                                    {
                                        Directory.CreateDirectory(target);
                                    }
                                    System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.FileName, att.FileBinary);
                                }
                                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                                item.FullPath = fullpath;
                            }
                        }
                    }
                }
                return allEventAttachement;
            }
            catch (Exception ex)
            {
                return allEventAttachement; ;
            }
        }
        private ICollection<EventAttachementDto> GetFullPathAndBinaryIColliction(ICollection<EventAttachementDto> allEventAttachement)
        {
            try
            {
                if (allEventAttachement.Count() > 0)
                {
                    foreach (var item in allEventAttachement)
                    {
                        if (File.Exists("wwwRoot/tempFiles/" + item.FileName))
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                            item.FullPath = fullpath;
                        }
                        else
                        {
                            if (item != null && item.FileName != null)
                            {
                                var att = _uow.GetRepository<EventAttachmentBinary>().Single(x => x.Id == item.Id , null, null);
                                if (att.FileBinary != null)
                                {
                                    var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwRoot/tempFiles");
                                    if (!Directory.Exists(target))
                                    {
                                        Directory.CreateDirectory(target);
                                    }
                                    System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.FileName, att.FileBinary);
                                }
                                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                                item.FullPath = fullpath;
                            }
                        }
                    }
                }
                return allEventAttachement;
            }
            catch (Exception ex)
            {
                return allEventAttachement; ;
            }
        }
        private PaginateDto<EventStudentFileDto> GetFullPathAndBinaryStudentFiles(PaginateDto<EventStudentFileDto> allEventStudentFiles)
        {
            try
            {
                if (allEventStudentFiles.Items.Count() > 0)
                {
                    foreach (var item in allEventStudentFiles.Items)
                    {
                        if (File.Exists("wwwRoot/tempFiles/" + item.FileName))
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                            item.FullPath = fullpath;
                        }
                        else
                        {
                            if (item != null && item.FileName != null)
                            {
                                var att = _uow.GetRepository<EventStudentFileBinary>().Single(x => x.Id == item.Id, null, null);
                                if (att.FileBinary != null)
                                {
                                    var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwRoot/tempFiles");
                                    if (!Directory.Exists(target))
                                    {
                                        Directory.CreateDirectory(target);
                                    }
                                    System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.FileName, att.FileBinary);
                                }
                                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                                item.FullPath = fullpath;
                            }
                        }
                    }
                }
                return allEventStudentFiles;
            }
            catch (Exception ex)
            {
                return allEventStudentFiles; ;
            }
        }
        private ICollection<EventStudentFileDto> GetFullPathAndBinaryStudentFilesICollection(ICollection<EventStudentFileDto> allEventStudentFiles)
        {
            try
            {
                if (allEventStudentFiles.Count() > 0)
                {
                    foreach (var item in allEventStudentFiles)
                    {
                        if (File.Exists("wwwRoot/tempFiles/" + item.FileName))
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                            item.FullPath = fullpath;
                        }
                        else
                        {
                            if (item != null && item.FileName != null)
                            {
                                var att = _uow.GetRepository<EventStudentFileBinary>().Single(x => x.Id == item.Id, null, null);
                                if (att.FileBinary != null)
                                {
                                    var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwRoot/tempFiles");
                                    if (!Directory.Exists(target))
                                    {
                                        Directory.CreateDirectory(target);
                                    }
                                    System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.FileName, att.FileBinary);
                                }
                                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                                item.FullPath = fullpath;
                            }
                        }
                    }
                }
                return allEventStudentFiles;
            }
            catch (Exception ex)
            {
                return allEventStudentFiles; ;
            }
        }
        #region NotNeededNow
        //public ResponseDto GetEventStudentById(int eventStudentId)
        //{
        //    try
        //    {
        //        var eventStudent = _uow.GetRepository<EventStudent>().Single(x => x.Id == eventStudentId, null, x => x.Include(x => x.EventStudentFiles));
        //        var mapper = _mapper.Map<EventStudentDto>(eventStudent);
        //        return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
        //    }
        //}
        //public ResponseDto AddEventStudent(List<EventStudentDto> eventStudentDto)
        //{
        //    try
        //    {
        //        List<EventStudent> eventStudent = new List<EventStudent>();
        //        foreach (var item in eventStudentDto)
        //            eventStudent.Add(new EventStudent { EventId = item.EventId, StudentId = item.StudentId });

        //        _uow.GetRepository<EventStudent>().Add(eventStudent);
        //        _uow.SaveChanges();
        //        eventStudentDto.Id = mapper.Id;
        //        return new ResponseDto { Status = 1, Message = "Event Student Added  Seccessfuly", Data = eventStudentDto };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }
        //}
        //public ResponseDto EditEventStudent(List<EventStudentDto> eventStudentDto)
        //{
        //    try
        //    {
        //        using var transaction = _iesContext.Database.BeginTransaction();
        //        var cmd = $"delete from Event_Student where EventId={eventStudentDto.FirstOrDefault().EventId}";
        //        _iesContext.Database.ExecuteSqlRaw(cmd);
        //        List<EventStudent> eventStudent = new List<EventStudent>();

        //        foreach (var item in eventStudentDto)
        //            eventStudent.Add(new EventStudent { EventId = item.EventId, StudentId = item.StudentId });

        //        _uow.GetRepository<EventStudent>().Update(eventStudent);
        //        _uow.SaveChanges();
        //        transaction.Commit();
        //        return new ResponseDto { Status = 1, Message = "Event Student Updated Seccessfuly", Data = eventStudentDto };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }
        //}
        //public ResponseDto GetEventTeachers()
        //{
        //    try
        //    {
        //        var allEventTeachers = _uow.GetRepository<EventTeacher>().GetList(null, null, null, 0, 100000, true);
        //        var mapper = _mapper.Map<PaginateDto<EventTeacherDto>>(allEventTeachers);
        //        return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
        //    }
        //}
        //public ResponseDto GetEventTeacherById(int eventTeacherId)
        //{
        //    try
        //    {
        //        var eventTeacher = _uow.GetRepository<EventTeacher>().Single(x => x.Id == eventTeacherId, null);
        //        var mapper = _mapper.Map<EventTeacherDto>(eventTeacher);
        //        return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
        //    }
        //}
        //public ResponseDto AddEventTeacher(List<EventTeacherDto> eventTeacherDto)
        //{
        //    try
        //    {
        //        List<EventTeacher> eventTeacher = new List<EventTeacher>();
        //        foreach (var item in eventTeacherDto)
        //            eventTeacher.Add(new EventTeacher { EventId = item.EventId, TeacherId = item.TeacherId });

        //        _uow.GetRepository<EventTeacher>().Add(eventTeacher);
        //        _uow.SaveChanges();
        //        return new ResponseDto { Status = 1, Message = "Event Teacher Added  Seccessfuly", Data = eventTeacherDto };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }
        //}
        //public ResponseDto EditEventTeacher(List<EventTeacherDto> eventTeacherDto)
        //{
        //    try
        //    {
        //        using var transaction = _iesContext.Database.BeginTransaction();
        //        var cmd = $"delete from Event_Teacher where EventId={eventTeacherDto.FirstOrDefault().EventId}";
        //        _iesContext.Database.ExecuteSqlRaw(cmd);
        //        List<EventTeacher> eventTeacher = new List<EventTeacher>();

        //        foreach (var item in eventTeacherDto)
        //            eventTeacher.Add(new EventTeacher { EventId = item.EventId, TeacherId = item.TeacherId });

        //        _uow.GetRepository<EventTeacher>().Update(eventTeacher);
        //        _uow.SaveChanges();
        //        transaction.Commit();
        //        return new ResponseDto { Status = 1, Message = "Event Teacher Updated Seccessfuly", Data = eventTeacherDto };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }
        //}
        //public ResponseDto DeleteEventTeacher(int eventTeacherId)
        //{
        //    try
        //    {
        //        var cmd = $"delete from Event_Teacher where Id={eventTeacherId}";
        //        _iesContext.Database.ExecuteSqlRaw(cmd);
        //        return new ResponseDto { Status = 1, Message = "Event Teacher Deleted Seccessfuly" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }
        //}
        //public ResponseDto EditEventAttachement(List<EventAttachementDto> eventAttachementDto)
        //{
        //    try
        //    {
        //        /// not use this func
        //        //        using var transaction = _iesContext.Database.BeginTransaction();
        //        //        List<EventAttachement> eventAttachement = new List<EventAttachement>();

        //        //        foreach (var item in eventAttachementDto)
        //        //            eventAttachement.Add(new EventAttachement
        //        //            {
        //        //                EventId = item.EventId,
        //        //                Name = item.Name,
        //        //                Description = item.Description,
        //        //                Date = item.Date,
        //        //                IsPublished = item.IsPublished,
        //        //                FileName = item.FileName,
        //        //            });
        //        //        var mapper = _mapper.Map<List<EventAttachement>>(eventAttachementDto);
        //        //        _uow.GetRepository<EventAttachement>().Update(mapper);
        //        //        _uow.SaveChanges();
        //        //        transaction.Commit();
        //        return new ResponseDto { Status = 1, Message = "Event Attachement Updated Seccessfuly", Data = eventAttachementDto };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }
        //}

        #endregion
    }
}