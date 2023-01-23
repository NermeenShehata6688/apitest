using AutoMapper;
using Dapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.EntityFrameworkCore;
using Olsys.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class ItpService : IItpService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        public ItpService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
        }
        public async Task<ResponseDto> GetItpsHelperDapper()
        {
            try
            {
                ItpHelper itpHelper = new ItpHelper();
                using (System.Data.IDbConnection dbConnection = ConnectionManager.GetConnection())
                {
                    dbConnection.Open();

                    var AllDepartments =( await dbConnection.QueryAsync<Department>(SqlGeneralBuilder.Select_All_Department())).OrderBy(x => x.DisplayOrder).ToList();
                    itpHelper.AllDepartments = new PaginateDto<Department>
                    {
                        Items = AllDepartments,
                        Count = AllDepartments.Count()
                    };
                    var allStudents = (await dbConnection.QueryAsync<VwStudent>(SqlGeneralBuilder.Select_All_Students())).ToList();
                    itpHelper.AllStudents = new PaginateDto<VwStudent>
                    {
                        Items = allStudents,
                        Count = allStudents.Count()
                    };
                    var allAcadmicYears =( await dbConnection.QueryAsync<AcadmicYear>(SqlGeneralBuilder.Select_All_AcadimicYears())).ToList();
                    itpHelper.AllAcadmicYears = new PaginateDto<AcadmicYear>
                    {
                        Items = allAcadmicYears,
                        Count = allAcadmicYears.Count()
                    };

                    var allTerms = (await dbConnection.QueryAsync<Term>(SqlGeneralBuilder.Select_AllTerms())).ToList();
                    itpHelper.AllTerms = new PaginateDto<Term>
                    {
                        Items = allTerms,
                        Count = allTerms.Count()
                    };

                    var AllTherapist =( await dbConnection.QueryAsync<User>(SqlGeneralBuilder.Select_AllTherapists())).ToList();
                    itpHelper.AllTherapist = new PaginateDto<User>
                    {
                        Items = AllTherapist,
                        Count = AllTherapist.Count()
                    };

                    var AllTeachers = (await dbConnection.QueryAsync<User>(SqlGeneralBuilder.Select_AllTeachers())).ToList();
                    itpHelper.AllTheachers = new PaginateDto<User>
                    {
                        Items = AllTeachers,
                        Count = AllTeachers.Count()
                    };

                    var AllHeadOfEducations =(await dbConnection.QueryAsync<User>(SqlGeneralBuilder.Select_AllHeadOfEducation())).ToList();
                    itpHelper.AllHeadOfEducations = new PaginateDto<User>
                    {
                        Items = AllHeadOfEducations,
                        Count = AllHeadOfEducations.Count()
                    };

                    var AllParamedicalServices = (await dbConnection.QueryAsync<ParamedicalService>(SqlGeneralBuilder.Select_All_ParamedicalServices())).ToList();
                    itpHelper.AllParamedicalServices = new PaginateDto<ParamedicalService>
                    {
                        Items = AllParamedicalServices,
                        Count = AllParamedicalServices.Count()
                    };

                    var TherapistParamedicalService =( await dbConnection.QueryAsync<TherapistParamedicalService>(SqlGeneralBuilder.Select_All_TherapistParamedicalServices())).ToList();
                    itpHelper.TherapistParamedicalService = new PaginateDto<TherapistParamedicalService>
                    {
                        Items = TherapistParamedicalService,
                        Count = TherapistParamedicalService.Count()
                    };
                    dbConnection.Close();

                }


                return new ResponseDto { Status = 1, Message = "Success", Data = itpHelper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItpsHelper()
        {
            try
            {
                ItpHelper itpHelper = new ItpHelper();
                //ItpHelper itpHelper = new ItpHelper()
                //{
                //    AllDepartments = _uow.GetRepository<Department>().GetList(null, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                //    AllStudents = _uow.GetRepository<VwStudent>().GetList((x => new VwStudent { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code, DepartmentId = x.DepartmentId, DateOfBirth = x.DateOfBirth, IsDeleted = x.IsDeleted }), null, null, null, 0, 100000, true),
                //    AllAcadmicYears = _uow.GetRepository<AcadmicYear>().GetList(null, null, null, 0, 1000000, true),
                //    AllTerms = _uow.GetRepository<Term>().GetList(null, null, null, 0, 1000000, true),
                //    AllTherapist = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, DepartmentId = x.DepartmentId, IsDeleted = x.IsDeleted }), x => x.IsTherapist == true, null, null, 0, 1000000, true),
                //    AllHeadOfEducations = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, IsDeleted = x.IsDeleted }), x => x.IsHeadofEducation == true, null, null, 0, 1000000, true),
                //    AllParamedicalServices = _uow.GetRepository<ParamedicalService>().GetList(null, null, null, 0, 1000000, true),
                //    TherapistParamedicalService = _uow.GetRepository<TherapistParamedicalService>().GetList(null, null, null, 0, 1000000, true),
                //};
                //var mapper = _mapper.Map<ItpHelperDto>(itpHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = itpHelper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItps(ItpSearchDto itpSearchDto)
        {
            try
            {
                var AllItpsx = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true, null,
                    x => x.Include(s => s.Student).ThenInclude(s => s.Department)
                     .Include(s => s.Therapist)
                     .Include(s => s.AcadmicYear)
                     .Include(s => s.Term)
                     .Include(s => s.ParamedicalService), 0, 100000, true);
             
                var AllItps = _mapper.Map<PaginateDto<ItpDto>>(AllItpsx).Items;
                if (itpSearchDto.Student_Id != null)
                {
                    AllItps = AllItps.Where(x => x.StudentId == itpSearchDto.Student_Id).ToList();
                }
                if (itpSearchDto.Teacher_Id != null)
                {
                    AllItps = AllItps.Where(x => x.Student.TeacherId == itpSearchDto.Teacher_Id).ToList();
                }
                if (itpSearchDto.AcadmicYear_Id != null)
                {
                    AllItps = AllItps.Where(x => x.AcadmicYearId == itpSearchDto.AcadmicYear_Id).ToList();
                }
                if (itpSearchDto.ParamedicalService_Id != null)
                {
                    AllItps = AllItps.Where(x => x.ParamedicalServiceId == itpSearchDto.ParamedicalService_Id).ToList();
                }
                if (itpSearchDto.Term_Id != null)
                {
                    AllItps = AllItps.Where(x => x.TermId == itpSearchDto.Term_Id).ToList();
                }
                if (itpSearchDto.Therapist_Id != null)
                {
                    AllItps = AllItps.Where(x => x.TherapistId == itpSearchDto.Therapist_Id).ToList();
                }
               
                if (itpSearchDto.Status != null)
                {
                    AllItps = AllItps.Where(x => x.Status == itpSearchDto.Status).ToList();
                }
                if (itpSearchDto.IsPublished != null)
                {
                    AllItps = AllItps.Where(x => x.IsPublished == itpSearchDto.IsPublished).ToList();
                }
                
               
                //var lstItpDto = _mapper.Map<PaginateDto<ItpDto>>(AllItps).Items;
                if (itpSearchDto.Index == null || itpSearchDto.Index == 0)
                {
                    itpSearchDto.Index = 0;
                }
                else
                {
                    itpSearchDto.Index += 1;
                }
                var mapper = new PaginateDto<ItpDto> { Count = AllItps.Count(), Items = AllItps != null ? AllItps.Skip(itpSearchDto.Index == null || itpSearchDto.PageSize == null ? 0 : ((itpSearchDto.Index.Value - 1) * itpSearchDto.PageSize.Value)).Take(itpSearchDto.PageSize ??= 20).ToList() : AllItps.ToList() };

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItpById(int itpId)
        {
            try
            {
                var itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId && x.IsDeleted != true, null,
                    x => x.Include(x => x.ItpGoals.Where(s => s.IsDeleted != true)).ThenInclude(x => x.ItpGoalObjectives.Where(s => s.IsDeleted != true))
                     .Include(s => s.Student).ThenInclude(s => s.Department)
                     .Include(s => s.Therapist)
                     .Include(s => s.AcadmicYear)
                     .Include(s => s.Term)
                     .Include(s => s.ParamedicalService)
                     .Include(s => s.HeadOfEducation));
                var mapper = _mapper.Map<ItpDto>(itp);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddItp(ItpDto itpDto)
        {
            try
            {
                if (itpDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();

                    itpDto.IsDeleted = false;
                    itpDto.CreatedOn = DateTime.Now;
                    var mapper = _mapper.Map<Itp>(itpDto);
                    _uow.GetRepository<Itp>().Add(mapper);
                    _uow.SaveChanges();

                    var cmd = $"UPDATE IEP_ParamedicalService SET IsItpCreated = 1 Where Id ="+ itpDto.Id;
                    _iesContext.Database.ExecuteSqlRaw(cmd);
                    transaction.Commit();
                    itpDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Itp Added  Seccessfuly", Data = itpDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditItp(ItpDto itpDto)
        {
            if (itpDto != null)
            {
                var mapper = _mapper.Map<Itp>(itpDto);
                _uow.GetRepository<Itp>().Update(mapper);
                _uow.SaveChanges();
                itpDto.Id = mapper.Id;
                itpDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Itp Updated Seccessfuly", Data = itpDto };
            }
            else
            {
                return new ResponseDto { Status = 1, Message = "null" };
            }

        }
        public ResponseDto DeleteItp(int itpId)
        {
            try
            {
                if (itpId >0)
                {
                    var itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId);
                        itp.IsDeleted = true;
                        itp.DeletedOn = DateTime.Now;
                        var mapper = _mapper.Map<Itp>(itp);
                        _uow.GetRepository<Itp>().Update(mapper);
                    
                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Itp Deleted Seccessfuly" };
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
        public ResponseDto ItpStatus(StatusDto statusDto)
        {
            try
            {
                if (statusDto.Id != 0)
                {
                    Itp itp = _uow.GetRepository<Itp>().Single(x => x.Id == statusDto.Id);
                    if (itp != null)
                    {
                        itp.Status = statusDto.StatusNo;
                        _uow.GetRepository<Itp>().Update(itp);
                        _uow.SaveChanges();
                        return new ResponseDto { Status = 1, Message = "Itp Status Has Changed" };
                    }
                    else
                    {
                        return new ResponseDto { Status = 1, Message = "null" };
                    }
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
        public ResponseDto ItpIsPublished(IsPuplishedDto isPuplishedDto)
        {
            try
            {
                if (isPuplishedDto.Id != 0)
                {
                    Itp itp = _uow.GetRepository<Itp>().Single(x => x.Id == isPuplishedDto.Id);
                    if (itp!=null)
                    {
                        itp.IsPublished = isPuplishedDto.IsPuplished;
                        _uow.GetRepository<Itp>().Update(itp);
                        _uow.SaveChanges();
                        return new ResponseDto { Status = 1, Message = "Itp Is Published Status Has Changed" };
                    }
                    else
                    {
                        return new ResponseDto { Status = 1, Message = "null" };
                    }
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
        public ResponseDto ItpDuplicate(int itpId)
        {
            try
            {
                var itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId && x.IsDeleted != true, null,
                    x => x.Include(x => x.ItpGoals.Where(s => s.IsDeleted != true)).ThenInclude(x => x.ItpGoalObjectives.Where(s => s.IsDeleted != true)));

                if (itp != null)
                {
                    itp.Id = 0;
                   
                    if (itp.ItpGoals.Count() > 0)
                    {
                        foreach (var goal in itp.ItpGoals)
                        {
                            goal.Id = 0;
                            if (goal.ItpGoalObjectives.Count() > 0)
                            {

                                goal.ItpGoalObjectives.ToList().ForEach(x => x.Id = 0);
                            }
                        }
                    }
                    itp.Status = 0;
                    _uow.GetRepository<Itp>().Add(itp);
                    _uow.SaveChanges();

                    var mapper = _mapper.Map<ItpDto>(itp);
                    if (mapper.Id > 0)
                    {
                        var objectivesToUpdate = mapper.ItpGoals.SelectMany(x => x.ItpGoalObjectives).ToList().Select(x => x.Id).ToArray();
                        if (objectivesToUpdate.Count() > 0)
                        {
                            string numbersToUpdate = string.Join(",", objectivesToUpdate);
                            var cmd = $"update ITP_GoalObjective set ItpId = {mapper.Id} Where Id IN ({numbersToUpdate})";
                            _iesContext.Database.ExecuteSqlRaw(cmd);
                        }
                    }
                    return new ResponseDto { Status = 1, Message = " ITP has been Duplicated", Data = mapper };
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

        public ResponseDto GetItpGoals()
        {
            try
            {
                var allItpGoals = _uow.GetRepository<ItpGoal>().GetList(x => x.IsDeleted != true,null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ItpGoalDto>>(allItpGoals);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItpGoalById(int itpGoalId)
        {
            try
            {
                if (itpGoalId != 0)
                {
                    var goal = _uow.GetRepository<ItpGoal>().Single(x => x.Id == itpGoalId && x.IsDeleted != true, null, x => x.Include(s => s.ItpGoalObjectives.Where(s => s.IsDeleted != true)));
                    var mapper = _mapper.Map<ItpGoalDto>(goal);
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
        public ResponseDto GetGoalByItpId(int itpId)
        {
            try
            {
                if (itpId != 0)
                {
                    var goals = _uow.GetRepository<ItpGoal>().GetList(x => x.ItpId == itpId && x.IsDeleted != true, null, x => x
               .Include(s => s.ItpGoalObjectives.Where(s => s.IsDeleted != true)), 0, 100000, true);
                    var mapper = _mapper.Map<PaginateDto<ItpGoalDto>>(goals);
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
        public ResponseDto AddItpGoal(ItpGoalDto itpGoalDto)
        {
            try
            {
                if (itpGoalDto != null)
                {
                    itpGoalDto.IsDeleted = false;
                    itpGoalDto.CreatedOn = DateTime.Now;
                    if (itpGoalDto.ItpGoalObjectives != null)
                    {
                        foreach (var objective in itpGoalDto.ItpGoalObjectives)
                        {
                            objective.IsDeleted = false;
                            objective.CreatedOn = DateTime.Now;
                        }
                    }
                    var mapper = _mapper.Map<ItpGoal>(itpGoalDto);
                    _uow.GetRepository<ItpGoal>().Add(mapper);
                    _uow.SaveChanges();
                    itpGoalDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Goal Added  Seccessfuly", Data = itpGoalDto };
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
        public ResponseDto EditItpGoal(ItpGoalDto goalDto)
        {
            try
            {
                if (goalDto != null)
                {
                    //using var transaction = _iesContext.Database.BeginTransaction();
                    //var cmd = $"delete from ITP_GoalObjective where ItpGoalId={goalDto.Id}";
                    //_iesContext.Database.ExecuteSqlRaw(cmd);

                    //foreach (var objective in goalDto.ItpGoalObjectives)
                    //{
                    //    objective.IsDeleted = false;
                    //    objective.CreatedOn = DateTime.Now;
                    //}

                    var mapper = _mapper.Map<ItpGoal>(goalDto);
                    _uow.GetRepository<ItpGoal>().Update(mapper);

                    using var transaction = _iesContext.Database.BeginTransaction();
                    if (mapper.Id != 0 && mapper.ItpGoalObjectives != null || _iesContext.ItpGoalObjectives.Where(x => x.ItpGoalId == mapper.Id) != null)///count>0
                    {
                        if (mapper.ItpGoalObjectives != null)
                        {
                            //// delete old Objectives Ids which are not in edited goal
                            var newObjInt = mapper.ItpGoalObjectives.Select(x => x.Id);
                            _iesContext.ItpGoalObjectives.RemoveRange(_iesContext.ItpGoalObjectives.Where(x => !newObjInt.Contains(x.Id) && x.ItpGoalId == mapper.Id));
                            _iesContext.SaveChanges();
                        }
                        else
                        {
                            _iesContext.ItpGoalObjectives.RemoveRange(_iesContext.ItpGoalObjectives.Where(x => x.ItpGoalId == mapper.Id));
                            _iesContext.SaveChanges();
                        }
                    }



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
        public ResponseDto DeleteItpGoal(int goalId)
        {
            try
            {
                if (goalId != 0)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from ITP_GoalObjective where ItpGoalId={goalId}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);


                    ItpGoal itpGoal = _uow.GetRepository<ItpGoal>().Single(x => x.Id == goalId);
                    itpGoal.IsDeleted = true;
                    itpGoal.DeletedOn = DateTime.Now;

                    _uow.GetRepository<ItpGoal>().Update(itpGoal);
                    _uow.SaveChanges();
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

        public ResponseDto GetItpObjectiveByGoalId(int goalId)
        {
            try
            {
                if (goalId != 0)
                {
                    var objectives = _uow.GetRepository<ItpGoalObjective>().GetList(x => x.ItpGoalId == goalId && x.IsDeleted != true, null, null, 0, 100000, true);
                    var mapper = _mapper.Map<PaginateDto<ItpGoalObjectiveDto>>(objectives);
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

        public ResponseDto GetItpProgressReportsByItpId(int itpId)
        {
            try
             {
                var itpProgressReports = _uow.GetRepository<ItpProgressReport>().GetList(x => x.ItpId == itpId && x.IsDeleted != true, null,
                    x => x.Include(x => x.Student).Include(x => x.AcadmicYear).Include(x => x.Term)
                    .Include(x => x.ParamedicalService).Include(x => x.Therapist));

                var mapper = _mapper.Map<PaginateDto<ItpProgressReportDto>>(itpProgressReports);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItpProgressReportById(int itpProgressReportId)
        {
            try
            {
                var itpProgressReport = _uow.GetRepository<ItpProgressReport>().Single(x => x.Id == itpProgressReportId && x.IsDeleted != true, null, 
                    x => x.Include(x => x.Student)
                  .Include(x => x.AcadmicYear).Include(x => x.Term)
                     .Include(x => x.Teacher)
                      .Include(x => x.Therapist)
                      .Include(x => x.ParamedicalService)
                     .Include(x => x.ItpObjectiveProgressReports).ThenInclude(x => x.ItpObjective));
                var mapper = _mapper.Map<ItpProgressReportDto>(itpProgressReport);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddItpProgressReport(ItpProgressReportDto itpProgressReportDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                itpProgressReportDto.IsDeleted = false;
                itpProgressReportDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<ItpProgressReport>(itpProgressReportDto);
                _uow.GetRepository<ItpProgressReport>().Add(mapper);
                _uow.SaveChanges();


                if (itpProgressReportDto.GeneralComment!=null)
                {
                    var iepProgressParamedical = _uow.GetRepository<ProgressReportParamedical>().GetList(x => x.IepParamedicalSerciveId == itpProgressReportDto.ItpId && x.IsDeleted != true);
          
                    if (iepProgressParamedical!= null && iepProgressParamedical.Items.Count()>0)
                    {
                       var iepProgressParamedicalLast = iepProgressParamedical.Items.OrderByDescending(x => x.CreatedOn).First();

                        iepProgressParamedicalLast.Comment = itpProgressReportDto.GeneralComment;
                        _uow.GetRepository<ProgressReportParamedical>().Update(iepProgressParamedicalLast);
                        _uow.SaveChanges();
                    }
                }

                transaction.Commit();

                itpProgressReportDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Itp Progress Report Added  Seccessfuly", Data = itpProgressReportDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditItpProgressReport(ItpProgressReportDto itpProgressReportDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();

                if (itpProgressReportDto.GeneralComment != null)
                {
                    var iepProgressParamedical = _uow.GetRepository<ProgressReportParamedical>().GetList(x => x.IepParamedicalSerciveId == itpProgressReportDto.ItpId && x.IsDeleted != true);

                    if (iepProgressParamedical != null && iepProgressParamedical.Items.Count() > 0)
                    {
                        var iepProgressParamedicalLast = iepProgressParamedical.Items.OrderByDescending(x => x.CreatedOn).First();

                        iepProgressParamedicalLast.Comment = itpProgressReportDto.GeneralComment;
                        _uow.GetRepository<ProgressReportParamedical>().Update(iepProgressParamedicalLast);
                        _uow.SaveChanges();
                    }
                }

                var cmd = $"delete from ITP_ObjectiveProgressReport where ItpProgressReportId={itpProgressReportDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);

                var mapper = _mapper.Map<ItpProgressReport>(itpProgressReportDto);
                _uow.GetRepository<ItpProgressReport>().Update(mapper);
                _uow.SaveChanges();
                transaction.Commit();

                return new ResponseDto { Status = 1, Message = "Itp Progress Report Updated Seccessfuly", Data = itpProgressReportDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteItpProgressReport(int itpProgressReportId)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from ITP_ObjectiveProgressReport where ItpProgressReportId={itpProgressReportId}" ;
                _iesContext.Database.ExecuteSqlRaw(cmd);

                ItpProgressReport itpProgressReport = _uow.GetRepository<ItpProgressReport>().Single(x => x.Id == itpProgressReportId);
                itpProgressReport.IsDeleted = true;
                itpProgressReport.DeletedOn = DateTime.Now;

                var mapper = _mapper.Map<ItpProgressReport>(itpProgressReport);
                _uow.GetRepository<ItpProgressReport>().Update(mapper);
                _uow.SaveChanges();
                transaction.Commit();
                return new ResponseDto { Status = 1, Message = "Itp Progress Report Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto CreateItpProgressReport(int itpId)
        {
            try
            {
                if (itpId > 0)
                {
                    var itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId && x.IsDeleted != true, null, x => x

               .Include(s => s.Student)
               .Include(s => s.AcadmicYear)
               .Include(s => s.Term)
               .Include(s => s.Therapist)
               .Include(s => s.ParamedicalService)
               .Include(s => s.ItpGoalObjectives.Where(x=> x.IsDeleted!=true)));
                    ItpProgressReportDto itpProgressReportDto = new ItpProgressReportDto();

                    if (itp != null)
                    {
                        itpProgressReportDto.ItpId = itp.Id;
                        itpProgressReportDto.StudentCode = itp.Student == null ? "": itp.Student.Code.ToString();
                        itpProgressReportDto.StudentName = itp.Student == null ? "" : itp.Student.Name;
                        itpProgressReportDto.AcadmicYearName = itp.AcadmicYear == null ? "" : itp.AcadmicYear.Name;
                        itpProgressReportDto.TermName = itp.Term == null ? "" : itp.Term.Name;
                        itpProgressReportDto.TherapistName = itp.Therapist == null ? "" : itp.Therapist.Name;
                        itpProgressReportDto.ParamedicalServiceId = itp.ParamedicalService == null ? 0 : itp.ParamedicalService.Id;
                        itpProgressReportDto.ParamedicalServiceName = itp.ParamedicalService == null ? "" : itp.ParamedicalService.Name;


                        itpProgressReportDto.ItpId = itp.Id;
                        itpProgressReportDto.StudentId = itp.StudentId;
                        itpProgressReportDto.AcadmicYearId = itp.AcadmicYearId;
                        itpProgressReportDto.TermId = itp.TermId;
                        itpProgressReportDto.ParamedicalServiceId = itp.ParamedicalServiceId;
                        itpProgressReportDto.TherapistId = itp.TherapistId;
                        itpProgressReportDto.HeadOfEducationId = itp.HeadOfEducationId;

                        if (itp.ItpGoalObjectives.Count > 0)
                        {
                            itpProgressReportDto.ItpObjectiveProgressReports = new List<ItpObjectiveProgressReportDto>();

                            for (int i = 0; i < itp.ItpGoalObjectives.Count; i++)
                            {
                                itpProgressReportDto.ItpObjectiveProgressReports.Add(new ItpObjectiveProgressReportDto
                                {
                                    Id = 0,
                                    ItpProgressReportId = 0,
                                    ItpObjectiveId = itp.ItpGoalObjectives.ToList()[i].Id == null ? 0 : itp.ItpGoalObjectives.ToList()[i].Id,
                                    ItpObjectiveNote = itp.ItpGoalObjectives.ToList()[i].ObjectiveNote == null ? "" : itp.ItpGoalObjectives.ToList()[i].ObjectiveNote,
                                    Comment = ""
                                });
                            }
                        }
                    }
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = itpProgressReportDto };
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
        public ResponseDto GetIepsForTherapist(int therapistId)
        {
            try
            {
                var iepParamedicalServices = _uow.GetRepository<IepParamedicalService>().GetList(x => x.TherapistId == therapistId && x.IsItpCreated!=true && x.IsDeleted != true, null,
                 x => x.Include(x => x.Iep).ThenInclude(x => x.Student)
                 .Include(x => x.Iep).ThenInclude(x => x.AcadmicYear)
                 .Include(x => x.Iep).ThenInclude(x => x.Term)
                 .Include(x => x.ParamedicalService), 0, 100000, true);

                var mapper = _mapper.Map<PaginateDto<IepParamedicalForTherapistDto>>(iepParamedicalServices);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto CreateItp(int iepParamedicalServiceId)
        {
            try
            {
                var iepParamedicalServices = _uow.GetRepository<IepParamedicalService>().Single(x => x.Id == iepParamedicalServiceId && x.IsItpCreated != true, null,
                 x => x.Include(x => x.Iep).ThenInclude(x => x.Student)
                 .Include(x => x.Iep).ThenInclude(x => x.AcadmicYear)
                 .Include(x => x.Iep).ThenInclude(x => x.Term));

                var mapper = _mapper.Map<IepParamedicalCreateItpDto>(iepParamedicalServices);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        #region NotNeededNow
        //public ResponseDto GetItpObjectives()
        //{
        //    try
        //    {
        //        var allItpObjectives = _uow.GetRepository<ItpGoalObjective>().GetList(null, null, null, 0, 100000, true);
        //        var mapper = _mapper.Map<PaginateDto<ItpGoalObjectiveDto>>(allItpObjectives);
        //        return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
        //    }
        //}
        //public ResponseDto GetItpObjectiveByItpId(int itpId)
        //{
        //    try
        //    {
        //        var itpObjectives = _uow.GetRepository<ItpObjective>().GetList(x => x.ItpId == itpId && x.IsDeleted != true, null);
        //        var mapper = _mapper.Map<PaginateDto<ItpGoalObjectiveDto>>(itpObjectives);
        //        return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
        //    }
        //}
        //public ResponseDto AddItpObjective(ItpGoalObjectiveDto itpObjectiveDto)
        //{
        //    try
        //    {
        //        var mapper = _mapper.Map<ItpObjective>(itpObjectiveDto);
        //        mapper.IsDeleted = false;
        //        mapper.CreatedOn = DateTime.Now;

        //        _uow.GetRepository<ItpObjective>().Add(mapper);
        //        _uow.SaveChanges();
        //        itpObjectiveDto.Id = mapper.Id;
        //        return new ResponseDto { Status = 1, Message = "Itp Objective Added  Seccessfuly", Data = itpObjectiveDto };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }
        //}
        //public ResponseDto EditItpObjective(ItpGoalObjectiveDto itpObjectiveDto)
        //{
        //    try
        //    {
        //        var mapper = _mapper.Map<ItpObjective>(itpObjectiveDto);
        //        _uow.GetRepository<ItpObjective>().Update(mapper);
        //        _uow.SaveChanges();
        //        itpObjectiveDto.Id = mapper.Id;
        //        return new ResponseDto { Status = 1, Message = "Itp Objective Updated Seccessfuly", Data = itpObjectiveDto };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }
        //}
        //public ResponseDto DeleteItpObjective(int itpObjectiveId)
        //{
        //    try
        //    {
        //        ItpObjective oItpObjective = _uow.GetRepository<ItpObjective>().Single(x => x.Id == itpObjectiveId);
        //        oItpObjective.IsDeleted = true;
        //        oItpObjective.DeletedOn = DateTime.Now;

        //        _uow.GetRepository<ItpObjective>().Update(oItpObjective);
        //        _uow.SaveChanges();
        //        return new ResponseDto { Status = 1, Message = "Itp Objective Deleted Seccessfuly" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
        //    }


        //}

        //public ResponseDto DeleteItp(List<ItpDto> itpDto)
        //{
        //    try
        //    {
        //        if (itpDto != null)
        //        {
        //            foreach (var itp in itpDto)
        //            {
        //                itp.IsDeleted = true;
        //                itp.DeletedOn = DateTime.Now;
        //                var mapper = _mapper.Map<Itp>(itp);
        //                _uow.GetRepository<Itp>().Update(mapper);
        //            }
        //            _uow.SaveChanges();
        //            return new ResponseDto { Status = 1, Message = "Itp Deleted Seccessfuly" };
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
        #endregion

        public ResponseDto GetObjectiveByITPId(int itpId)
        {
            try
            {
                var objective = _uow.GetRepository<ItpGoalObjective>().GetList(x => x.ItpId == itpId && x.IsDeleted != true, null,
                    x => x.Include(s => s.ItpGoalObjectiveActivities), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ItpGoalObjectiveDto>>(objective);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public ResponseDto DeleteObjective(int objectiveId)
        {
            try
            {
                if (objectiveId != 0)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from ITP_GoalObjective where Id={objectiveId}";
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

        public ResponseDto GetObjectiveById(int objectiveId)
        {
            try
            {
                var objective = _uow.GetRepository<ItpGoalObjective>().Single(x => x.Id == objectiveId && x.IsDeleted != true, null,
                    x => x.Include(s => s.ItpGoalObjectiveActivities));
                var mapper = _mapper.Map<ItpGoalObjectiveDto>(objective);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public ResponseDto EditObjectiveActivities(ItpGoalObjectiveActivitiesDto objectiveActivitiesDto)
        {
            try
            {
                if (objectiveActivitiesDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from ITP_GoalObjectiveActivity where ItpGoalObjectiveId ={objectiveActivitiesDto.Id}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);

                    var obj = _mapper.Map<ItpGoalObjective>(objectiveActivitiesDto);
                    // objectiveActivitiesDto.IsMasterd = ObjectiveIsMasterd(obj);

                    var mapper = _mapper.Map<ItpGoalObjective>(objectiveActivitiesDto);
                    mapper.IsDeleted = false;
                    _uow.GetRepository<ItpGoalObjective>().Update(mapper);
                    _uow.SaveChanges();
                    objectiveActivitiesDto.Id = mapper.Id;
                    transaction.Commit();

                    //check if object isMasterd
                    //var newObjective = _uow.GetRepository<Objective>().Single(x => x.Id == mapper.Id && x.IsDeleted != true, null, x => x.Include(s => s.Activities));
                    //if (newObjective != null && newObjective.Activities.Count() > 2)
                    //{
                    //    newObjective.IsMasterd = ObjectiveIsMasterd(newObjective);
                    //    if (newObjective.IsMasterd == true)
                    //    {
                    //        newObjective.Date = DateTime.Now;
                    //    }
                    //    _uow.GetRepository<Objective>().Update(mapper);
                    //    _uow.SaveChanges();
                    //}
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

        public ResponseDto GetActivityByObjectiveId(int objectiveId)
        {
            try
            {
                var objActivities = _uow.GetRepository<ItpGoalObjectiveActivity>().GetList(x => x.ItpGoalObjectiveId == objectiveId, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ItpGoalObjectiveActivityDto>>(objActivities);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public ResponseDto DeleteActivity(int activityId)
        {
            try
            {
                if (activityId > 0)
                {
                    var cmd = $"delete from ITP_GoalObjectiveActivity where Id={activityId}";
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


    }
}
