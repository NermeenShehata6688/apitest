using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IesSchool.Core.Services
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        private IFileService _ifileService;
      

        private readonly UserManager<IdentityUser<int>> _userManager;

        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<IdentityUser<int>> userManage,
            IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext,
            IFileService ifileService, IHostingEnvironment hostingEnvironment, 
            IHttpContextAccessor httpContextAccessor
           )
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
            _ifileService = ifileService;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManage;
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
                   AllAttachmentTypes= _uow.GetRepository<AttachmentType>().GetList(null, null, null, 0, 1000000, true),
                   AllExtraCurriculars = _uow.GetRepository<ExtraCurricular>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true)
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
                if (userSearchDto.Index == null || userSearchDto.Index == 0)
                {
                    userSearchDto.Index = 0;
                }
                else
                {
                    userSearchDto.Index += 1;
                }
                var mapper = new PaginateDto<VwUserDto> { Count = allUsers.Count(), Items = lstToSend != null ? lstUserDto.Skip(userSearchDto.Index == null || userSearchDto.PageSize == null ? 0 : ((userSearchDto.Index.Value - 1) * userSearchDto.PageSize.Value)).Take(userSearchDto.PageSize ??= 20).ToList() : lstToSend.ToList() };
               // var mapper = new PaginateDto<VwUserDto> { Count = allUsers.Count(), Items = lstToSend, Index = userSearchDto.Index, Pages = userSearchDto.PageSize };
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
                var teacherAssignedStudents = _uow.GetRepository<Student>().GetList((x => new Student { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code, DateOfBirth = x.DateOfBirth }),x => x.IsDeleted != true && x.TeacherId == teacherId, x => x.OrderBy(c => c.Name), null, 0, 100000, true);
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
                x.Include(x => x.StudentTherapists).Include(x=>x.AspNetUser)
                .Include(x => x.UserAttachments).ThenInclude(x => x.AttachmentType)
                .Include(x => x.UserAssistants).Include(x => x.TherapistParamedicalServices)
                .Include(x => x.UserExtraCurriculars).ThenInclude(x => x.ExtraCurricular)
                .Include(x => x.StudentExtraTeachers)
                .Include(x => x.AspNetUser)
                .Include(x => x.StudentParents));
                if (user!=null)
                {
                    user.ImageBinary = null;
                }
                var mapper = _mapper.Map<UserDto>(user);
                //if (user.AspNetUser != null)
                //{
                //    mapper.UserPassword = user.AspNetUser.PasswordHash;
                //}

                var appUser = _userManager.Users.FirstOrDefault(r => r.Email == user.Email);
                if (appUser!= null)
                {
                    var roles = _userManager.GetRolesAsync(appUser).Result;
                    if (roles.Count() > 0)
                    {
                        mapper.UserRoles = string.Join(Environment.NewLine, roles);
                    }
                }
                if (mapper.UserAttachments.Count()>0)
                {
                    mapper.UserAttachments = GetFullPathAndBinaryICollictionAtt(mapper.UserAttachments);
                }

                mapper.UserName= user.AspNetUser == null ? "" : user.AspNetUser.UserName==null? "": user.AspNetUser.UserName;
                mapper.Email= user.AspNetUser == null ? "" : user.AspNetUser.Email ==null?"": user.AspNetUser.Email;
                if (mapper.Image!=null)
                {
                    string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                    var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{mapper.Image}";
                    mapper.FullPath = fullpath;
                }
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public UserDto GetJustUserById(int userId)
        {
            try
            {
                var user = _uow.GetRepository<User>().Single(x => x.Id == userId && x.IsDeleted != true, null);
               
                if (user != null)
                {
                    user.ImageBinary = null;
                }
                var mapper = _mapper.Map<UserDto>(user);
              
                if (mapper.Image != null)
                {
                    string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                    var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{mapper.Image}";
                    mapper.FullPath = fullpath;
                }
                return mapper;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseDto>  AddUser (IFormFile file, UserDto userDto)
        {
            using var transaction = _iesContext.Database.BeginTransaction();

            try
            {

                if (userDto != null)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    userDto.ImageBinary = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                    //upload file in local directory
                    var result = _ifileService.UploadFile(file);
                    userDto.Image = result.FileName;
                    var mapper = _mapper.Map<User>(userDto);
                    mapper.IsDeleted = false;
                    mapper.CreatedOn = DateTime.Now;
                    mapper.IsSuspended = false;

                   
                    _uow.GetRepository<User>().Add(mapper);
                    _uow.SaveChanges();
                    if (userDto.StudentsIdsForParent != null && userDto.StudentsIdsForParent.Count() > 0)
                    {
                        var student = _uow.GetRepository<Student>().GetList(x => x.IsDeleted != true && userDto.StudentsIdsForParent.Contains(x.Id), null).Items;
                        string numbersToUpdate = string.Join(",", userDto.StudentsIdsForParent);
                        
                        var cmd = $"UPDATE Students SET Students.ParentId = { mapper.Id} Where Id IN ({numbersToUpdate})";
                        _iesContext.Database.ExecuteSqlRaw(cmd);
                    }
                    userDto.Id = mapper.Id;
                    var user = new IdentityUser<int>
                    {
                        UserName = userDto.UserName,
                        Email = userDto.Email,
                        Id = mapper.Id
                        //Email = model.Email,
                    };
                    //transaction.CreateSavepoint("AfterSavingUser");

                    transaction.Commit();

                    var resultt = await _userManager.CreateAsync(user, userDto.UserPassword);

                    if (resultt.Succeeded)
                    {
                        //_iaplicationGroupService.AddGroupToUser(model.Roles.Select(x => x.Id).ToArray(), user.Id);
                        //return Ok(new ResponseDto { Status = 1, Message = "تم تسجيل المستخدم بنجاح!" });
                    }
                    else
                    {
                        var cmd = $"delete from [dbo].[User] where Id={userDto.Id}";
                        _ = _iesContext.Database.ExecuteSqlRaw(cmd);

                    }
                    //_uow.SaveChanges();
                    userDto.Id = mapper.Id;
                    userDto.ImageBinary = null;
                    userDto.FullPath = result.virtualPath;

                    return new ResponseDto { Status = 1, Message = "User Added  Seccessfuly", Data = userDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };

            }
            catch (Exception ex)
            {
                //transaction.RollbackToSavepoint("AfterSavingUser");
                var cmd = $"delete from [dbo].[User] where Id={userDto.Id}";
                _ = _iesContext.Database.ExecuteSqlRaw(cmd);

                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }   
        public async Task<ResponseDto> AddUser2( UserDto userDto)
        {
           using var transaction = _iesContext.Database.BeginTransaction();
                try
                {

                    if (userDto != null)
                    {
                   
                    var mapper = _mapper.Map<User>(userDto);
                    mapper.IsDeleted = false;
                    mapper.CreatedOn = DateTime.Now;
                    mapper.IsSuspended = false;
                    _uow.GetRepository<User>().Add(mapper);
                    _uow.SaveChanges();

                    userDto.Id = mapper.Id;
                    if (userDto.StudentsIdsForParent != null && userDto.StudentsIdsForParent.Count() > 0)
                    {
                        var student = _uow.GetRepository<Student>().GetList(x => x.IsDeleted != true && userDto.StudentsIdsForParent.Contains(x.Id), null).Items;
                        string numbersToUpdate = string.Join(",", userDto.StudentsIdsForParent);

                        var cmd = $"UPDATE Students SET Students.ParentId = { mapper.Id} Where Id IN ({numbersToUpdate})";
                        _iesContext.Database.ExecuteSqlRaw(cmd);
                    }

                    var user = new IdentityUser<int>
                    {
                        UserName = userDto.UserName,
                        Email = userDto.Email,
                        Id = mapper.Id
                    };
                    transaction.Commit();

                    var resultt = await _userManager.CreateAsync(user, userDto.UserPassword);

                    if (resultt.Succeeded)
                    {
                        //_iaplicationGroupService.AddGroupToUser(model.Roles.Select(x => x.Id).ToArray(), user.Id);
                        //return Ok(new ResponseDto { Status = 1, Message = "تم تسجيل المستخدم بنجاح!" });
                    }
                    else
                    {
                        var cmd = $"delete from [dbo].[User] where Id={userDto.Id}";
                        _ = _iesContext.Database.ExecuteSqlRaw(cmd);
                        //transaction.RollbackToSavepoint("AfterSavingUser");
                    }

                    userDto.Id = mapper.Id;

                    return new ResponseDto { Status = 1, Message = "User Added  Seccessfuly", Data = userDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };

            }
            catch (Exception ex)
            {
                var cmd = $"delete from [dbo].[User] where Id={userDto.Id}";
                _ = _iesContext.Database.ExecuteSqlRaw(cmd);
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
                        $" delete from Student_Therapist where TherapistId={ userDto.Id}" +
                        $"delete from User_ExtraCurricular where UserId={userDto.Id}" +
                        $" delete from Student_ExtraTeacher where ExtraTeacherId={ userDto.Id}";
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

                    if (userDto.StudentsIdsForParent != null && userDto.StudentsIdsForParent.Count() > 0)
                    {
                        var student = _uow.GetRepository<Student>().GetList(x => x.IsDeleted != true && userDto.StudentsIdsForParent.Contains(x.Id), null).Items;
                        string numbersToUpdate = string.Join(",", userDto.StudentsIdsForParent);

                        var cmd2 = $"UPDATE Students SET Students.ParentId =Null where  Students.ParentId={ mapper.Id}" +
                            $"UPDATE Students SET Students.ParentId = { mapper.Id} Where Id IN ({numbersToUpdate})";
                        _iesContext.Database.ExecuteSqlRaw(cmd2);
                    }
                    else
                    {
                        var cmd2 = $"UPDATE Students SET Students.ParentId =Null where  Students.ParentId={ mapper.Id}" ;
                        _iesContext.Database.ExecuteSqlRaw(cmd2);
                    }

                    //AspNetUser aspNetUser = new AspNetUser();
                    //aspNetUser.Id = userDto.Id;
                    //aspNetUser.UserName = userDto.UserName == null ? "" : userDto.UserName;
                    //aspNetUser.Email = userDto.Email == null ? "" : userDto.Email;
                    //_uow.GetRepository<AspNetUser>().Update(aspNetUser);
                    //_uow.SaveChanges();
                    var AspnetBeforeUpdtate = _uow.GetRepository<AspNetUser>().Single(x => x.Id == mapper.Id);
                    if (AspnetBeforeUpdtate != null)
                    {
                        AspnetBeforeUpdtate.UserName = userDto.UserName == null ? AspnetBeforeUpdtate.UserName : userDto.UserName;
                        AspnetBeforeUpdtate.Email = userDto.Email == null ? AspnetBeforeUpdtate.Email : userDto.Email;
                        _uow.GetRepository<AspNetUser>().Update(AspnetBeforeUpdtate);
                        _uow.SaveChanges();
                    }




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
        public  ResponseDto EditUser2(UserDto userDto)
        {
            try
            {
                if (userDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from User_Assistant where UserId={userDto.Id}" +
                        $"delete from TherapistParamedicalService where UserId={userDto.Id}" +
                        $"delete from UserAttachment where UserId={userDto.Id}" +
                        $" delete from Student_Therapist where TherapistId={ userDto.Id}"+
                         $"delete from User_ExtraCurricular where UserId={userDto.Id}" +
                        $" delete from Student_ExtraTeacher where ExtraTeacherId={ userDto.Id}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);
                    
                    var mapper = _mapper.Map<User>(userDto);
                    _uow.GetRepository<User>().Update(mapper);
                    _uow.SaveChanges();
                    var AspnetBeforeUpdtate = _uow.GetRepository<AspNetUser>().Single(x => x.Id == mapper.Id);
                    if (AspnetBeforeUpdtate!=null)
                    {
                        AspnetBeforeUpdtate.UserName = userDto.UserName == null ? AspnetBeforeUpdtate.UserName : userDto.UserName;
                        AspnetBeforeUpdtate.Email = userDto.Email == null ? AspnetBeforeUpdtate.Email : userDto.Email;
                        _uow.GetRepository<AspNetUser>().Update(AspnetBeforeUpdtate);
                        _uow.SaveChanges();
                    }
                  
                    // AspNetUser aspNetUser = new AspNetUser();
                    //aspNetUser.Id = userDto.Id;
                    //aspNetUser.UserName = userDto.UserName == null ? "" : userDto.UserName;
                    //aspNetUser.Email = userDto.Email == null ? "" : userDto.Email;


                 
                    if (userDto.StudentsIdsForParent != null && userDto.StudentsIdsForParent.Count() > 0)
                    {
                        var student = _uow.GetRepository<Student>().GetList(x => x.IsDeleted != true && userDto.StudentsIdsForParent.Contains(x.Id), null).Items;
                        string numbersToUpdate = string.Join(",", userDto.StudentsIdsForParent);

                        var cmd2 = $"UPDATE Students SET Students.ParentId =Null where  Students.ParentId={ mapper.Id}" +
                            $"UPDATE Students SET Students.ParentId = { mapper.Id} Where Id IN ({numbersToUpdate})";
                        _iesContext.Database.ExecuteSqlRaw(cmd2);
                    }
                    else
                    {

                        var cmd2 = $"UPDATE Students SET Students.ParentId =Null where  Students.ParentId={ mapper.Id}";
                        _iesContext.Database.ExecuteSqlRaw(cmd2);
                    }
                    transaction.Commit();

                    userDto.Id = mapper.Id;
                    userDto.ImageBinary = null;
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
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from User_Assistant where UserId={userId}" +
                    $"delete from TherapistParamedicalService where UserId={userId}" +
                    $"delete from UserAttachment where UserId={userId}" +
                    $" delete from Student_Therapist where TherapistId={userId}" +
                     $"delete from User_ExtraCurricular where UserId={userId}" +
                    $" delete from Student_ExtraTeacher where ExtraTeacherId={ userId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                User user = _uow.GetRepository<User>().Single(x => x.Id == userId);
                user.IsDeleted = true;
                user.DeletedOn = DateTime.Now;

                _uow.GetRepository<User>().Update(user);
                _uow.SaveChanges();
                transaction.Commit();
                return new ResponseDto { Status = 1, Message = "User Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetUserAttachmentByUserId(int userId)
        {
            try
            {
                var userAttachment = _uow.GetRepository<UserAttachment>().GetList(x => x.UserId == userId, null, x=> x.Include(x => x.AttachmentType));
                var mapper = _mapper.Map<PaginateDto<UserAttachmentDto>>(userAttachment);
                
                if (mapper.Items.Count() > 0)
                {
                    mapper = GetFullPathAndBinaryAtt(mapper);
                }
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

        public bool IsEmailExist(string userEmail)
        {
            try
            {
                var user = _uow.GetRepository<User>().GetList(x => x.IsDeleted != true && x.Email == userEmail );
                if (user.Items.Count() >0)
                    return true;

                else 
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool IsUserCodeExist(string UserCode, int? userId)
        {
            try
            {
                if (UserCode != null && userId != null)
                {
                    var user = _uow.GetRepository<User>().GetList(x => x.Code == UserCode && x.Id != userId);
                    if (user.Items.Count() > 0)
                        return true;
                }
                if (UserCode != null && userId == null)
                {
                    var user = _uow.GetRepository<User>().GetList(x => x.Code == UserCode);
                    if (user.Items.Count() > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool IsUserNameExist(string UserName, int? userId)
        {
            try
            {
                if (UserName!= null && userId != null)
                {
                    var user = _uow.GetRepository<AspNetUser>().GetList(x => x.UserName == UserName &&  x.Id != userId);
                    if (user.Items.Count() > 0)
                        return true;
                }
                if (UserName != null && userId == null)
                {
                    var user = _uow.GetRepository<AspNetUser>().GetList(x => x.UserName == UserName);
                    if (user.Items.Count() > 0)
                        return true;
                }
                    return false;
            }
            catch (Exception ex)
            {
                return false;
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

        public ResponseDto GetUserForProfileById(int userId)
        {

            try
            {
                var user = _uow.GetRepository<User>().Single(x=> x.Id== userId && x.IsDeleted!=true, null, x => x.Include(x => x.AspNetUser));
                if (user != null)
                {
                    user.ImageBinary = null;
                }
                var mapper = _mapper.Map<UserDto>(user);
               
                mapper.UserName = user.AspNetUser == null ? "" : user.AspNetUser.UserName == null ? "" : user.AspNetUser.UserName;
                mapper.Email = user.AspNetUser == null ? "" : user.AspNetUser.Email == null ? "" : user.AspNetUser.Email;
                if (mapper.Image != null)
                {
                    string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                    var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{mapper.Image}";
                    mapper.FullPath = fullpath;
                }
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAllParents(UserSearchDto userSearchDto)
        {

            try
            {
                var AllParentss = _uow.GetRepository<User>().GetList(x => x.IsDeleted != true && x.IsParent==true, null);
                var AllParents = _mapper.Map<PaginateDto<UserDto>>(AllParentss).Items;

                if (!string.IsNullOrEmpty(userSearchDto.StringSearch))
                {
                    AllParents = AllParents.Where(x => x.Code.Contains(userSearchDto.StringSearch)
                        || x.Name.Contains(userSearchDto.StringSearch)).ToList();
                }
                if (AllParents != null)
                {
                    AllParents.ToList().ForEach(x => x.ImageBinary = null);
                }
               
                if (userSearchDto.Index == null || userSearchDto.Index == 0)
                {
                    userSearchDto.Index = 0;
                }
                else
                {
                    userSearchDto.Index += 1;
                }


                var mapper = new PaginateDto<UserDto> { Count = AllParents.Count(), Items = AllParents != null ? AllParents.Skip(userSearchDto.Index == null || userSearchDto.PageSize == null ? 0 : ((userSearchDto.Index.Value - 1) * userSearchDto.PageSize.Value)).Take(userSearchDto.PageSize ??= 20).ToList() : AllParents.ToList() };
               
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public bool IsParentUserNameExist(string UserName, int? userId)
        {
            try
            {
                if (UserName != null && userId != null)
                {
                    var user = _uow.GetRepository<User>().GetList(x => x.ParentUserName == UserName && x.Id != userId);
                    if (user.Items.Count() > 0)
                        return true;
                }
                if (UserName != null && userId == null)
                {
                    var user = _uow.GetRepository<User>().GetList(x => x.ParentUserName == UserName);
                    if (user.Items.Count() > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private PaginateDto<UserAttachmentDto> GetFullPathAndBinaryAtt(PaginateDto<UserAttachmentDto> allUserAttachement)
        {
            try
            {
                if (allUserAttachement.Items.Count() > 0)
                {
                    foreach (var item in allUserAttachement.Items)
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
                                var att = _uow.GetRepository<UserAttachmentBinary>().Single(x => x.Id == item.Id, null, null);
                                if (att.FileBinary != null)
                                {
                                    System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.FileName, att.FileBinary);
                                }
                                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                                item.FullPath = fullpath;
                            }
                        }
                    }
                }
                return allUserAttachement;
            }
            catch (Exception ex)
            {
                return allUserAttachement; ;
            }
        }
        private ICollection<UserAttachmentDto> GetFullPathAndBinaryICollictionAtt(ICollection<UserAttachmentDto> allUserAttachement)
        {
            try
            {
                if (allUserAttachement.Count() > 0)
                {
                    foreach (var item in allUserAttachement)
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
                                var att = _uow.GetRepository<UserAttachmentBinary>().Single(x => x.Id == item.Id, null, null);
                                if (att.FileBinary != null)
                                {
                                    System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.FileName, att.FileBinary);
                                }
                                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                                item.FullPath = fullpath;
                            }
                        }
                    }
                }
                return allUserAttachement;
            }
            catch (Exception ex)
            {
                return allUserAttachement; ;
            }
        }

    }
}
