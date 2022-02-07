using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class MobileService : IMobileService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MobileService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public bool IsParentExist(string UserName, string Password)
        {
            try
            {
                if (UserName != null && Password != null)
                {
                    // obj.Password.CompareTo(pass) == 0/string.Equals
                    var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && String.Compare( x.ParentPassword,Password)==0 );
                   // var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.Equals( Password, StringComparison.Ordinal) && x.IsSuspended != true);
                   // var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.CompareTo( Password)==0 && x.IsSuspended != true);
                    if (user != null)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ResponseDto Login(string UserName, string Password)
        {
            try
            {
                if (UserName != null && Password != null)
                {
                    // obj.Password.CompareTo(pass) == 0/string.Equals
                    //var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && String.Compare(x.ParentPassword, Password) == 0 && x.IsSuspended != true);
                     var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.Equals( Password, StringComparison.Ordinal) && x.IsSuspended != true);
                    // var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.CompareTo( Password)==0 && x.IsSuspended != true);
                    if (user != null)
                        return new ResponseDto { Status = 1, Message = " Seccess", Data = user };
                    else
                        return new ResponseDto { Status = 0, Message = " null" };

                }
                return new ResponseDto { Status = 0, Message = " null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };

            }
        }
        public ResponseDto GetParentById(int parentId)
        {
            try
            {
                var parent = _uow.GetRepository<User>().Single(x => x.Id == parentId && x.IsDeleted != true);
                var mapper = _mapper.Map<ParentDto>(parent);

                if (parent != null)
                {
                    parent.ImageBinary = null;
                    var parentStudents = _uow.GetRepository<Student>().GetList(x => x.ParentId == parent.Id && x.IsDeleted != true);
                    if (parentStudents.Items.Count()>0)
                    {
                        var mapperStudent = _mapper.Map<PaginateDto<StudentDto>>(parentStudents).Items;
                        if (mapperStudent.Count()>0)
                        {
                            mapper.Students = mapperStudent;
                        }
                    }
                    if (mapper.Image!=null)
                    {
                        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                        var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{mapper.Image}";
                        mapper.FullPath = fullpath;
                    }
                }

                
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetParentStudents(int parentId)
        {
            try
            {
                var students = _uow.GetRepository<Student>().GetList((x => new Student { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code, Image = x.Image }),x => x.ParentId == parentId && x.IsDeleted != true, null,null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<StudentDto>>(students);

                if (mapper.Items.Count()>0&& mapper != null)
                {
                    foreach (var item in mapper.Items)
                    {
                        if (item.Image != null)
                        {
                            item.ImageBinary = null;
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.Image}";
                            item.FullPath = fullpath;
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
        public ResponseDto GetStudentsEventsByParentId(int parentId)
        {
            try
            {
                var studentsIds = _uow.GetRepository<Student>().GetList( x => x.ParentId == parentId && x.IsDeleted != true, null, null, 0, 100000, true).Items.Select(x=> x.Id).ToArray();

                var eventStudentIds = _uow.GetRepository<EventStudent>().GetList( x => studentsIds.Contains(x.StudentId.Value == null ? 0 : x.StudentId.Value) , null, null, 0, 100000, true).Items.Select(x=> x.Id).ToArray();

                var eventStudentFiles = _uow.GetRepository<EventStudentFile>().GetList(x => eventStudentIds.Contains(x.EventStudentId.Value == null ? 0 : x.EventStudentId.Value), null,
                  x => x.Include(x => x.EventStudent).ThenInclude(x => x.Event) );

                var mapper = _mapper.Map<PaginateDto<EventStudentFileDto>>(eventStudentFiles);
                if (mapper.Items.Count() > 0 && mapper != null)
                {
                    foreach (var item in mapper.Items)
                    {
                        if (item.FileName != null)
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                            item.FullPath = fullpath;
                        }
                    }
                }
                var mapperData = mapper.Items.GroupBy(x => x.EventName)
                                                .OrderByDescending(x => x.Key)
                                                .Select(evt => new
                                                {
                                                    EventName = evt.Key,
                                                    EventAttachement = evt.OrderBy(x => x.EventName)
                                                });
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapperData };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetEventsImageGroubedByEventId()
        {
            try
            {
                var eventsImage = _uow.GetRepository<EventAttachement>().GetList(null, null, x=> x.Include(x=>x.Event), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<EventAttachementDto>>(eventsImage);

                if (mapper.Items.Count > 0)
                {
                    mapper = GetFullPath(mapper);

                    var mapperData = mapper.Items.GroupBy(x => x.EventName)
                                                 .OrderByDescending(x => x.Key)
                                                 .Select(evt => new
                                                 {
                                                     EventName = evt.Key,
                                                     EventAttachement = evt.OrderBy(x => x.EventId)
                                                 });
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapperData };
                }
                else
                    return new ResponseDto { Status = 1, Message = " null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetStudentIepsItpsIxps(int studentId)
        {
            try
            {
                IepsItpsIxpsDto iepsItpsIxpsDto = new IepsItpsIxpsDto();
                var iep = _uow.GetRepository<Iep>().GetList(x => x.StudentId == studentId && x.IsDeleted != true && x.IsPublished == true, null, x => x.Include(x => x.Student)
                    .Include(x => x.AcadmicYear).Include(x => x.Term), 0, 100000, true);
                var iepMapper = _mapper.Map<PaginateDto<GetIepDto>>(iep).Items;

                    iepsItpsIxpsDto.Ieps = iepMapper;


                var AllItps = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true && x.StudentId == studentId && x.IsPublished == true, null,
                   x => x.Include(s => s.Student)
                    .Include(s => s.Therapist)
                    .Include(s => s.AcadmicYear)
                    .Include(s => s.Term)
                    .Include(s => s.ParamedicalService), 0, 100000, true);
                var itpsMapper = _mapper.Map<PaginateDto<ItpDto>>(AllItps).Items;

                iepsItpsIxpsDto.Itps = itpsMapper;


                var AllIxpsx = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true && x.StudentId == studentId && x.IsPublished == true, null,
                   x => x.Include(s => s.Student)
                    .Include(s => s.AcadmicYear)
                    .Include(s => s.Term)
                    .Include(s => s.IxpExtraCurriculars).ThenInclude(s => s.ExtraCurricular), 0, 100000, true);
                var ixpMapper = _mapper.Map<PaginateDto<IxpDto>>(AllIxpsx).Items;
               
                iepsItpsIxpsDto.Ixps = ixpMapper;

                return new ResponseDto { Status = 1, Message = " Seccess", Data = iepsItpsIxpsDto };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        private PaginateDto<EventAttachementDto> GetFullPath(PaginateDto<EventAttachementDto> allEventAttachement)
        {
            try
            {
                if (allEventAttachement.Items.Count() > 0)
                {
                    foreach (var item in allEventAttachement.Items)
                    {
                        if (item.FileName != null)
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                            item.FullPath = fullpath;
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

    }
}
