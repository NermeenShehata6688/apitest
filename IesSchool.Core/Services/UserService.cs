﻿using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IesSchool.Core.Services
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        private IFileService _ifileService;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext, IFileService ifileService, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
            _ifileService = ifileService;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseDto GetUsersHelper()
        {
            try
            {
                UserHelper userHelper = new UserHelper()
                {
                    AllDepartments = _uow.GetRepository<Department>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                    AllNationalities = _uow.GetRepository<Country>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 1000000, true),
                    AllAssistants = _uow.GetRepository  <Assistant>().GetList((x => new Assistant { Id = x.Id, Name = x.Name }),x => x.IsDeleted != true , x => x.OrderBy(c => c.Name), null, 0, 1000000, true),
                    AllParamedicalServices = _uow.GetRepository<ParamedicalService>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllStudents = _uow.GetRepository<Student>().GetList((x => new Student { Id = x.Id, Name = x.Name }),x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 1000000, true),
                   AllAttachmentTypes= _uow.GetRepository<AttachmentType>().GetList(null, null, null, 0, 1000000, true)

            };

                var mapper = _mapper.Map<UserHelperDto>(userHelper);



                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }

            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetUsers(UserSearchDto userSearchDto)
        {
            try
            {
                var allUsers = _uow.GetRepository<VwUser>().Query("select * from Vw_Users where IsDeleted != 1");

                if (!string.IsNullOrEmpty(userSearchDto.StringSearch))
                {
                    allUsers = allUsers.Where(x => x.Code.Contains(userSearchDto.StringSearch)
                        || x.Name.Contains(userSearchDto.StringSearch)
                        || x.Code.Contains(userSearchDto.StringSearch)
                        || x.Email.Contains(userSearchDto.StringSearch)
                        || x.RoomNumber.ToString().Contains(userSearchDto.StringSearch));
                }
                if (userSearchDto.NationalityId != null)
                {
                    allUsers = allUsers.Where(x => x.NationalityId == userSearchDto.NationalityId);
                }
                if (userSearchDto.IsTeacher != null)
                {
                    allUsers = allUsers.Where(x => x.IsTeacher == userSearchDto.IsTeacher);
                }
                if (userSearchDto.IsTherapist != null)
                {
                    allUsers = allUsers.Where(x => x.IsTherapist == userSearchDto.IsTherapist);
                }
                if (userSearchDto.IsParent != null)
                {
                    allUsers = allUsers.Where(x => x.IsParent == userSearchDto.IsParent);
                }
                if (userSearchDto.IsManager != null)
                {
                    allUsers = allUsers.Where(x => x.IsManager == userSearchDto.IsManager);
                }
                if (userSearchDto.IsHeadofEducation != null)
                {
                    allUsers = allUsers.Where(x => x.IsHeadofEducation == userSearchDto.IsHeadofEducation);
                }
                if (userSearchDto.IsOther != null)
                {
                    allUsers = allUsers.Where(x => x.IsOther == userSearchDto.IsOther);
                }
                if (userSearchDto.IsExtraCurricular != null)
                {
                    allUsers = allUsers.Where(x => x.IsExtraCurricular == userSearchDto.IsExtraCurricular);
                }
                if (userSearchDto.IsSuspended != null)
                {
                    allUsers = allUsers.Where(x => x.IsSuspended == userSearchDto.IsSuspended);
                }
                if (userSearchDto.DepartmentId != null)
                {
                    allUsers = allUsers.Where(x => x.DepartmentId == userSearchDto.DepartmentId);
                }

                var lstUserDto = _mapper.Map<List<VwUserDto>>(allUsers);
                var lstToSend = GetFullPathAndBinary(lstUserDto);

                var mapper = new PaginateDto<VwUserDto> { Count = allUsers.Count(), Items = lstToSend, Index = userSearchDto.Index, Pages = userSearchDto.PageSize };
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetTeacherAssignedStudents(int teacherId)
        {
            try
            {
                var teacherAssignedStudents = _uow.GetRepository<Student>().GetList((x => new Student { Id = x.Id, Name = x.Name }),x => x.IsDeleted != true && x.TeacherId == teacherId, x => x.OrderBy(c => c.Name), null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<StudentDto>>(teacherAssignedStudents);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetTherapistAssignedStudents(int therapistId)
        {
            try
            {
                var therapistAssignedStudents = _uow.GetRepository<StudentTherapist>().GetList(x => x.TherapistId == therapistId , null, x => x.Include(x => x.Student));
                var mapper = _mapper.Map<PaginateDto<StudentTherapistDto>>(therapistAssignedStudents);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            } 
        }
        public ResponseDto GetTherapistParamedicalServices(int therapistId)
        {
            try
            {
                var therapistParamedicalServics = _uow.GetRepository<TherapistParamedicalService>().GetList(x => x.UserId == therapistId, null, x => x.Include(x => x.ParamedicalService));
                var mapper = _mapper.Map<PaginateDto<TherapistParamedicalServiceDto>>(therapistParamedicalServics);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAllTeachers()
        {
            try
            {
                var allTeachers = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name }),x => x.IsDeleted != true && x.IsTeacher == true, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<UserDto>>(allTeachers);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAllTherapists()
        {
            try
            {
                var allTherapists = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name }),x => x.IsDeleted != true && x.IsTherapist == true, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<UserDto>>(allTherapists);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetUserById(int userId)
        {
            try
            {
                var user = _uow.GetRepository<User>().Single(x => x.Id == userId && x.IsDeleted != true, null, x => 
                x.Include(x => x.StudentTherapists).Include(x=>x.AspNetUser).Include(x => x.UserAssistants).Include(x => x.TherapistParamedicalServices));
                user.ImageBinary = null;
                var mapper = _mapper.Map<UserDto>(user);
                mapper.UserName= user.AspNetUser.UserName;
                mapper.Email= user.AspNetUser.Email;
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{mapper.Image}";
                mapper.FullPath = fullpath;
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddUser(IFormFile file, UserDto userDto)
        {
            try
            {

                if (userDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();

                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    userDto.ImageBinary = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                    //upload file in local directory
                    AspNetUser aspNetUser = new AspNetUser();
                    var result = _ifileService.UploadFile(file);
                    userDto.Image = result.FileName;
                    var mapper = _mapper.Map<User>(userDto);
                    mapper.IsDeleted = false;
                    mapper.CreatedOn = DateTime.Now;
                    mapper.IsSuspended = false;
                    _uow.GetRepository<User>().Add(mapper);
                    _uow.SaveChanges();

                    aspNetUser.Id = mapper.Id;
                    aspNetUser.EmailConfirmed = false;
                    aspNetUser.PhoneNumberConfirmed = false;
                    aspNetUser.TwoFactorEnabled = false;
                    aspNetUser.LockoutEnabled = false;
                    aspNetUser.AccessFailedCount = 0;
                    _uow.GetRepository<AspNetUser>().Add(aspNetUser);
                    _uow.SaveChanges();

                    userDto.Id = mapper.Id;
                    userDto.ImageBinary = null;
                    userDto.FullPath = result.virtualPath;
                    transaction.Commit();

                    return new ResponseDto { Status = 1, Message = "User Added  Seccessfuly", Data = userDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }   
        public ResponseDto AddUser2( UserDto userDto)
        {
            try
            {

                if (userDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();

                    //MemoryStream ms = new MemoryStream();
                    //file.CopyTo(ms);
                    //userDto.ImageBinary = ms.ToArray();
                    //ms.Close();
                    //ms.Dispose();
                    //upload file in local directory
                    AspNetUser aspNetUser = new AspNetUser();
                    //var result = _ifileService.UploadFile(file);
                    //userDto.Image = result.FileName;
                    var mapper = _mapper.Map<User>(userDto);
                    mapper.IsDeleted = false;
                    mapper.CreatedOn = DateTime.Now;
                    mapper.IsSuspended = false;
                    _uow.GetRepository<User>().Add(mapper);
                    _uow.SaveChanges();

                    aspNetUser.Id = mapper.Id;
                    aspNetUser.EmailConfirmed = false;
                    aspNetUser.PhoneNumberConfirmed = false;
                    aspNetUser.TwoFactorEnabled = false;
                    aspNetUser.LockoutEnabled = false;
                    aspNetUser.AccessFailedCount = 0;
                    _uow.GetRepository<AspNetUser>().Add(aspNetUser);
                    _uow.SaveChanges();

                    userDto.Id = mapper.Id;
                    //userDto.ImageBinary = null;
                    //userDto.FullPath = result.virtualPath;
                    transaction.Commit();

                    return new ResponseDto { Status = 1, Message = "User Added  Seccessfuly", Data = userDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditUser(IFormFile file, UserDto userDto)
        {
            try
            {
                if (userDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from User_Assistant where UserId={userDto.Id}" +
                        $"delete from TherapistParamedicalService where UserId={userDto.Id}" +
                        $"delete from UserAttachment where UserId={userDto.Id}" +
                        $" delete from Student_Therapist where TherapistId={ userDto.Id}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);
                    //change image to binary
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    userDto.ImageBinary = ms.ToArray();
                    ms.Close();
                    ms.Dispose();

                    //upload file in local directory
                    var result = _ifileService.UploadFile(file);

                    userDto.Image = result.FileName;
                    var mapper = _mapper.Map<User>(userDto);

                    _uow.GetRepository<User>().Update(mapper);
                    _uow.SaveChanges();

                    AspNetUser aspNetUser = new AspNetUser();
                    aspNetUser.Id = userDto.Id;
                    aspNetUser.UserName = userDto.Name;
                    aspNetUser.Email = userDto.Email;
                    _uow.GetRepository<AspNetUser>().Update(aspNetUser);
                    _uow.SaveChanges();
                    transaction.Commit();

                    userDto.Id = mapper.Id;
                    userDto.ImageBinary = null;
                    userDto.FullPath = result.virtualPath;
                    userDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Student Updated Seccessfuly", Data = userDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteUser(int userId)
        {
            try
            {
                User user = _uow.GetRepository<User>().Single(x => x.Id == userId);
                user.IsDeleted = true;
                user.DeletedOn = DateTime.Now;

                _uow.GetRepository<User>().Update(user);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "User Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetUserAttachmentById(int userAttachmentId)
        {
            try
            {
                var userAttachment = _uow.GetRepository<UserAttachment>().Single(x => x.Id == userAttachmentId, null);
                var mapper = _mapper.Map<UserAttachmentDto>(userAttachment);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddUserAttachment(IFormFile file, UserAttachmentDto userAttachmentDto)
        {
            try
            {
                //change File to binary
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                UserAttachmentBinary userAttachmentBinary = new UserAttachmentBinary();
                userAttachmentBinary.FileBinary = ms.ToArray();
                ms.Close();
                ms.Dispose();

                //upload file in local directory
                var result = _ifileService.UploadFile(file);
                userAttachmentDto.FileName = result.FileName;

                //saving to DataBase
                var mapper = _mapper.Map<UserAttachment>(userAttachmentDto);
                mapper.UserAttachmentBinary = userAttachmentBinary;
                _uow.GetRepository<UserAttachment>().Add(mapper);
                _uow.SaveChanges();

                return new ResponseDto { Status = 1, Message = "User Attachment Added  Seccessfuly", Data = userAttachmentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditUserAttachment(UserAttachmentDto userAttachmentDto)
        {
            try
            {
                var mapper = _mapper.Map<UserAttachment>(userAttachmentDto);
                _uow.GetRepository<UserAttachment>().Update(mapper);
                _uow.SaveChanges();
                userAttachmentDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "User Attachment Updated Seccessfuly", Data = userAttachmentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteUserAttachment(int userAttachmentId)
        {
            try
            {
                var cmd = $"delete from UserAttachment where Id={userAttachmentId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "User Attachment Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto IsSuspended(int usertId, bool isSuspended)
        {
            try
            {
                User user = _uow.GetRepository<User>().Single(x => x.Id == usertId);
                user.IsSuspended = isSuspended;
                _uow.GetRepository<User>().Update(user);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "User Is Suspended State Has Changed" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public List<VwUserDto> GetFullPathAndBinary(List<VwUserDto> allUsers )
        {
            try
            {
                if (allUsers.Count() > 0)
                {
                    foreach (var item in allUsers)
                    {
                        if (File.Exists("wwwRoot/tempFiles/" + item.Image))
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;

                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.Image}";
                            //var target = Path.Combine(Environment.CurrentDirectory, "wwwRoot/tempFiles"+$"{item.Image}");
                            item.FullPath = fullpath;
                        }
                        else
                        {
                            if (item != null && item.Image != null)
                            {
                                var user = _uow.GetRepository<User>().Single(x => x.Id == item.Id && x.IsDeleted != true, null, null);
                                if (user.ImageBinary != null)
                                {
                                    System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.Image, user.ImageBinary);
                                }
                                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.Image}";
                                item.FullPath = fullpath;
                            }
                        }

                    }
                }
                return allUsers;
            }
            catch (Exception ex)
            {
                return allUsers; 
            }
        }
    }
}
