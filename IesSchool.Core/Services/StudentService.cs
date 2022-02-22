using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using IesSchool.InfraStructure.Paging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IesSchool.Core.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        private IFileService _ifileService;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext, IFileService ifileService, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
            _ifileService = ifileService; 
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseDto GetStudents(StudentSearchDto studentSearchDto)
        {
            try
            {
                var allStudents = _uow.GetRepository<VwStudent>().Query("select * from Vw_Students where IsDeleted <> 1");

                if (!string.IsNullOrEmpty(studentSearchDto.StringSearch))
                {
                    allStudents = allStudents.Where(x => x.NameAr.Contains(studentSearchDto.StringSearch)
                        || x.Name.Contains(studentSearchDto.StringSearch)
                        || x.Code.ToString().Contains(studentSearchDto.StringSearch)
                       );
                }
                if (studentSearchDto.NationalityId != null)
                {
                    allStudents = allStudents.Where(x => x.NationalityId == studentSearchDto.NationalityId);
                }
                if (studentSearchDto.DepartmentId != null)
                {
                    allStudents = allStudents.Where(x => x.DepartmentId == studentSearchDto.DepartmentId);
                }
                if (studentSearchDto.TeacherId != null)
                {
                    allStudents = allStudents.Where(x => x.TeacherId == studentSearchDto.TeacherId);
                }
                if (studentSearchDto.StateId != null)
                {
                    allStudents = allStudents.Where(x => x.StateId == studentSearchDto.StateId);
                }
                if (studentSearchDto.CityId != null)
                {
                    allStudents = allStudents.Where(x => x.CityId == studentSearchDto.CityId);
                }
                if (studentSearchDto.IsSuspended != null)
                {
                    allStudents = allStudents.Where(x => x.IsSuspended == studentSearchDto.IsSuspended);
                }
                if (studentSearchDto.IsActive != null)
                {
                    allStudents = allStudents.Where(x => x.IsActive == studentSearchDto.IsActive);
                }

                var lstStudentDto = _mapper.Map<List<VwStudentDto>>(allStudents);
                var lstToSend = GetFullPathAndBinary(lstStudentDto);

                if (studentSearchDto.Index == null || studentSearchDto.Index == 0)
                {
                    studentSearchDto.Index = 0;
                }
                else
                {
                    studentSearchDto.Index += 1;
                }
                var mapper = new PaginateDto<VwStudentDto> { Count = allStudents.Count(), Items = lstToSend != null ? lstStudentDto.Skip(studentSearchDto.Index == null || studentSearchDto.PageSize == null ? 0 : ((studentSearchDto.Index.Value - 1) * studentSearchDto.PageSize.Value)).Take(studentSearchDto.PageSize ??= 20).ToList() : lstToSend.ToList() };

                //var mapper = new PaginateDto<VwStudentDto> { Count = allStudents.Count(), Items = lstStudentDto != null ? lstStudentDto.Skip(studentSearchDto.Index == null || studentSearchDto.PageSize == null ? 0 : ((studentSearchDto.Index.Value - 1) * studentSearchDto.PageSize.Value)).Take(studentSearchDto.PageSize ??= 20).ToList() : lstStudentDto.ToList() };
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetStudentHelper()
        {
            try
            {
                StudentHelper studentHelper = new StudentHelper()
                {
                    AllDepartments = _uow.GetRepository<Department>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                    AllTeachers = _uow.GetRepository<VwUser>().GetList((x => new VwUser { Id = x.Id, Name = x.Name, RoomNumber = x.RoomNumber }), x => x.IsDeleted != true && x.IsTeacher == true, null, null, 0, 1000000, true),
                    AllTherapists = _uow.GetRepository<VwUser>().GetList((x => new VwUser { Id = x.Id, Name = x.Name, RoomNumber = x.RoomNumber }), x => x.IsDeleted != true && x.IsTherapist == true, null, null, 0, 1000000, true),
                    AllNationalities = _uow.GetRepository<Country>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllStates = _uow.GetRepository<State>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 1000000, true),
                    AllAreas = _uow.GetRepository<City>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 1000000, true),
                    AllSkills = _uow.GetRepository<Skill>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(x => x.Strand).ThenInclude(x => x.Area).Include(x => x.SkillAlowedDepartments).ThenInclude(x => x.Department), 0, 1000000, true),
                    AllWorkCategorys = _uow.GetRepository<WorkCategory>().GetList(null, null, null, 0, 1000000, true),
                    AllAttachmentTypes = _uow.GetRepository<AttachmentType>().GetList(null, null, null, 0, 1000000, true),
                    AllReligions = _uow.GetRepository<Religion>().GetList(null, null, null, 0, 1000000, true),
                };
                var mapper = _mapper.Map<StudentHelperDto>(studentHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }

            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetStudentById(int studentId)
        {
            try
            {
                if (studentId != null)
                {
                    var student = _uow.GetRepository<Student>().Single(x => x.Id == studentId && x.IsDeleted != true, null, x => x.Include(x => x.Phones)
                    .Include(x => x.StudentAttachments).ThenInclude(x => x.AttachmentType)
                    .Include(x => x.StudentHistoricalSkills).Include(x => x.StudentTherapists));
                    if (student != null)
                    {
                        student.ImageBinary = null;


                        var mapper = _mapper.Map<StudentDetailsDto>(student);

                        if (mapper.StudentAttachments.Count() > 0)
                        {
                            mapper.StudentAttachments = GetFullPathAndBinaryICollictionAtt(mapper.StudentAttachments);
                        }
                        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                        var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{mapper.Image}";
                        mapper.FullPath = fullpath;
                        return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                    }
                    else
                        return new ResponseDto { Status = 0, Message = "null" };
                }
                else
                    return new ResponseDto { Status = 0, Message = "null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddStudent(StudentDto studentDto, IFormFile? file)
        {
            try
            {
                //change image to binary
                if (studentDto != null)
                {
                    if (file!= null)
                    {
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        studentDto.ImageBinary = ms.ToArray();
                        ms.Close();
                        ms.Dispose();
                        //upload file in local directory
                        // var result = _ifileService.UploadFile(file);
                        var result = _ifileService.SaveBinary(file.FileName, studentDto.ImageBinary);

                        studentDto.Image = result.FileName;
                        studentDto.FullPath = result.virtualPath;
                    }
                    
                    var mapper = _mapper.Map<Student>(studentDto);
                    mapper.IsDeleted = false;
                    mapper.CreatedOn = DateTime.Now;
                    mapper.IsSuspended = false;
                    _uow.GetRepository<Student>().Add(mapper);
                    _uow.SaveChanges();
                    studentDto.Id = mapper.Id;
                    studentDto.ImageBinary = null;
                    return new ResponseDto { Status = 1, Message = "Student Added  Seccessfuly", Data = studentDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditStudent(StudentDto studentDto, IFormFile? file)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from Student_Therapist where StudentId={studentDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                //change image to binary
                if (file != null)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    studentDto.ImageBinary = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                    //var result = _ifileService.UploadFile(file);
                    var result = _ifileService.SaveBinary(file.FileName, studentDto.ImageBinary);

                    studentDto.Image = result.FileName;
                    studentDto.FullPath = result.virtualPath;
                }
                else if (file == null && studentDto.Image== null) 
                {

                    studentDto.Image = null;
                    studentDto.ImageBinary = null;
                }
                else if (file == null && studentDto.Image != null)
                {
                    string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                    studentDto.FullPath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{studentDto.Image}";
                }
                var mapper = _mapper.Map<Student>(studentDto);
                mapper.IsDeleted = false;
                _uow.GetRepository<Student>().Update(mapper);
                _uow.SaveChanges();
                transaction.Commit();

                studentDto.Id = mapper.Id;
                studentDto.ImageBinary = null;
               // studentDto.FullPath = result.virtualPath;
                return new ResponseDto { Status = 1, Message = "Student Updated Seccessfuly", Data = studentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteStudent(int studentId)
        {
            try
            {
                Student oStudent = _uow.GetRepository<Student>().Single(x => x.Id == studentId);
                oStudent.IsDeleted = true;
                oStudent.DeletedOn = DateTime.Now;

                _uow.GetRepository<Student>().Update(oStudent);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Student Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto GetStudentIeps(int studentId)
        {
            try
            {
                var allStudentIeps = _uow.GetRepository<VwIep>().GetList(x => x.IsDeleted != true&& x.StudentId== studentId, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<VwIepDto>>(allStudentIeps);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetStudentItps(int studentId)
        {
            try
            {
                var allStudentItps = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true && x.StudentId == studentId, null, 
                    x=> x.Include(x=> x.Student)
                    .Include(x => x.Therapist)
                    .Include(x => x.AcadmicYear)
                    .Include(x => x.Term)
                    .Include(x => x.ParamedicalService)
                    , 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ItpDto>>(allStudentItps);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetStudentIxps(int studentId)
        {
            try
            {
                var allStudentIxps = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true && x.StudentId == studentId, null, 
                    x=> x.Include(x => x.Student)
                    .Include(x => x.AcadmicYear)
                    .Include(x => x.Term)
                    , 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<IxpDto>>(allStudentIxps);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public ResponseDto GetStudentTherapistsById(int studentTherapistId)
        {
            try
            {
                var studentTherapist = _uow.GetRepository<StudentTherapist>().Single();
                var mapper = _mapper.Map<StudentTherapistDto>(studentTherapist);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddStudentTherapist(StudentTherapistDto studentTherapistDto)
        {
            try
            {
                var mapper = _mapper.Map<StudentTherapist>(studentTherapistDto);
                _uow.GetRepository<StudentTherapist>().Add(mapper);
                _uow.SaveChanges();
                studentTherapistDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Student Therapist Added  Seccessfuly", Data = studentTherapistDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditStudentTherapist(StudentTherapistDto studentTherapistDto)
        {
            try
            {
                var mapper = _mapper.Map<StudentTherapist>(studentTherapistDto);
                _uow.GetRepository<StudentTherapist>().Update(mapper);
                _uow.SaveChanges();
                studentTherapistDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Student Therapist Updated Seccessfuly", Data = studentTherapistDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteStudentTherapist(int studentTherapistId)
        {
            try
            {
                StudentTherapist oStudentTherapist = _uow.GetRepository<StudentTherapist>().Single(x => x.Id == studentTherapistId);
                _uow.GetRepository<StudentTherapist>().Delete(oStudentTherapist);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Student Therapist Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetHistoricalSkillsBystudentId(int studentId)
        {
            try
            {
                if (studentId != null || studentId != 0)
                {
                    var studentHistoricalSkill = _uow.GetRepository<StudentHistoricalSkill>().GetList(x => x.StudentId == studentId);
                var mapper = _mapper.Map<Paginate<StudentHistoricalSkillDto>>(studentHistoricalSkill);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                }
                else
                    return new ResponseDto { Status = 1, Message = " null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddStudentHistoricalSkill(List<StudentHistoricalSkillDto> studentHistoricalSkillDto)
        {
            try
            {
                //var mapper = _mapper.Map<List<StudentHistoricalSkill>>(studentHistoricalSkillDto);
                List<StudentHistoricalSkill> historicalSkill = new List<StudentHistoricalSkill>();
                foreach (var item in studentHistoricalSkillDto)
                    historicalSkill.Add(new StudentHistoricalSkill { HistoricalSkilld = item.HistoricalSkilld, StudentId = item.StudentId });

                _uow.GetRepository<StudentHistoricalSkill>().Add(historicalSkill);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Student Historical Skill Added  Seccessfuly", Data = historicalSkill };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditStudentHistoricalSkill(StudentHistoricalSkillDto studentHistoricalSkillDto)
        {
            try
            {
                var mapper = _mapper.Map<StudentHistoricalSkill>(studentHistoricalSkillDto);
                _uow.GetRepository<StudentHistoricalSkill>().Update(mapper);
                _uow.SaveChanges();
                studentHistoricalSkillDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Student Historical Skill Updated Seccessfuly", Data = studentHistoricalSkillDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteStudentHistoricalSkill(int studentHistoricalSkillId)
        {
            try
            {
                var cmd = $"delete from StudentHistoricalSkills where Id={studentHistoricalSkillId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Student Historical Skill Seccessfuly Deleted" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetAttachmentsByStudentId(int studentId)
        {
            try
            {
                if (studentId != null || studentId != 0)
                {
                    var studentAttachment = _uow.GetRepository<StudentAttachment>().GetList(x => x.StudentId == studentId, null, x => x.Include(x => x.AttachmentType));
                    var mapper = _mapper.Map <PaginateDto<StudentAttachmentDto>>(studentAttachment);
                    if (mapper.Items.Count() > 0)
                    {
                        mapper = GetFullPathAndBinaryAtt(mapper);
                    }
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                }
              else
                return new ResponseDto { Status = 1, Message = " null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddStudentAttachment(IFormFile file, StudentAttachmentDto studentAttachmentDto)
        {
            try
            {
                if (file != null)
                {
                    //change File to binary
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    StudentAttachmentBinary studentAttachmentBinary = new StudentAttachmentBinary();
                    studentAttachmentBinary.FileBinary = ms.ToArray();
                    ms.Close();
                    ms.Dispose();

                    //upload file in local directory

                   // var result = _ifileService.UploadFile(file);
                    var result = _ifileService.SaveBinary(file.FileName, studentAttachmentBinary.FileBinary);

                    studentAttachmentDto.FileName = result.FileName;

                    //saving to DataBase
                    var mapper = _mapper.Map<StudentAttachment>(studentAttachmentDto);
                    mapper.StudentAttachmentBinary = studentAttachmentBinary;
                    _uow.GetRepository<StudentAttachment>().Add(mapper);
                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Student Attachment Added  Seccessfuly", Data = studentAttachmentDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null"};
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditStudentAttachment(StudentAttachmentDto studentAttachmentDto)
        {
            try
            {
                var mapper = _mapper.Map<StudentAttachment>(studentAttachmentDto);
                _uow.GetRepository<StudentAttachment>().Update(mapper);
                _uow.SaveChanges();
                studentAttachmentDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Student Attachment Updated Seccessfuly", Data = studentAttachmentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteStudentAttachment(int studentAttachmentId)
        {
            try
            {
                var cmd = $"delete from StudentAttachmentBinary where Id={studentAttachmentId}" +
                 $"delete from StudentAttachment where Id={studentAttachmentId}" ;
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Student Attachment Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetPhonesByStudentId(int studentId)
        {
            try
            {
                if (studentId != null || studentId != 0)
                {
                    var phone = _uow.GetRepository<Phone>().GetList(x => x.StudentId == studentId);
                var mapper = _mapper.Map < Paginate<PhoneDto>>(phone);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                }
                else
                    return new ResponseDto { Status = 1, Message = " null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddStudentPhone(PhoneDto phoneDto)
        {
            try
            {
                var mapper = _mapper.Map<Phone>(phoneDto);
                _uow.GetRepository<Phone>().Add(mapper);
                _uow.SaveChanges();
                phoneDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Student Phone Added  Seccessfuly", Data = phoneDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditStudentPhone(PhoneDto phoneDto)
        {
            try
            {
                var mapper = _mapper.Map<Phone>(phoneDto);
                _uow.GetRepository<Phone>().Update(mapper);
                _uow.SaveChanges();
                phoneDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Student Phone Updated Seccessfuly", Data = phoneDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteStudentPhone(int phoneId)
        {
            try
            {
                var cmd = $"delete from Phone where Id={phoneId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Student Phone Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto IsSuspended(int studentId, bool isSuspended)
        {
            try
            {
                Student oStudent = _uow.GetRepository<Student>().Single(x => x.Id == studentId);
                oStudent.IsSuspended = isSuspended;
                _uow.GetRepository<Student>().Update(oStudent);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Student Is Suspended State Has Changed" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto IsActive(int studentId, bool isActive)
        {
            try
            {
                Student oStudent = _uow.GetRepository<Student>().Single(x => x.Id == studentId);
                oStudent.IsActive = isActive;
                _uow.GetRepository<Student>().Update(oStudent);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Student Is Active State Has Changed" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public List<VwStudentDto> GetFullPathAndBinary(List<VwStudentDto> allStudents)
        {
            try
            {
                if (allStudents.Count() > 0)
                {
                    foreach (var item in allStudents)
                    {
                        if (File.Exists("wwwRoot/tempFiles/" + item.Image))
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.Image}";
                            item.FullPath = fullpath;
                        }
                        else
                        {
                            if (item != null && item.Image != null)
                            {
                                var student = _uow.GetRepository<Student>().Single(x => x.Id == item.Id && x.IsDeleted != true, null, null);
                                if (student.ImageBinary != null)
                                {
                                    System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.Image, student.ImageBinary);
                                }
                                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                                var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.Image}";
                                item.FullPath = fullpath;
                            }
                        }
                    }
                }
                return allStudents;
            }
            catch (Exception ex)
            {
                return allStudents; ;
            }
        }
        public bool IsStudentCodeExist(int StudentCode, int? StudentId)
        {
            try
            {
                if (StudentCode != null && StudentId != null)
                {
                    var student = _uow.GetRepository<Student>().GetList(x => x.Code == StudentCode && x.Id != StudentId);
                    if (student.Items.Count() > 0)
                        return true;
                }
                if (StudentCode != null && StudentId == null)
                {
                    var student = _uow.GetRepository<Student>().GetList(x => x.Code == StudentCode);
                    if (student.Items.Count() > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ResponseDto GetStudentHistoricalSkills(int studentId)
        {
            try
            {
                var studentIeps = _uow.GetRepository<Iep>().GetList(x => x.StudentId == studentId && x.IsDeleted != true && x.Status == 3).Items.Select(x=> x.Id).ToArray();
                if (studentIeps.Count()>0)
                {
                    var studentObjectives = _uow.GetRepository<Objective>().GetList(x => studentIeps.Contains(x.IepId.Value == null ? 0 : x.IepId.Value)).Items.Select(x => x.Id).ToArray();
                    if (studentObjectives.Count() > 0)
                    {
                        var studentObjectiveSkill = _uow.GetRepository<ObjectiveSkill>().GetList(x => x.ObjectiveId!= null&& studentObjectives.Contains(x.ObjectiveId.Value == null?0: x.ObjectiveId.Value)).Items.Select(x => x.SkillId).Distinct().ToArray();
                        if (studentObjectiveSkill.Count()>0)
                        {
                            var studentSkill = _uow.GetRepository<Skill>().GetList(x => x.Id != null && studentObjectiveSkill.Contains(x.Id), null, x=>x.Include(x => x.Strand).ThenInclude((x => x.Area)));
                            if (studentSkill.Items.Count()>0)
                            {
                                var mapper = _mapper.Map<PaginateDto<SkillDto>>(studentSkill);
                                var mappers = mapper.Items.Distinct();

                                return new ResponseDto { Status = 1, Message = "Student Is Active State Has Changed", Data = mappers };
                            }
                            else
                                return new ResponseDto { Status = 1, Message = "null" };
                        }
                        else
                            return new ResponseDto { Status = 1, Message = "null" };
                    }
                    else
                        return new ResponseDto { Status = 1, Message = "null" };
                }
                else
                return new ResponseDto { Status = 1, Message = "null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        private PaginateDto<StudentAttachmentDto> GetFullPathAndBinaryAtt(PaginateDto<StudentAttachmentDto> allStudentAttachement)
        {
            try
            {
                if (allStudentAttachement.Items.Count() > 0)
                {
                    foreach (var item in allStudentAttachement.Items)
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
                                var att = _uow.GetRepository<StudentAttachmentBinary>().Single(x => x.Id == item.Id, null, null);
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
                return allStudentAttachement;
            }
            catch (Exception ex)
            {
                return allStudentAttachement; ;
            }
        }
        private ICollection<StudentAttachmentDto> GetFullPathAndBinaryICollictionAtt(ICollection<StudentAttachmentDto> allStudentAttachement)
        {
            try
            {
                if (allStudentAttachement.Count() > 0)
                {
                    foreach (var item in allStudentAttachement)
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
                                var att = _uow.GetRepository<StudentAttachmentBinary>().Single(x => x.Id == item.Id, null, null);
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
                return allStudentAttachement;
            }
            catch (Exception ex)
            {
                return allStudentAttachement; ;
            }
        }


    }
}
