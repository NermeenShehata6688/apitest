﻿using AutoMapper;
using Dapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using IesSchool.InfraStructure.Paging;
using Microsoft.EntityFrameworkCore;
using Olsys.Business.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class IepService : IIepService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        public IepService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
        }
        #region Iep
        public ResponseDto GetIepsHelper()
        {
            try
            {

                var ress = GetIepHelperDapper();

                IepHelper iepHelper = new IepHelper();
                //IepHelper iepHelper = new IepHelper()
                //{
                //    //AllDepartments = _uow.GetRepository<Department>().GetList(null, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                //    AllStudents = _uow.GetRepository<VwStudent>().GetList((x => new VwStudent { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code, TeacherId = x.TeacherId, IsDeleted = x.IsDeleted }), null, null, null, 0, 100000, true),
                //    AllAcadmicYears = _uow.GetRepository<AcadmicYear>().GetList(null, null, null, 0, 1000000, true),
                //    AllTerms = _uow.GetRepository<Term>().GetList(null, null, null, 0, 1000000, true),
                //    AllTeachers = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, RoomNumber = x.RoomNumber, IsDeleted = x.IsDeleted }), x => x.IsTeacher == true, null, null, 0, 1000000, true),
                //    AllAssistants = _uow.GetRepository<Assistant>().GetList((x => new Assistant { Id = x.Id, Name = x.Name }), null, null, null, 0, 1000000, true),
                //    AllHeadOfEducations = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, IsDeleted = x.IsDeleted }), x => x.IsHeadofEducation == true, null, null, 0, 1000000, true),
                //    AllTeacherAssistants = _uow.GetRepository<UserAssistant>().GetList(null, null, x => x.Include(x => x.Assistant), 0, 1000000, true),
                //    Setting = _uow.GetRepository<Setting>().Single(),
                //};


                var mapper = _mapper.Map<IepHelperDto>(iepHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public ResponseDto GetIepHelperDapper()
        {
            IepHelper iepHelper = new IepHelper();
            using (IDbConnection dbConnection = ConnectionManager.GetConnection())
            {
                dbConnection.Open();
                var allDepartment = dbConnection.Query<Department>(SqlGeneralBuilder.Select_All_Department()).OrderBy(x => x.DisplayOrder).ToList();
                iepHelper.AllDepartments = new PaginateDto<Department>
                {
                    Items = allDepartment,
                    Count = allDepartment.Count()
                };


                var allStudents = dbConnection.Query<VwStudent>(SqlGeneralBuilder.Select_All_Students()).ToList();
                iepHelper.AllStudents = new PaginateDto<VwStudent>
                {
                    Items = allStudents,
                    Count = allStudents.Count()
                };

                var allAcadmicYears = dbConnection.Query<AcadmicYear>(SqlGeneralBuilder.Select_All_AcadimicYears()).ToList();
                iepHelper.AllAcadmicYears = new PaginateDto<AcadmicYear>
                {
                    Items = allAcadmicYears,
                    Count = allAcadmicYears.Count()
                };

                var allTerms = dbConnection.Query<Term>(SqlGeneralBuilder.Select_AllTerms()).ToList();
                iepHelper.AllTerms = new PaginateDto<Term>
                {
                    Items = allTerms,
                    Count = allTerms.Count()
                };

                var allTeachers = dbConnection.Query<User>(SqlGeneralBuilder.Select_AllUsers()).ToList();
                iepHelper.AllTeachers = new PaginateDto<User>
                {
                    Items = allTeachers,
                    Count = allTeachers.Count()
                };

                var allAssistants = dbConnection.Query<Assistant>(SqlGeneralBuilder.Select_AllAssistant()).ToList();
                iepHelper.AllAssistants = new PaginateDto<Assistant>
                {
                    Items = allAssistants,
                    Count = allAssistants.Count()
                };

                var allHeadOfEducations = dbConnection.Query<User>(SqlGeneralBuilder.Select_AllHeadOfEducation()).ToList();
                iepHelper.AllHeadOfEducations = new PaginateDto<User>
                {
                    Items = allHeadOfEducations,
                    Count = allHeadOfEducations.Count()
                };

                var allTeacherAssistants = dbConnection.Query<UserAssistant>(SqlGeneralBuilder.Select_AllUserAssistant()).ToList();
                iepHelper.AllTeacherAssistants = new PaginateDto<UserAssistant>
                {
                    Items = allTeacherAssistants,
                    Count = allTeacherAssistants.Count()
                };

                var setting = dbConnection.Query<Setting>(SqlGeneralBuilder.Select_Setting()).ToList();
                iepHelper.Setting = setting.FirstOrDefault();
                dbConnection.Close();
                //var mapper = _mapper.Map<IepHelperDto>(iepHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = iepHelper };
            }
        }
        public ResponseDto GetIepsHelper2()
        {
            try
            {
                IepHelper2 iepHelper = new IepHelper2();
                //IepHelper2 iepHelper = new IepHelper2()
                //{
                //    AllPrograms = _uow.GetRepository<Program>().GetList(null, null, null, 0, 1000000, true),
                //    AllAreas = _uow.GetRepository<Area>().GetList(null, null, null, 0, 1000000, true),
                //    AllStrands = _uow.GetRepository<Strand>().GetList(null, null, null, 0, 1000000, true),
                //    AllParamedicalServices = _uow.GetRepository<ParamedicalService>().GetList(null, null, null, 0, 1000000, true),
                //    AllExtraCurriculars = _uow.GetRepository<ExtraCurricular>().GetList(null, null, null, 0, 1000000, true),
                //    AllSkillEvaluations = _uow.GetRepository<SkillEvaluation>().GetList(null, null, null, 0, 1000000, true),
                //    AllTherapist = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, DepartmentId = x.DepartmentId, IsDeleted = x.IsDeleted }), x => x.IsTherapist == true, null, null, 0, 1000000, true),
                //    AllExtraCurricularsTeachers = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, DepartmentId = x.DepartmentId, IsDeleted = x.IsDeleted }), x => x.IsExtraCurricular == true, null, null, 0, 1000000, true),
                //    TherapistParamedicalService = _uow.GetRepository<TherapistParamedicalService>().GetList(null, null, null, 0, 1000000, true),
                //    UserExtraCurricular = _uow.GetRepository<UserExtraCurricular>().GetList(null, null, null, 0, 1000000, true),
                //    AllStudentTherapist = _uow.GetRepository<StudentTherapist>().GetList(null, null, null, 0, 1000000, true),
                //    AllStudentExtraTeacher = _uow.GetRepository<StudentExtraTeacher>().GetList(null, null, null, 0, 1000000, true),
                //    Setting = _uow.GetRepository<Setting>().Single(),

                //};
                var mapper = _mapper.Map<IepHelper2Dto>(iepHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetIepsHelper2Dapper()
        {
            try
            {


                IepHelper2 iepHelper = new IepHelper2();
                using (IDbConnection dbConnection = ConnectionManager.GetConnection())
                {
                    dbConnection.Open();
                    var allPrograms = dbConnection.Query<Program>(SqlGeneralBuilder.Select_All_Programs()).ToList();
                    iepHelper.AllPrograms = new PaginateDto<Program>
                    {
                        Items = allPrograms,
                        Count = allPrograms.Count()
                    };

                    var allAreas = dbConnection.Query<Area>(SqlGeneralBuilder.Select_All_Areas()).ToList();
                    iepHelper.AllAreas = new PaginateDto<Area>
                    {
                        Items = allAreas,
                        Count = allAreas.Count()
                    };

                    var AllStrands = dbConnection.Query<Strand>(SqlGeneralBuilder.Select_All_Strands()).ToList();
                    iepHelper.AllStrands = new PaginateDto<Strand>
                    {
                        Items = AllStrands,
                        Count = AllStrands.Count()
                    };

                    var AllParamedicalServices = dbConnection.Query<ParamedicalService>(SqlGeneralBuilder.Select_All_ParamedicalServices()).ToList();
                    iepHelper.AllParamedicalServices = new PaginateDto<ParamedicalService>
                    {
                        Items = AllParamedicalServices,
                        Count = AllParamedicalServices.Count()
                    };

                    var AllExtraCurriculars = dbConnection.Query<ExtraCurricular>(SqlGeneralBuilder.Select_All_ExtraCurriculars()).ToList();
                    iepHelper.AllExtraCurriculars = new PaginateDto<ExtraCurricular>
                    {
                        Items = AllExtraCurriculars,
                        Count = AllExtraCurriculars.Count()
                    };

                    var AllSkillEvaluations = dbConnection.Query<SkillEvaluation>(SqlGeneralBuilder.Select_All_SkillEvaluations()).ToList();
                    iepHelper.AllSkillEvaluations = new PaginateDto<SkillEvaluation>
                    {
                        Items = AllSkillEvaluations,
                        Count = AllSkillEvaluations.Count()
                    };

                    var AllTherapist = dbConnection.Query<User>(SqlGeneralBuilder.Select_AllTherapists()).ToList();
                    iepHelper.AllTherapist = new PaginateDto<User>
                    {
                        Items = AllTherapist,
                        Count = AllTherapist.Count()
                    };

                    var AllExtraCurricularsTeachers = dbConnection.Query<User>(SqlGeneralBuilder.Select_AllExtraCurricularsTeacher()).ToList();
                    iepHelper.AllExtraCurricularsTeachers = new PaginateDto<User>
                    {
                        Items = AllExtraCurricularsTeachers,
                        Count = AllExtraCurricularsTeachers.Count()
                    };
                    var TherapistParamedicalService = dbConnection.Query<TherapistParamedicalService>(SqlGeneralBuilder.Select_All_TherapistParamedicalServices()).ToList();
                    iepHelper.TherapistParamedicalService = new PaginateDto<TherapistParamedicalService>
                    {
                        Items = TherapistParamedicalService,
                        Count = TherapistParamedicalService.Count()
                    };

                    var UserExtraCurricular = dbConnection.Query<UserExtraCurricular>(SqlGeneralBuilder.Select_All_UserExtraCurriculars()).ToList();
                    iepHelper.UserExtraCurricular = new PaginateDto<UserExtraCurricular>
                    {
                        Items = UserExtraCurricular,
                        Count = UserExtraCurricular.Count()
                    };

                    var AllStudentTherapist = dbConnection.Query<StudentTherapist>(SqlGeneralBuilder.Select_All_StudentTherapists()).ToList();
                    iepHelper.AllStudentTherapist = new PaginateDto<StudentTherapist>
                    {
                        Items = AllStudentTherapist,
                        Count = AllStudentTherapist.Count()
                    };

                    var AllStudentExtraTeacher = dbConnection.Query<StudentExtraTeacher>(SqlGeneralBuilder.Select_All_StudentExtraTeachers()).ToList();
                    iepHelper.AllStudentExtraTeacher = new PaginateDto<StudentExtraTeacher>
                    {
                        Items = AllStudentExtraTeacher,
                        Count = AllStudentExtraTeacher.Count()
                    };
                    var setting = dbConnection.Query<Setting>(SqlGeneralBuilder.Select_Setting()).ToList();
                    iepHelper.Setting = setting.FirstOrDefault();
                    dbConnection.Close();


                }

                return new ResponseDto { Status = 1, Message = "Success", Data = iepHelper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetIeps(IepSearchDto iepSearchDto)
        {
            try
            {
                var allIeps = _uow.GetRepository<VwIep>().Query("select * from Vw_Ieps where IsDeleted != 1");

                if (iepSearchDto.Student_Id != null)
                {
                    allIeps = allIeps.Where(x => x.Student_Id == iepSearchDto.Student_Id);
                }
                if (iepSearchDto.AcadmicYear_Id != null)
                {
                    allIeps = allIeps.Where(x => x.AcadmicYear_Id == iepSearchDto.AcadmicYear_Id);
                }
                if (iepSearchDto.Term_Id != null)
                {
                    allIeps = allIeps.Where(x => x.Term_Id == iepSearchDto.Term_Id);
                }
                if (iepSearchDto.Teacher_Id != null)
                {
                    allIeps = allIeps.Where(x => x.Teacher_Id == iepSearchDto.Teacher_Id);
                }
                if (iepSearchDto.HeadOfDepartment != null)
                {
                    allIeps = allIeps.Where(x => x.HeadOfDepartment == iepSearchDto.HeadOfDepartment);
                }
                if (iepSearchDto.HeadOfEducation != null)
                {
                    allIeps = allIeps.Where(x => x.HeadOfEducation == iepSearchDto.HeadOfEducation);
                }
                if (iepSearchDto.Status != null)
                {
                    allIeps = allIeps.Where(x => x.Status == iepSearchDto.Status);
                }
                if (iepSearchDto.IsPublished != null)
                {
                    allIeps = allIeps.Where(x => x.IsPublished == iepSearchDto.IsPublished);
                }
                if (iepSearchDto.Department_Id != null)
                {
                    allIeps = allIeps.Where(x => x.Department_Id == iepSearchDto.Department_Id);
                }
                if (iepSearchDto.StudentCode != null)
                {
                    allIeps = allIeps.Where(x => x.StudentCode.ToString().Contains(iepSearchDto.StudentCode));
                }


                var lstIepDto = _mapper.Map<List<VwIepDto>>(allIeps);
                if (iepSearchDto.Index == null || iepSearchDto.Index == 0)
                {
                    iepSearchDto.Index = 0;
                }
                else
                {
                    iepSearchDto.Index += 1;
                }
                var mapper = new PaginateDto<VwIepDto> { Count = allIeps.Count(), Items = lstIepDto != null ? lstIepDto.Skip(iepSearchDto.Index == null || iepSearchDto.PageSize == null ? 0 : ((iepSearchDto.Index.Value - 1) * iepSearchDto.PageSize.Value)).Take(iepSearchDto.PageSize ??= 20).ToList() : lstIepDto.ToList() };

                //var mapper = new PaginateDto<VwIepDto> { Count = allIeps.Count(), Items = lstIepDto != null ? lstIepDto.Skip(iepSearchDto.Index ??= 0).Take(iepSearchDto.PageSize ??= 20).ToList() : lstIepDto.ToList() };
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public async Task<ResponseDto> GetIepByIdDapper(int iepId)
        {
            try
            {
                if (iepId != 0)
                {

                    var iep = await _iesContext.Ieps.Where(x => x.Id == iepId).Include(x => x.IepAssistants).ThenInclude(x => x.Assistant)
                               .Include(s => s.IepParamedicalServices.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ParamedicalService)
                             .Include(s => s.IepParamedicalServices.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Therapist)
                             .Include(s => s.IepExtraCurriculars.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ExtraCurricular)
                             .Include(s => s.IepExtraCurriculars.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ExTeacher)
                             .Include(s => s.Student).ThenInclude(s => s.Department)
                             .Include(s => s.AcadmicYear)
                             .Include(s => s.Term)
                             .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills).ThenInclude(s => s.Skill)
                             .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveEvaluationProcesses).ThenInclude(s => s.SkillEvaluation)
                             .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Strand)
                             .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Area)
                             .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Program).FirstOrDefaultAsync();
                    var mapper = _mapper.Map<GetIepDto>(iep);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };


                }
                else
                {
                    return new ResponseDto { Status = 0, Message = " null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetIepById(int iepId)
        {
            try
            {
                if (iepId != 0)
                {
                    var iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true, null, x => x
                    .Include(s => s.IepAssistants).ThenInclude(s => s.Assistant)
               .Include(s => s.IepParamedicalServices.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ParamedicalService)
               .Include(s => s.IepParamedicalServices.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Therapist)
               .Include(s => s.IepExtraCurriculars.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ExtraCurricular)
               .Include(s => s.IepExtraCurriculars.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ExTeacher)
               .Include(s => s.Student).ThenInclude(s => s.Department)
               //.Include(s => s.Teacher)
               // .Include(s => s.HeadOfDepartmentNavigation)
               // .Include(s => s.HeadOfEducationNavigation)
               .Include(s => s.AcadmicYear)
               .Include(s => s.Term)
               .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills).ThenInclude(s => s.Skill)
               .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveEvaluationProcesses).ThenInclude(s => s.SkillEvaluation)
               .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Strand)
               .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Area)
               .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Program)
               );
                    var mapper = _mapper.Map<GetIepDto>(iep);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                }
                else
                {
                    return new ResponseDto { Status = 0, Message = " null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> AddIep(IepDto iepDto)
        {
            try
            {
                if (iepDto != null)
                {
                    iepDto.IsDeleted = false;
                    iepDto.CreatedOn = DateTime.Now;
                    var mapper = _mapper.Map<Iep>(iepDto);
                    await _uow.GetRepositoryAsync<Iep>().AddAsync(mapper);
                    _uow.SaveChanges();
                    iepDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Iep Added  Seccessfuly", Data = iepDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditIep(IepDto iepDto)
        {
            try
            {
                if (iepDto != null)
                {
                    var oldIep = _uow.GetRepository<Iep>().Single(x => x.Id == iepDto.Id);
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from IepAssistant where IEPId={iepDto.Id}";
                    _iesContext.Database.ExecuteSqlRawAsync(cmd);
                    var mapper = _mapper.Map<Iep>(iepDto);
                    mapper.IsDeleted = false;
                    _uow.GetRepository<Iep>().Update(mapper);
                    _uow.SaveChanges();
                    transaction.Commit();
                    if (oldIep.StudentId != mapper.StudentId || oldIep.AcadmicYearId != mapper.AcadmicYearId || oldIep.TermId != mapper.TermId)
                    {
                        UpdateIepInfo(mapper);
                    }
                    //if (oldIep.StudentId != mapper.StudentId)
                    //{
                    //    UpdateStudentInfo(mapper);
                    //}
                    //if (oldIep.AcadmicYearId != mapper.AcadmicYearId)
                    //{
                    //    UpdateAcademicYearInfo(mapper);
                    //}
                    //if (oldIep.TermId != mapper.TermId)
                    //{
                    //    UpdateTermInfo(mapper);
                    //}


                    iepDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Iep Updated Seccessfuly", Data = iepDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public void UpdateIepInfoLight(Iep mapper)
        {
            try
            {
                if (mapper != null && mapper.Id > 0)
                {
                    var iepParmedicalIds = _uow.GetRepository<IepParamedicalService>().GetList(x => x.Iepid == mapper.Id && x.IsItpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
                    if (iepParmedicalIds.Count() > 0)
                    {
                        var itpList = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true && iepParmedicalIds.Contains(x.Id), null, null, 0, 1000, true).Items;
                        if (itpList.Count() > 0)
                        {
                            foreach (var item in itpList)
                            {
                                item.StudentId = mapper.StudentId;
                                item.AcadmicYearId = mapper.AcadmicYearId;
                                item.TermId = mapper.TermId;
                            }
                            //_uow.GetRepository<Itp>().Update(itpList);
                            //_uow.SaveChanges();

                            _iesContext.Itps.UpdateRange(itpList);
                            _iesContext.SaveChanges();
                        }
                    }
                    var iepExtraIds = _uow.GetRepository<IepExtraCurricular>().GetList(x => x.Iepid == mapper.Id && x.IsIxpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
                    if (iepExtraIds.Count() > 0)
                    {
                        var ixpList = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true && iepExtraIds.Contains(x.Id), null, null, 0, 1000, true).Items;
                        if (ixpList.Count() > 0)
                        {
                            foreach (var item in ixpList)
                            {
                                item.StudentId = mapper.StudentId;
                                item.AcadmicYearId = mapper.AcadmicYearId;
                                item.TermId = mapper.TermId;
                            }
                            //_uow.GetRepository<Ixp>().Update(ixpList);
                            //_uow.SaveChanges();

                            _iesContext.Ixps.UpdateRange(ixpList);
                            _iesContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public async Task<ResponseDto> EditIepLight(IepDto iepDto)
        {
            try
            {
                if (iepDto != null)
                {
                    var cmd = $"delete from IepAssistant where IEPId={iepDto.Id}";
                    await _iesContext.Database.ExecuteSqlRawAsync(cmd); 

                    var mapper = _mapper.Map<Iep>(iepDto);
                    mapper.IsDeleted = false;

                    _iesContext.Ieps.Update(mapper);
                    _iesContext.SaveChanges();
                    

                    var oldIep = await _uow.GetRepositoryAsync<Iep>().SingleAsync(x => x.Id == iepDto.Id);

                    if (oldIep.StudentId != mapper.StudentId || oldIep.AcadmicYearId != mapper.AcadmicYearId || oldIep.TermId != mapper.TermId)
                    {
                        UpdateIepInfo(mapper);
                    }
                    //if (oldIep.StudentId != mapper.StudentId)
                    //{
                    //    UpdateStudentInfo(mapper);
                    //}
                    //if (oldIep.AcadmicYearId != mapper.AcadmicYearId)
                    //{
                    //    UpdateAcademicYearInfo(mapper);
                    //}
                    //if (oldIep.TermId != mapper.TermId)
                    //{
                    //    UpdateTermInfo(mapper);
                    //}
                    iepDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Iep Updated Seccessfuly", Data = iepDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public void UpdateIepInfo(Iep mapper)
        {
            try
            {
                if (mapper != null && mapper.Id > 0)
                {
                    var iepParmedicalIds = _uow.GetRepository<IepParamedicalService>().GetList(x => x.Iepid == mapper.Id && x.IsItpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
                    if (iepParmedicalIds.Count() > 0)
                    {
                        var itpList = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true && iepParmedicalIds.Contains(x.Id), null, null, 0, 1000, true).Items;
                        if (itpList.Count() > 0)
                        {
                            foreach (var item in itpList)
                            {
                                item.StudentId = mapper.StudentId;
                                item.AcadmicYearId = mapper.AcadmicYearId;
                                item.TermId = mapper.TermId;
                            }
                            _uow.GetRepository<Itp>().Update(itpList);
                            _uow.SaveChanges();
                        }
                    }
                    var iepExtraIds = _uow.GetRepository<IepExtraCurricular>().GetList(x => x.Iepid == mapper.Id && x.IsIxpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
                    if (iepExtraIds.Count() > 0)
                    {
                        var ixpList = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true && iepExtraIds.Contains(x.Id), null, null, 0, 1000, true).Items;
                        if (ixpList.Count() > 0)
                        {
                            foreach (var item in ixpList)
                            {
                                item.StudentId = mapper.StudentId;
                                item.AcadmicYearId = mapper.AcadmicYearId;
                                item.TermId = mapper.TermId;
                            }
                            _uow.GetRepository<Ixp>().Update(ixpList);
                            _uow.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async Task<ResponseDto> DeleteIep(int iepId)
        {
            try
            {
                if (iepId > 0)
                {
                    var iep = await _uow.GetRepositoryAsync<Iep>().SingleAsync(x => x.Id == iepId, null, x => x
                     .Include(x => x.IepExtraCurriculars.Where(x => x.IsIxpCreated == true && x.IsDeleted != true))
                     .Include(x => x.IepParamedicalServices.Where(x => x.IsItpCreated == true && x.IsDeleted != true)));

                    if (iep != null)
                    {
                        if (iep.IepExtraCurriculars.Count() > 0)
                        {
                            return new ResponseDto { Status = 0, Message = "Iep can not be deleted,when it has IXPs Related" };
                        }
                        if (iep.IepParamedicalServices.Count() > 0)
                        {
                            return new ResponseDto { Status = 0, Message = "Iep can not be deleted,when it has ITPs Related" };
                        }
                    }
                    iep.IsDeleted = true;
                    iep.DeletedOn = DateTime.Now;
                    _uow.GetRepositoryAsync<Iep>().UpdateAsync(iep);
                    _uow.SaveChanges();

                    // using var transaction = _iesContext.Database.BeginTransaction();
                    // var cmd = $"delete from IEP_ParamedicalService where IEPId={iepId}" +
                    //           $"delete from IEP_ExtraCurricular where IEPId={iepId}";
                    // await  _iesContext.Database.ExecuteSqlRawAsync(cmd);
                    //await transaction.CommitAsync();

                    return new ResponseDto { Status = 1, Message = "Iep Deleted Seccessfuly" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto IepStatus(StatusDto statusDto)
        {
            try
            {
                if (statusDto.Id != 0)
                {
                    Iep iep = _uow.GetRepository<Iep>().Single(x => x.Id == statusDto.Id);
                    iep.Status = statusDto.StatusNo;
                    _uow.GetRepository<Iep>().Update(iep);
                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Iep Status Has Changed" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto IepIsPublished(IsPuplishedDto isPuplishedDto)
        {
            try
            {
                if (isPuplishedDto.Id != 0)
                {
                    Iep iep = _uow.GetRepository<Iep>().Single(x => x.Id == isPuplishedDto.Id);
                    iep.IsPublished = isPuplishedDto.IsPuplished;
                    _uow.GetRepository<Iep>().Update(iep);
                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Iep Is Published Status Has Changed" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public decimal IepObjectiveMasterdPercentage(int iepId)
        {
            try
            {
                decimal iepMasterdPercentage = 0M;
                if (iepId != 0)
                {
                    var iepObjectives = _uow.GetRepository<Objective>().GetList(x => x.IsDeleted != true && x.IepId == iepId);
                    if (iepObjectives.Items.Count() > 0)
                    {
                        var iepMasterdObjectives = iepObjectives.Items.Where(x => x.IsMasterd == true).ToList();
                        if (iepMasterdObjectives.Count() > 0)
                        {
                            iepMasterdPercentage = ((decimal)(iepMasterdObjectives.Count()) / ((decimal)iepObjectives.Items.Count())) * 100;
                            return Math.Round(iepMasterdPercentage);
                        }
                    }
                }
                return iepMasterdPercentage;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public ResponseDto DuplicateIEP(int iepId)
        {
            try
            {
                if (iepId != 0)
                {
                    var oldIep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true, null, x => x
               .Include(s => s.IepAssistants)
               .Include(s => s.IepParamedicalServices)
               .Include(s => s.IepExtraCurriculars)
               .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Objectives.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ObjectiveSkills)
               .Include(s => s.Goals.Where(x => x.IsDeleted != true)).ThenInclude(s => s.Objectives.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ObjectiveEvaluationProcesses));

                    if (oldIep != null)
                    {
                        oldIep.Id = 0;
                        oldIep.IsPublished = false;
                        // oldIep.TermId = null;
                        if (oldIep.IepAssistants.Count() > 0)
                        {
                            oldIep.IepAssistants.ToList().ForEach(x => x.Id = 0);
                        }
                        if (oldIep.IepParamedicalServices.Count() > 0)
                        {
                            oldIep.IepParamedicalServices.ToList().ForEach(x => { x.Id = 0; x.IsItpCreated = false; });
                        }
                        if (oldIep.IepExtraCurriculars.Count() > 0)
                        {
                            oldIep.IepExtraCurriculars.ToList().ForEach(x => { x.Id = 0; x.IsIxpCreated = false; });
                        }
                        if (oldIep.Goals.Count() > 0)
                        {
                            foreach (var goal in oldIep.Goals)
                            {
                                goal.Id = 0;
                                if (goal.Objectives.Count() > 0)
                                {
                                    foreach (var obj in goal.Objectives)
                                    {
                                        obj.Id = 0;
                                        obj.IsMasterd = false;

                                        if (obj.ObjectiveSkills.Count() > 0)
                                        {
                                            obj.ObjectiveSkills.ToList().ForEach(x => x.Id = 0);
                                        }
                                        if (obj.ObjectiveEvaluationProcesses.Count() > 0)
                                        {
                                            obj.ObjectiveEvaluationProcesses.ToList().ForEach(x => x.Id = 0);
                                        }
                                    }
                                }
                            }
                        }
                        oldIep.Status = 0;
                        _uow.GetRepository<Iep>().Add(oldIep);
                        _uow.SaveChanges();

                        var mapper = _mapper.Map<GetIepDto>(oldIep);

                        if (mapper.Id > 0)
                        {
                            var objectivesToUpdate = mapper.Goals.SelectMany(x => x.Objectives).ToList().Select(x => x.Id).ToArray();
                            if (objectivesToUpdate.Count() > 0)
                            {
                                string numbersToUpdate = string.Join(",", objectivesToUpdate);
                                var cmd = $"update Objective set Objective.IepId = {mapper.Id} Where Objective.Id IN ({numbersToUpdate})";
                                _iesContext.Database.ExecuteSqlRaw(cmd);
                            }
                        }

                        return new ResponseDto { Status = 1, Message = " IEP has been Duplicated", Data = mapper };
                    }
                    else
                    {
                        return new ResponseDto { Status = 0, Message = " null" };
                    }
                }
                else
                {
                    return new ResponseDto { Status = 0, Message = " null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        #endregion
        #region Goals
        public ResponseDto GetGoals()
        {
            try
            {
                var allGoals = _uow.GetRepository<Goal>().GetList(x => x.IsDeleted != true, null, x => x.Include(s => s.Strand).Include(s => s.Area), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<GetGoalDto>>(allGoals);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetGoalById(int goalId)
        {
            try
            {
                if (goalId != 0)
                {
                    var goal = await _uow.GetRepositoryAsync<Goal>().SingleAsync(x => x.Id == goalId && x.IsDeleted != true, null, x => x.Include(s => s.Objectives.Where(s => s.IsDeleted != true)).ThenInclude(s => s.ObjectiveEvaluationProcesses).ThenInclude(s => s.SkillEvaluation).Include(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills).ThenInclude(s => s.Skill));
                    var mapper = _mapper.Map<GetGoalDto>(goal);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = " null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetGoalByIepId(int iepId)
        {
            try
            {
                if (iepId != 0)
                {
                    var goals = await _uow.GetRepositoryAsync<Goal>().GetListAsync(x => x.Iepid == iepId && x.IsDeleted != true, null,
                x => x.Include(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills).ThenInclude(s => s.Skill)
               .Include(s => s.Objectives).ThenInclude(s => s.ObjectiveEvaluationProcesses).ThenInclude(s => s.SkillEvaluation)
               .Include(s => s.Strand)
               .Include(s => s.Area)
               .Include(x => x.Program), 0, 100000, true
               );
                    var mapper = _mapper.Map<PaginateDto<GoalDto>>(goals);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = " null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddGoal(GoalDto goalDto)
        {
            try
            {
                if (goalDto != null)
                {
                    goalDto.IsDeleted = false;
                    goalDto.CreatedOn = DateTime.Now;
                    if (goalDto.Objectives != null)
                    {
                        foreach (var objective in goalDto.Objectives)
                        {
                            objective.IsDeleted = false;
                            objective.CreatedOn = DateTime.Now;

                            //if (objective.Activities != null && objective.Activities.Count() > 2)
                            //{
                            //    var obj = _mapper.Map<Objective>(objective);
                            //    objective.IsMasterd = ObjectiveIsMasterd(obj);
                            //    if (objective.IsMasterd==true)
                            //    {
                            //        objective.Date = DateTime.Now;
                            //    }
                            //}
                        }
                    }
                    var mapper = _mapper.Map<Goal>(goalDto);
                    _uow.GetRepository<Goal>().Add(mapper);
                    _uow.SaveChanges();
                    goalDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Goal Added  Seccessfuly", Data = goalDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public async Task<ResponseDto> EditGoal(GoalDto goalDto)
        {
            try
            {
                if (goalDto != null)
                {
                    var mapper = _mapper.Map<Goal>(goalDto);
                    _uow.GetRepository<Goal>().Update(mapper);
                    mapper.IsDeleted = false;
                    using var transaction = _iesContext.Database.BeginTransaction();
                    if (mapper.Id != 0 && mapper.Objectives != null || _iesContext.Objectives.Where(x => x.GoalId == mapper.Id) != null)///count>0
                    {
                        if (mapper.Objectives != null)
                        {
                            //// delete old Objectives Ids which are not in edited goal
                            var newObjInt = mapper.Objectives.Select(x => x.Id);
                            _iesContext.Objectives.RemoveRange(_iesContext.Objectives.Where(x => !newObjInt.Contains(x.Id) && x.GoalId == mapper.Id));
                            await _iesContext.SaveChangesAsync();
                            var oldObjAfterDel = _iesContext.Objectives.Where(x => x.GoalId == mapper.Id).ToList();
                            if (oldObjAfterDel.Count() > 0)
                            {
                                foreach (var newObj in mapper.Objectives.ToList())
                                {
                                    if ((newObj.ObjectiveSkills == null || newObj.ObjectiveSkills.Count == 0) && newObj.Id != 0)  /// deit obj  and delete all skills
                                    {
                                        var listofobskills = _iesContext.ObjectiveSkills.Where(x => x.ObjectiveId == newObj.Id);
                                        _iesContext.ObjectiveSkills.RemoveRange(listofobskills);
                                        await _iesContext.SaveChangesAsync();
                                    }
                                    if ((newObj.ObjectiveSkills != null || newObj.ObjectiveSkills.Count != 0) && newObj.Id != 0)  /// deit obj  and edit all skills
                                    {
                                        var newObjSkillsInt = newObj.ObjectiveSkills.Select(o => o.Id).ToList();
                                        if (newObjSkillsInt.Count() > 0)
                                        {
                                            var listofobskills = _iesContext.ObjectiveSkills.Where(x => !newObjSkillsInt.Contains(x.Id) && x.ObjectiveId == newObj.Id).ToList();
                                            _iesContext.ObjectiveSkills.RemoveRange(listofobskills);
                                            await _iesContext.SaveChangesAsync();
                                        }
                                    }
                                    if ((newObj.ObjectiveEvaluationProcesses == null || newObj.ObjectiveEvaluationProcesses.Count == 0) && newObj.Id != 0)  /// deit obj  and delete all Evaluation
                                    {
                                        var lisObEvaluationProcesses = _iesContext.ObjectiveEvaluationProcesses.Where(x => x.ObjectiveId == newObj.Id).ToList();
                                        _iesContext.ObjectiveEvaluationProcesses.RemoveRange(lisObEvaluationProcesses);
                                        await _iesContext.SaveChangesAsync();
                                    }
                                    if ((newObj.ObjectiveEvaluationProcesses != null || newObj.ObjectiveEvaluationProcesses.Count() != 0) && newObj.Id != 0)  /// deit obj  and edit all Evaluation
                                    {
                                        var newObjEvalProcessesInt = newObj.ObjectiveEvaluationProcesses.Select(o => o.Id).ToList();
                                        if (newObjEvalProcessesInt.Count() > 0)
                                        {
                                            var listofEvaluations = _iesContext.ObjectiveEvaluationProcesses.Where(x => !newObjEvalProcessesInt.Contains(x.Id) && x.ObjectiveId == newObj.Id).ToList();
                                            _iesContext.ObjectiveEvaluationProcesses.RemoveRange(listofEvaluations);
                                            await _iesContext.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            _iesContext.Objectives.RemoveRange(_iesContext.Objectives.Where(x => x.GoalId == mapper.Id));
                            await _iesContext.SaveChangesAsync();
                        }
                    }
                    //_uow.GetRepositoryAsync<Goal>().UpdateAsync(mapper);
                    _uow.SaveChanges();
                    transaction.Commit();

                    goalDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Goal Updated Seccessfuly", Data = goalDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteGoal(int goalId)
        {
            try
            {
                if (goalId != 0)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();

                    var goal = _uow.GetRepository<Goal>().Single(x => x.Id == goalId);
                    if (goal != null)
                    {
                        var iepProgressReport = _uow.GetRepository<IepProgressReport>().GetList(x => x.IepId == goal.Iepid);
                        if (iepProgressReport != null)
                        {
                            if (iepProgressReport.Items.Count() > 0)
                            {
                                var iepProgressReportIds = iepProgressReport.Items.Select(x => x.Id).ToArray();
                                var iepProgressReportIdString = String.Join(',', iepProgressReportIds);

                                var cmd1 = $"delete from  ProgressReportStrand  where ProgressReportId in ({iepProgressReportIdString}) and StrandId = {goal.StrandId}";
                                _iesContext.Database.ExecuteSqlRaw(cmd1);
                            }
                        }
                    }

                    var cmd = $"delete from Goals where Id={goalId}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);

                    transaction.Commit();

                    return new ResponseDto { Status = 1, Message = "Goal Deleted Seccessfuly" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        #endregion
        #region Objectives
        public ResponseDto GetObjectives()
        {
            try
            {
                var allObjectives = _uow.GetRepository<Objective>().GetList(x => x.IsDeleted != true, null, x => x.Include(s => s.ObjectiveSkills).ThenInclude(s => s.Skill).Include(s => s.ObjectiveEvaluationProcesses).ThenInclude(x => x.SkillEvaluation), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<GetObjectiveDto>>(allObjectives);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetObjectiveById(int objectiveId)
        {
            try
            {
                var objective = await _uow.GetRepositoryAsync<Objective>().SingleAsync(x => x.Id == objectiveId && x.IsDeleted != true, null,
                    x => x.Include(s => s.ObjectiveEvaluationProcesses).Include(s => s.ObjectiveSkills).Include(s => s.Activities)
                    .Include(s => s.Goal).ThenInclude(s => s.Area)
                    .Include(s => s.Goal).ThenInclude(s => s.Strand));
                var mapper = _mapper.Map<ObjectiveDto>(objective);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetObjectiveByIEPId(int iepId)
        {
            try
            {
                var objective = await _uow.GetRepositoryAsync<Objective>().GetListAsync(x => x.IepId == iepId && x.IsDeleted != true, null,
                    x => x.Include(s => s.ObjectiveEvaluationProcesses)
                    .Include(s => s.ObjectiveSkills)
                    //.Include(s => s.Activities)
                    .Include(s => s.Goal).ThenInclude(s => s.Strand)
                     .Include(s => s.Goal).ThenInclude(s => s.Area), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<IepObjectiveDto>>(objective);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddObjective(ObjectiveDto objectiveDto)
        {
            try
            {
                if (objectiveDto != null)
                {
                    objectiveDto.IsDeleted = false;
                    objectiveDto.CreatedOn = DateTime.Now;
                    var mapper = _mapper.Map<Objective>(objectiveDto);
                    //if (objectiveDto.Activities != null && objectiveDto.Activities.Count() > 2)
                    //{
                    //    mapper.IsMasterd = ObjectiveIsMasterd(mapper);
                    //    if (mapper.IsMasterd==true)
                    //    {
                    //        mapper.Date = DateTime.Now;
                    //    }
                    //}
                    _uow.GetRepository<Objective>().Add(mapper);
                    _uow.SaveChanges();
                    objectiveDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Objective Added  Seccessfuly", Data = objectiveDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public async Task<ResponseDto> EditObjectiveActivities(ObjectiveActivitiesDto objectiveActivitiesDto)
        {
            try
            {
                if (objectiveActivitiesDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from Activities where ObjectiveId ={objectiveActivitiesDto.Id}";
                    await _iesContext.Database.ExecuteSqlRawAsync(cmd);

                    var obj = _mapper.Map<Objective>(objectiveActivitiesDto);
                    // objectiveActivitiesDto.IsMasterd = ObjectiveIsMasterd(obj);

                    var mapper = _mapper.Map<Objective>(objectiveActivitiesDto);
                    mapper.IsDeleted = false;

                    _iesContext.Objectives.UpdateRange(mapper);
                    _iesContext.SaveChanges();

                    //_uow.GetRepository<Objective>().Update(mapper);
                    //_uow.SaveChanges();
                    objectiveActivitiesDto.Id = mapper.Id;
                    transaction.Commit();

                    //check if object isMasterd
                    var newObjective = _uow.GetRepository<Objective>().Single(x => x.Id == mapper.Id && x.IsDeleted != true, null, x => x.Include(s => s.Activities));
                    if (newObjective != null && newObjective.Activities.Count() > 2)
                    {
                        newObjective.IsMasterd = ObjectiveIsMasterd(newObjective);
                        if (newObjective.IsMasterd == true)
                        {
                            newObjective.Date = DateTime.Now;
                        }
                        _uow.GetRepository<Objective>().Update(mapper);
                        _uow.SaveChanges();
                    }
                    return new ResponseDto { Status = 1, Message = "Objective Updated Seccessfuly", Data = objectiveActivitiesDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteObjective(int objectiveId)
        {
            try
            {
                if (objectiveId != 0)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from Objective where Id={objectiveId}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);
                    transaction.Commit();
                    return new ResponseDto { Status = 1, Message = "Objective Deleted Seccessfuly" };
                }

                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto ObjIsMasterd(ObjStatus statusDto)
        {
            try
            {
                if (statusDto.objId != 0)
                {
                    Objective Objective = _uow.GetRepository<Objective>().Single(x => x.Id == statusDto.objId);
                    Objective.IsMasterd = statusDto.IsMasterd;
                    _uow.GetRepository<Objective>().Update(Objective);
                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Objective Status Has Changed" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public bool ObjectiveIsMasterd(Objective objective)
        {
            try
            {
                //// add new Objective
                if (objective.Id == 0 || objective.Activities != null || objective.Activities.Any(x => x.Evaluation == 3))
                {
                    int eval = 0;
                    for (int i = 0; i < objective.Activities.Count(); i++)
                    {
                        if (objective.Activities.ToList()[i].Evaluation == 3)
                        {
                            eval++;
                            if (eval == 3)
                                return true;
                        }
                        else
                            eval = 0;
                    }
                    if (eval >= 3)
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ResponseDto GetActivities()
        {
            try
            {
                var allActivities = _uow.GetRepository<Activity>().GetList(null, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ActivityDto>>(allActivities);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetActivityByObjectiveId(int objectiveId)
        {
            try
            {
                var objActivities = await _uow.GetRepositoryAsync<Activity>().GetListAsync(x => x.ObjectiveId == objectiveId, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ActivityDto>>(objActivities);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddActivity(ActivityDto activityDto)
        {
            try
            {
                if (activityDto != null)
                {
                    var mapper = _mapper.Map<Activity>(activityDto);
                    _uow.GetRepository<Activity>().Add(mapper);
                    _uow.SaveChanges();
                    activityDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Activity Added  Seccessfuly", Data = activityDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditActivity(ActivityDto activityDto)
        {
            try
            {
                if (activityDto != null)
                {
                    var mapper = _mapper.Map<Activity>(activityDto);
                    _uow.GetRepository<Activity>().Update(mapper);
                    _uow.SaveChanges();
                    activityDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Activity Updated Seccessfuly", Data = activityDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteActivity(int activityId)
        {
            try
            {
                if (activityId > 0)
                {
                    var cmd = $"delete from Activities where Id={activityId}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);
                    return new ResponseDto { Status = 1, Message = "Activity Deleted Seccessfuly" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public int CalculateMasterdObjPercentage(Goal goal)
        {
            try
            {
                int masterdObjPercentage = 0;
                if (goal != null)
                {
                    if (goal.Objectives.Count > 0)
                    {
                        foreach (var obj in goal.Objectives)
                        {
                            if (obj.IsMasterd == true)
                            {
                                masterdObjPercentage = masterdObjPercentage + (obj.ObjectiveNumber == null ? 0 : obj.ObjectiveNumber.Value);
                            }
                        }
                    }
                }
                return masterdObjPercentage;
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion
        #region IepParamedicalService
        public ResponseDto GetIepParamedicalServices()
        {
            try
            {
                var allIepParamedicalServices = _uow.GetRepository<IepParamedicalService>().GetList(null, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<IepParamedicalServiceDto>>(allIepParamedicalServices);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetIepParamedicalServiceByIepId(int iepId)
        {
            try
            {
                var iepParamedicalService = await _uow.GetRepositoryAsync<IepParamedicalService>().GetListAsync(x => x.Iepid == iepId && x.IsDeleted != true, null, x => x.Include(x => x.ParamedicalService).Include(x => x.Therapist));
                var mapper = _mapper.Map<PaginateDto<IepParamedicalServiceDto>>(iepParamedicalService);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto)
        {
            try
            {
                iepParamedicalServiceDto.IsDeleted = false;
                iepParamedicalServiceDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<IepParamedicalService>(iepParamedicalServiceDto);
                _uow.GetRepository<IepParamedicalService>().Add(mapper);
                _uow.SaveChanges();
                iepParamedicalServiceDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Iep Paramedical Service Added  Seccessfuly", Data = iepParamedicalServiceDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public async Task<ResponseDto> EditIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var mapper = _mapper.Map<IepParamedicalService>(iepParamedicalServiceDto);


                _iesContext.IepParamedicalServices.Update(mapper);
                await _iesContext.SaveChangesAsync();

                //_uow.GetRepository<IepParamedicalService>().Update(mapper);
                //_uow.SaveChanges();
                var cmd = $"update ITP set ParamedicalServiceId ={iepParamedicalServiceDto.ParamedicalServiceId} , TherapistId ={iepParamedicalServiceDto.TherapistId},TherapistDepartmentId={iepParamedicalServiceDto.TherapistDepartmentId} Where Id ={iepParamedicalServiceDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                transaction.Commit();
                iepParamedicalServiceDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Iep Paramedical Service Updated Seccessfuly", Data = iepParamedicalServiceDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteIepParamedicalService(int iepParamedicalServiceId)
        {
            try
            {
                if (iepParamedicalServiceId > 0)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    IepParamedicalService iepParamedicalService = _uow.GetRepository<IepParamedicalService>().Single(x => x.Id == iepParamedicalServiceId);
                    iepParamedicalService.IsDeleted = true;
                    iepParamedicalService.DeletedOn = DateTime.Now;
                    _uow.GetRepository<IepParamedicalService>().Update(iepParamedicalService);

                    Itp itp = _uow.GetRepository<Itp>().Single(x => x.Id == iepParamedicalServiceId);
                    if (itp != null)
                    {
                        itp.IsDeleted = true;
                        itp.DeletedOn = DateTime.Now;
                        _uow.GetRepository<Itp>().Update(itp);

                        var itpProgressReport = _uow.GetRepository<ItpProgressReport>().GetList(x => x.Id == itp.Id, null, null, 0, 100000, true);
                        if (itpProgressReport != null)
                        {
                            foreach (var item in itpProgressReport.Items)
                            {
                                item.IsDeleted = true;
                                item.DeletedOn = DateTime.Now;
                                _uow.GetRepository<ItpProgressReport>().Update(item);
                            }
                        }
                    }
                    var iepProgressReport = _uow.GetRepository<IepProgressReport>().GetList(x => x.IepId == iepParamedicalService.Iepid);
                    if (iepProgressReport != null)
                    {
                        if (iepProgressReport.Items.Count() > 0)
                        {
                            var iepProgressReportIds = iepProgressReport.Items.Select(x => x.Id).ToArray();
                            var iepProgressReportIdString = String.Join(',', iepProgressReportIds);

                            var cmd = $"update  ProgressReportParamedical set IsDeleted = 1 where ProgressReportId in ({iepProgressReportIdString}) and ParamedicalServiceId = {iepParamedicalService.ParamedicalServiceId}";
                            _iesContext.Database.ExecuteSqlRaw(cmd);
                        }
                    }


                    _uow.SaveChanges();
                    transaction.Commit();
                    return new ResponseDto { Status = 1, Message = "Iep Paramedical Service Deleted Seccessfuly" };
                }
                return new ResponseDto { Status = 0, Message = "null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        #endregion
        #region IepExtraCurricular
        public ResponseDto GetIepExtraCurriculars()
        {
            try
            {
                var allIepExtraCurriculars = _uow.GetRepository<IepExtraCurricular>().GetList(null, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<IepExtraCurricularDto>>(allIepExtraCurriculars);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetIepExtraCurricularByIepId(int iepId)
        {
            try
            {
                var iepExtraCurricular = await _uow.GetRepositoryAsync<IepExtraCurricular>().GetListAsync(x => x.Iepid == iepId && x.IsDeleted != true, null, x => x.Include(x => x.ExtraCurricular).Include(x => x.ExTeacher), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<IepExtraCurricularDto>>(iepExtraCurricular);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto)
        {
            try
            {
                iepExtraCurricularDto.IsDeleted = false;
                iepExtraCurricularDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<IepExtraCurricular>(iepExtraCurricularDto);
                _uow.GetRepository<IepExtraCurricular>().Add(mapper);
                _uow.SaveChanges();
                iepExtraCurricularDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Iep Extra Curricular Added  Seccessfuly", Data = iepExtraCurricularDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public async Task<ResponseDto> EditIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();

                var mapper = _mapper.Map<IepExtraCurricular>(iepExtraCurricularDto);
                _uow.GetRepository<IepExtraCurricular>().Update(mapper);

                var cmd = $"update IXP set  ExTeacherId ={iepExtraCurricularDto.ExTeacherId},ExtraCurricularId={iepExtraCurricularDto.ExtraCurricularId} Where Id ={iepExtraCurricularDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                _uow.SaveChanges();
                transaction.Commit();
                iepExtraCurricularDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Iep Extra Curricular Updated Seccessfuly", Data = iepExtraCurricularDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteIepExtraCurricular(int iepExtraCurricularId)
        {
            try
            {
                if (iepExtraCurricularId > 0)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    IepExtraCurricular iepExtraCurricular = _uow.GetRepository<IepExtraCurricular>().Single(x => x.Id == iepExtraCurricularId);
                    iepExtraCurricular.IsDeleted = true;
                    iepExtraCurricular.DeletedOn = DateTime.Now;

                    Ixp ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == iepExtraCurricularId);
                    if (ixp != null)
                    {
                        ixp.IsDeleted = true;
                        ixp.DeletedOn = DateTime.Now;
                        _uow.GetRepository<Ixp>().Update(ixp);
                    }
                    var iepProgressReport = _uow.GetRepository<IepProgressReport>().GetList(x => x.IepId == iepExtraCurricular.Iepid);
                    if (iepProgressReport != null)
                    {
                        if (iepProgressReport.Items.Count() > 0)
                        {
                            var iepProgressReportIds = iepProgressReport.Items.Select(x => x.Id).ToArray();
                            var iepProgressReportIdString = String.Join(',', iepProgressReportIds);

                            var cmd = $"update  ProgressReportExtraCurricular set IsDeleted = 1 where ProgressReportId in ({iepProgressReportIdString}) and ExtraCurricularId = {iepExtraCurricular.ExtraCurricularId}";
                            _iesContext.Database.ExecuteSqlRaw(cmd);
                        }
                    }
                    _uow.GetRepository<IepExtraCurricular>().Update(iepExtraCurricular);
                    _uow.SaveChanges();
                    transaction.Commit();
                    return new ResponseDto { Status = 1, Message = "Iep ExtraCurricular Deleted Seccessfuly" };
                }
                return new ResponseDto { Status = 0, Message = "null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        #endregion
        #region IepProgressReport
        public async Task<ResponseDto> GetIepProgressReportsByIepId(int iepId)
        {
            try
            {
                var iepProgressReports = await _uow.GetRepositoryAsync<IepProgressReport>().GetListAsync(x => x.IepId == iepId && x.IsDeleted != true, null,
                    x => x.Include(x => x.Student).Include(x => x.AcadmicYear).Include(x => x.Term)
                    .Include(x => x.Teacher).Include(x => x.ProgressReportExtraCurriculars)
                    .Include(x => x.ProgressReportParamedicals.Where(x => x.IsDeleted != true)).Include(x => x.ProgressReportStrands), 0, 100000, true);

                var mapper = _mapper.Map<PaginateDto<IepProgressReportDto>>(iepProgressReports);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetIepProgressReportById(int iepProgressReportId)
        {
            try
            {
                var iepProgressReport = await _uow.GetRepositoryAsync<IepProgressReport>().SingleAsync(x => x.Id == iepProgressReportId && x.IsDeleted != true, null, x => x.Include(x => x.Student)
                   .Include(x => x.AcadmicYear).Include(x => x.Term)
                      .Include(x => x.Teacher)
                      .Include(x => x.ProgressReportExtraCurriculars.Where(x => x.IsDeleted != true)).ThenInclude(x => x.ExtraCurricular)
                      .Include(x => x.ProgressReportParamedicals.Where(x => x.IsDeleted != true)).ThenInclude(x => x.ParamedicalService)
                      .Include(x => x.ProgressReportStrands).ThenInclude(x => x.Strand));

                var mapper = _mapper.Map<IepProgressReportDto>(iepProgressReport);

                var iep = await _uow.GetRepositoryAsync<Iep>().SingleAsync(x => x.Id == iepProgressReport.IepId && x.IsDeleted != true, null, x => x
                 .Include(x => x.IepExtraCurriculars.Where(x => x.IsDeleted != true)).ThenInclude(x => x.ExtraCurricular)
                 .Include(x => x.IepParamedicalServices.Where(x => x.IsDeleted != true)).ThenInclude(x => x.ParamedicalService)
                 .Include(x => x.Goals.Where(x => x.IsDeleted != true)).ThenInclude(x => x.Strand));
                var iepMapper = _mapper.Map<IepDto2>(iep);



                if (iepMapper != null)
                {
                    if (iepMapper.IepExtraCurriculars != null && iepProgressReport.ProgressReportExtraCurriculars != null)
                    {
                        if (iepMapper.IepExtraCurriculars.Count > iepProgressReport.ProgressReportExtraCurriculars.Count)
                        {
                            foreach (var iepExtraCurriculars in iepMapper.IepExtraCurriculars)
                            {
                                if (!iepProgressReport.ProgressReportExtraCurriculars.Any(x => x.ExtraCurricularId == iepExtraCurriculars.ExtraCurricularId))
                                {

                                    mapper.ProgressReportExtraCurriculars.Add(new ProgressReportExtraCurricularDto
                                    {
                                        Id = 0,
                                        ProgressReportId = iepProgressReport.Id,
                                        IepextraCurricularId = iepExtraCurriculars.Id,
                                        ExtraCurricularId = iepExtraCurriculars.ExtraCurricularId,
                                        ExtraCurricularName = iepExtraCurriculars.ExtraCurricularName,
                                        Comment = ""
                                    });
                                }
                            }
                        }
                    }

                    if (iepMapper.IepParamedicalServices != null && iepProgressReport.ProgressReportParamedicals != null)
                    {
                        if (iepMapper.IepParamedicalServices.Count > iepProgressReport.ProgressReportParamedicals.Count)
                        {
                            foreach (var iepParamedicalServices in iepMapper.IepParamedicalServices)
                            {
                                if (!iepProgressReport.ProgressReportParamedicals.Any(x => x.ParamedicalServiceId == iepParamedicalServices.ParamedicalServiceId))
                                {

                                    mapper.ProgressReportParamedicals.Add(new ProgressReportParamedicalDto
                                    {
                                        Id = 0,
                                        ProgressReportId = iepProgressReport.Id,
                                        IepParamedicalSerciveId = iepParamedicalServices.Id,
                                        ParamedicalServiceId = iepParamedicalServices.ParamedicalServiceId,
                                        ParamedicalServiceName = iepParamedicalServices.ParamedicalServiceName,
                                        Comment = ""
                                    });
                                }
                            }
                        }
                    }
                    if (iepMapper.Goals != null && iepProgressReport.ProgressReportStrands != null)
                    {
                        if (iepMapper.Goals.Count > iepProgressReport.ProgressReportStrands.Count)
                        {
                            foreach (var Goal in iepMapper.Goals)
                            {
                                if (!iepProgressReport.ProgressReportStrands.Any(x => x.StrandId == Goal.StrandId))
                                {

                                    mapper.ProgressReportStrands.Add(new ProgressReportStrandDto
                                    {
                                        Id = 0,
                                        ProgressReportId = iepProgressReport.Id,
                                        StrandId = Goal.StrandId,
                                        StrandName = Goal.StrandName,
                                        Comment = ""
                                    });
                                }
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
        public ResponseDto AddIepProgressReport(IepProgressReportDto iepProgressReportDto)
        {
            try
            {
                iepProgressReportDto.IsDeleted = false;
                iepProgressReportDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<IepProgressReport>(iepProgressReportDto);
                if (mapper.ProgressReportParamedicals != null && mapper.ProgressReportParamedicals.Count() > 0)
                {
                    foreach (var item in mapper.ProgressReportParamedicals)
                    {
                        item.IsDeleted = false;
                        item.CreatedOn = DateTime.Now;
                    }
                }
                if (mapper.ProgressReportExtraCurriculars != null && mapper.ProgressReportExtraCurriculars.Count() > 0)
                {
                    foreach (var item in mapper.ProgressReportExtraCurriculars)
                    {
                        item.IsDeleted = false;
                        item.CreatedOn = DateTime.Now;
                    }
                }
                _uow.GetRepository<IepProgressReport>().Add(mapper);
                _uow.SaveChanges();
                iepProgressReportDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Iep Progress Report Added  Seccessfuly", Data = iepProgressReportDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public async Task<ResponseDto> EditIepProgressReport(IepProgressReportDto iepProgressReportDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from ProgressReportExtraCurricular where ProgressReportId={iepProgressReportDto.Id}" +
                    $"delete from ProgressReportParamedical where ProgressReportId={iepProgressReportDto.Id}" +
                    $"delete from ProgressReportStrand where ProgressReportId={iepProgressReportDto.Id}";
                await _iesContext.Database.ExecuteSqlRawAsync(cmd);

                var mapper = _mapper.Map<IepProgressReport>(iepProgressReportDto);


                _iesContext.IepProgressReports.Update(mapper);
                _iesContext.SaveChanges();
                //_uow.GetRepository<IepProgressReport>().Update(mapper);
                //_uow.SaveChanges();
                transaction.Commit();

                return new ResponseDto { Status = 1, Message = "Iep Progress Report Updated Seccessfuly", Data = iepProgressReportDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public async Task<ResponseDto> DeleteIepProgressReport(int iepProgressReportId)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from ProgressReportExtraCurricular where ProgressReportId={iepProgressReportId}" +
                    $"delete from ProgressReportParamedical where ProgressReportId={iepProgressReportId}" +
                    $"delete from ProgressReportStrand where ProgressReportId={iepProgressReportId}";
                await _iesContext.Database.ExecuteSqlRawAsync(cmd);

                IepProgressReport iepProgressReport = _uow.GetRepository<IepProgressReport>().Single(x => x.Id == iepProgressReportId);
                iepProgressReport.IsDeleted = true;
                iepProgressReport.DeletedOn = DateTime.Now;

                var mapper = _mapper.Map<IepProgressReport>(iepProgressReport);


                _iesContext.IepProgressReports.Update(mapper);
                _iesContext.SaveChanges();

                //_uow.GetRepository<IepProgressReport>().Update(mapper);
                //_uow.SaveChanges();
                transaction.Commit();
                return new ResponseDto { Status = 1, Message = "Iep Progress Report Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public async Task<ResponseDto> CreateIepProgressReport(int iepId)
        {
            try
            {
                if (iepId > 0)
                {
                    var iep = await _uow.GetRepositoryAsync<Iep>().SingleAsync(x => x.Id == iepId && x.IsDeleted != true, null, x => x
                .Include(s => s.IepParamedicalServices.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ParamedicalService)
                .Include(s => s.IepExtraCurriculars.Where(x => x.IsDeleted != true)).ThenInclude(s => s.ExtraCurricular)
                .Include(s => s.Student)
                .Include(s => s.AcadmicYear)
                .Include(s => s.Term)
                .Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills)
                .Include(s => s.Goals).ThenInclude(s => s.Strand)
                .Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveEvaluationProcesses)
               );
                    IepProgressReportDto iepProgressReportDto = new IepProgressReportDto();

                    if (iep != null)
                    {

                        iepProgressReportDto.StudentCode = iep.Student == null ? "" : iep.Student.Code.ToString();
                        iepProgressReportDto.StudentName = iep.Student == null ? "" : iep.Student.Name;
                        iepProgressReportDto.AcadmicYearName = iep.AcadmicYear == null ? "" : iep.AcadmicYear.Name;
                        iepProgressReportDto.TermName = iep.Term == null ? "" : iep.Term.Name;


                        iepProgressReportDto.IepId = iep.Id;
                        iepProgressReportDto.StudentId = iep.StudentId;
                        iepProgressReportDto.AcadmicYearId = iep.AcadmicYearId;
                        iepProgressReportDto.TermId = iep.TermId;
                        iepProgressReportDto.TeacherId = iep.TeacherId;
                        iepProgressReportDto.HeadOfEducationId = iep.HeadOfEducation;

                        if (iep.IepExtraCurriculars.Count > 0)
                        {
                            iepProgressReportDto.ProgressReportExtraCurriculars = new List<ProgressReportExtraCurricularDto>();
                            for (int i = 0; i < iep.IepExtraCurriculars.Count; i++)
                            {
                                var ixp = await _uow.GetRepositoryAsync<Ixp>().SingleAsync(x => x.Id == iep.IepExtraCurriculars.ToList()[i].Id && x.IsDeleted != true);
                                string ixpComment = "";
                                if (ixp != null)
                                {
                                    ixpComment = ixp.FooterNotes;

                                }

                                iepProgressReportDto.ProgressReportExtraCurriculars.Add(new ProgressReportExtraCurricularDto
                                {
                                    Id = 0,
                                    ProgressReportId = 0,
                                    IepextraCurricularId = iep.IepExtraCurriculars.ToList()[i].Id,
                                    ExtraCurricularId = iep.IepExtraCurriculars.ToList()[i].ExtraCurricularId == null ? 0 : iep.IepExtraCurriculars.ToList()[i].ExtraCurricularId.Value,
                                    ExtraCurricularName = iep.IepExtraCurriculars.ToList()[i].ExtraCurricular == null ? "" : iep.IepExtraCurriculars.ToList()[i].ExtraCurricular.Name == null ? "" : iep.IepExtraCurriculars.ToList()[i].ExtraCurricular.Name,
                                    ExtraCurricularNameAr = iep.IepExtraCurriculars.ToList()[i].ExtraCurricular == null ? "" : iep.IepExtraCurriculars.ToList()[i].ExtraCurricular.NameAr == null ? "" : iep.IepExtraCurriculars.ToList()[i].ExtraCurricular.NameAr,
                                    Comment = ixpComment
                                });
                            }
                        }
                        if (iep.IepParamedicalServices.Count > 0)
                        {
                            iepProgressReportDto.ProgressReportParamedicals = new List<ProgressReportParamedicalDto>();
                            for (int i = 0; i < iep.IepParamedicalServices.Count; i++)
                            {
                                var itp = _uow.GetRepository<Itp>().Single(x => x.Id == iep.IepParamedicalServices.ToList()[i].Id && x.IsDeleted != true, null,
                                    x => x.Include(x => x.ItpProgressReports));
                                string itpReportComment = "";
                                if (itp != null)
                                {
                                    if (itp.ItpProgressReports != null && itp.ItpProgressReports.Count() > 0)
                                    {
                                        var lastReport = itp.ItpProgressReports.OrderByDescending(x => x.CreatedOn).First();
                                        itpReportComment = lastReport.GeneralComment;
                                    }
                                }
                                iepProgressReportDto.ProgressReportParamedicals.Add(new ProgressReportParamedicalDto
                                {
                                    Id = 0,
                                    ProgressReportId = 0,
                                    IepParamedicalSerciveId = iep.IepParamedicalServices.ToList()[i].Id,
                                    ParamedicalServiceId = iep.IepParamedicalServices.ToList()[i].ParamedicalServiceId == null ? 0 : iep.IepParamedicalServices.ToList()[i].ParamedicalServiceId.Value,
                                    ParamedicalServiceName = iep.IepParamedicalServices.ToList()[i].ParamedicalService == null ? "" : iep.IepParamedicalServices.ToList()[i].ParamedicalService.Name == null ? "" : iep.IepParamedicalServices.ToList()[i].ParamedicalService.Name,
                                    ParamedicalServiceNameAr = iep.IepParamedicalServices.ToList()[i].ParamedicalService == null ? "" : iep.IepParamedicalServices.ToList()[i].ParamedicalService.NameAr == null ? "" : iep.IepParamedicalServices.ToList()[i].ParamedicalService.NameAr,
                                    Comment = itpReportComment
                                }); ;
                            }
                        }
                        if (iep.Goals.Count > 0)
                        {
                            iepProgressReportDto.ProgressReportStrands = new List<ProgressReportStrandDto>();

                            for (int i = 0; i < iep.Goals.Count; i++)
                            {
                                int frsTermPercentage = 0;
                                int scndTermPercentage = 0;
                                if (iep.TermId == 1)
                                {
                                    frsTermPercentage = CalculateMasterdObjPercentage(iep.Goals.ToList()[i]);
                                    iepProgressReportDto.ProgressReportStrands.Add(new ProgressReportStrandDto
                                    {
                                        Id = 0,
                                        ProgressReportId = 0,
                                        StrandId = iep.Goals.ToList()[i].StrandId == null ? 0 : iep.Goals.ToList()[i].StrandId,
                                        StrandName = iep.Goals.ToList()[i].Strand == null ? "" : iep.Goals.ToList()[i].Strand.Name == null ? "" : iep.Goals.ToList()[i].Strand.Name,
                                        GoalLongTermNumber = iep.Goals.ToList()[i].LongTermNumber == null ? 0 : iep.Goals.ToList()[i].LongTermNumber,
                                        GoalShortTermNumber = iep.Goals.ToList()[i].ShortTermProgressNumber == null ? 0 : iep.Goals.ToList()[i].ShortTermProgressNumber,
                                        FirstTermPercentage = frsTermPercentage,
                                        SecondTermPercentage = scndTermPercentage,
                                        Comment = ""
                                    });
                                }
                                else if (iep.TermId == 2)
                                {
                                    var  iepFirstTerm = await _uow.GetRepositoryAsync<Iep>().SingleAsync(x => x.StudentId == iep.StudentId && x.IsDeleted != true && x.AcadmicYearId == iep.AcadmicYearId && x.TermId == 1, null, x => x
                                            .Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills)
                                            .Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveEvaluationProcesses)
                                       );
                                    if (iepFirstTerm != null && iepFirstTerm.Goals.Count > 0)
                                    {
                                        Goal firstTermGoal = iepFirstTerm.Goals.Where(x => x.StrandId == iep.Goals.ToList()[i].StrandId).FirstOrDefault();
                                        if (firstTermGoal != null)
                                        {
                                            frsTermPercentage = CalculateMasterdObjPercentage(firstTermGoal);
                                        }
                                    }
                                    scndTermPercentage = CalculateMasterdObjPercentage(iep.Goals.ToList()[i]);
                                    iepProgressReportDto.ProgressReportStrands.Add(new ProgressReportStrandDto
                                    {
                                        Id = 0,
                                        ProgressReportId = 0,
                                        StrandId = iep.Goals.ToList()[i].StrandId == null ? 0 : iep.Goals.ToList()[i].StrandId.Value,
                                        StrandName = iep.Goals.ToList()[i].Strand == null ? "" : iep.Goals.ToList()[i].Strand.Name == null ? "" : iep.Goals.ToList()[i].Strand.Name,

                                        GoalLongTermNumber = iep.Goals.ToList()[i].LongTermNumber == null ? 0 : iep.Goals.ToList()[i].LongTermNumber,
                                        GoalShortTermNumber = iep.Goals.ToList()[i].ShortTermProgressNumber == null ? 0 : iep.Goals.ToList()[i].ShortTermProgressNumber,

                                        FirstTermPercentage = frsTermPercentage,
                                        SecondTermPercentage = scndTermPercentage,
                                        Comment = ""
                                    });
                                }

                            }
                        }
                    }
                    //var mapper = _mapper.Map<IepProgressReportDto>(iep);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = iepProgressReportDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = " null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        #endregion
        #region ProgressReportParamedical
        public ResponseDto GetProgressReportParamedicalByUserId(int userId)
        {
            try
            {
                var paramedicalIds = _uow.GetRepository<TherapistParamedicalService>().GetList(x => x.UserId == userId, null, null, 0, 100000, true).Items.Select(x => x.ParamedicalServiceId).ToArray();
                if (paramedicalIds != null)
                {

                    var progressReportParamedicals = _uow.GetRepository<ProgressReportParamedical>().GetList(x => paramedicalIds.Contains(x.ParamedicalServiceId.Value == null ? 0 : x.ParamedicalServiceId.Value) && x.IsDeleted != true, null,
                   x => x.Include(x => x.ProgressReport).ThenInclude(x => x.Student)
                   .Include(x => x.ProgressReport).ThenInclude(x => x.AcadmicYear)
                   .Include(x => x.ProgressReport).ThenInclude(x => x.Term)
                   .Include(x => x.ParamedicalService), 0, 100000, true);

                    var mapper = _mapper.Map<PaginateDto<ProgressReportParamedicalDto>>(progressReportParamedicals);
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
        public ResponseDto GetProgressReportParamedicalById(int progressReportParamedicalId)
        {
            try
            {
                var progressReportParamedicals = _uow.GetRepository<ProgressReportParamedical>().Single(x => x.Id == progressReportParamedicalId, null,
                    x => x.Include(x => x.ProgressReport).ThenInclude(x => x.Student)
                    .Include(x => x.ProgressReport).ThenInclude(x => x.AcadmicYear)
                    .Include(x => x.ProgressReport).ThenInclude(x => x.Term)
                    .Include(x => x.ParamedicalService));

                var mapper = _mapper.Map<ProgressReportParamedicalDto>(progressReportParamedicals);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto EditProgressReportParamedical(ProgressReportParamedicalDto progressReportParamedicalDto)
        {
            try
            {
                if (progressReportParamedicalDto != null)
                {
                    var mapper = _mapper.Map<ProgressReportParamedical>(progressReportParamedicalDto);
                    _uow.GetRepository<ProgressReportParamedical>().Update(mapper);
                    _uow.SaveChanges();

                    return new ResponseDto { Status = 1, Message = "(Progress Report Paramedical Updated Seccessfuly", Data = progressReportParamedicalDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteProgressReportParamedicalt(int progressReportParamedicalId)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from ProgressReportParamedical where Id={progressReportParamedicalId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                transaction.Commit();
                return new ResponseDto { Status = 1, Message = "(Progress Report Paramedical Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        #endregion

        public ResponseDto GetSkillsByObjectiveId(int objectiveId)
        {
            try
            {
                var objectiveSkills = _uow.GetRepository<ObjectiveSkill>().GetList(x => x.ObjectiveId == objectiveId, null, null, 0, 100000, true).Items.Select(x => x.SkillId).ToArray();
                var Skills = _uow.GetRepository<Skill>().GetList(x => objectiveSkills.Contains(x.Id), null, null, 0, 100000, true);

                var mapper = _mapper.Map<PaginateDto<SkillDto>>(Skills);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
    }
}
#region Not Neded Now
//ItpGoal itpGoal = _uow.GetRepository<ItpGoal>().Single(x => x.ItpId == itp.Id);
//if (itpGoal != null)
//{
//    itpGoal.IsDeleted = true;
//    itpGoal.DeletedOn = DateTime.Now;

//    ItpGoalObjective itpGoalObjective = _uow.GetRepository<ItpGoalObjective>().Single(x => x.ItpGoalId == itpGoal.Id);
//    if (itpGoalObjective != null)
//    {
//        itpGoalObjective.IsDeleted = true;
//        itpGoalObjective.DeletedOn = DateTime.Now;
//    }
//}
//public ResponseDto ObjIsMasterd(int objId, bool IsMasterd)
//{
//    try
//    {
//        if (objId != 0)
//        {
//            Objective objective = _uow.GetRepository<Objective>().Single(x => x.Id == objId);
//            if (objective!=null)
//            {
//                objective.IsMasterd = IsMasterd;
//                _uow.GetRepository<Objective>().Update(objective);
//                _uow.SaveChanges();
//                return new ResponseDto { Status = 1, Message = "Objective Is Masterd Status Has Changed" };
//            }
//                return new ResponseDto { Status = 1, Message = "null" };

//        }
//        else
//        {
//            return new ResponseDto { Status = 1, Message = "null" };
//        }
//    }
//    catch (Exception ex)
//    {
//        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
//    }
//}


//var goalObjectives = iep.Goals.ToList()[i].Objectives.Where(x => x.IsDeleted != true);

//if (goalObjectives.Count() > 0 && iep.TermId == 1)
//{
//    foreach (var objective in goalObjectives)
//    {
//        // to calculate frsTermPercentage Percentage
//        if (objective.IsMasterd == true)
//        {
//            frsTermPercentage = frsTermPercentage + (objective.ObjectiveNumber == null ? 0 : objective.ObjectiveNumber.Value);
//        }
//    }
//}
//else if (goalObjectives.Count() > 0 && iep.TermId == 2)
//{
//    foreach (var objective in goalObjectives)
//    {
//        // to calculate scndTermPercentage Percentage
//        if (objective.IsMasterd == true)
//        {
//            scndTermPercentage = scndTermPercentage + (objective.ObjectiveNumber == null ? 0 : objective.ObjectiveNumber.Value);
//        }
//    }
//    #region FirstTerm
//    var iepFirstTerm = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true && x.AcadmicYearId == iep.AcadmicYearId && x.TermId == 1, null, x => x
//          .Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills)
//          .Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveEvaluationProcesses)
//       );
//    if (iepFirstTerm != null)
//    {
//        if (iepFirstTerm.Goals.Count() > 0)
//        {
//            for (int j = 0; j < iepFirstTerm.Goals.Count; j++)
//            {
//                var firstTermObjectives = iep.Goals.ToList()[j].Objectives.Where(x => x.IsDeleted != true);
//                if (firstTermObjectives.Count() > 0)
//                {
//                    foreach (var objective in goalObjectives)
//                    {
//                        // to calculate scndTermPercentage Percentage
//                        if (objective.IsMasterd == true)
//                        {
//                            scndTermPercentage = scndTermPercentage + (objective.ObjectiveNumber == null ? 0 : objective.ObjectiveNumber.Value);
//                        }
//                    }
//                }
//            }
//        }
//    }
//    #endregion
//}
//public void UpdateStudentInfo(Iep mapper)
//{
//    try
//    {
//        if (mapper != null && mapper.Id > 0)
//        {
//            var iepParmedicalIds = _uow.GetRepository<IepParamedicalService>().GetList(x => x.Iepid == mapper.Id && x.IsItpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
//            if (iepParmedicalIds.Count() > 0)
//            {
//                var itpList = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true && iepParmedicalIds.Contains(x.Id), null, null, 0, 1000, true).Items;
//                if (itpList.Count() > 0)
//                {
//                    foreach (var item in itpList)
//                    {
//                        item.StudentId = mapper.StudentId;
//                    }
//                    _uow.GetRepository<Itp>().Update(itpList);
//                    _uow.SaveChanges();
//                }
//            }
//            var iepExtraIds = _uow.GetRepository<IepExtraCurricular>().GetList(x => x.Iepid == mapper.Id && x.IsIxpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
//            if (iepExtraIds.Count() > 0)
//            {
//                var ixpList = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true && iepExtraIds.Contains(x.Id), null, null, 0, 1000, true).Items;
//                if (ixpList.Count() > 0)
//                {
//                    foreach (var item in ixpList)
//                    {
//                        item.StudentId = mapper.StudentId;
//                    }
//                    _uow.GetRepository<Ixp>().Update(ixpList);
//                    _uow.SaveChanges();
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        return;
//    }
//}
//public void UpdateAcademicYearInfo(Iep mapper)
//{
//    try
//    {
//        if (mapper != null && mapper.Id > 0)
//        {
//            var iepParmedicalIds = _uow.GetRepository<IepParamedicalService>().GetList(x => x.Iepid == mapper.Id && x.IsItpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
//            if (iepParmedicalIds.Count() > 0)
//            {
//                var itpList = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true && iepParmedicalIds.Contains(x.Id), null, null, 0, 1000, true).Items;
//                if (itpList.Count() > 0)
//                {
//                    foreach (var item in itpList)
//                    {
//                        item.AcadmicYearId = mapper.AcadmicYearId;
//                    }
//                    _uow.GetRepository<Itp>().Update(itpList);
//                    _uow.SaveChanges();
//                }
//            }
//            var iepExtraIds = _uow.GetRepository<IepExtraCurricular>().GetList(x => x.Iepid == mapper.Id && x.IsIxpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
//            if (iepExtraIds.Count() > 0)
//            {
//                var ixpList = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true && iepExtraIds.Contains(x.Id), null, null, 0, 1000, true).Items;
//                if (ixpList.Count() > 0)
//                {
//                    foreach (var item in ixpList)
//                    {
//                        item.AcadmicYearId = mapper.AcadmicYearId;
//                    }
//                    _uow.GetRepository<Ixp>().Update(ixpList);
//                    _uow.SaveChanges();
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        return;
//    }
//}
//public void UpdateTermInfo(Iep mapper)
//{
//    try
//    {
//        if (mapper != null && mapper.Id > 0)
//        {
//            var iepParmedicalIds = _uow.GetRepository<IepParamedicalService>().GetList(x => x.Iepid == mapper.Id && x.IsItpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
//            if (iepParmedicalIds.Count() > 0)
//            {
//                var itpList = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true && iepParmedicalIds.Contains(x.Id), null, null, 0, 1000, true).Items;
//                if (itpList.Count() > 0)
//                {
//                    foreach (var item in itpList)
//                    {
//                        item.TermId = mapper.TermId;
//                    }
//                    _uow.GetRepository<Itp>().Update(itpList);
//                    _uow.SaveChanges();
//                }
//            }
//            var iepExtraIds = _uow.GetRepository<IepExtraCurricular>().GetList(x => x.Iepid == mapper.Id && x.IsIxpCreated == true && x.IsDeleted != true, null, null, 0, 1000, true).Items.Select(x => x.Id).ToArray();
//            if (iepExtraIds.Count() > 0)
//            {
//                var ixpList = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true && iepExtraIds.Contains(x.Id), null, null, 0, 1000, true).Items;
//                if (ixpList.Count() > 0)
//                {
//                    foreach (var item in ixpList)
//                    {
//                        item.TermId = mapper.TermId;
//                    }
//                    _uow.GetRepository<Ixp>().Update(ixpList);
//                    _uow.SaveChanges();
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        return;
//    }
//}
#endregion

