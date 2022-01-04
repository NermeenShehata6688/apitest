using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public ResponseDto GetIepsHelper()
        {
            try
            {
                IepHelper iepHelper = new IepHelper()
                {
                    AllDepartments = _uow.GetRepository<Department>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                    AllStudents = _uow.GetRepository<VwStudent>().GetList((x => new VwStudent { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code }), x => x.IsDeleted != true, null, null, 0, 100000, true),
                    AllAcadmicYears = _uow.GetRepository<AcadmicYear>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllTerms = _uow.GetRepository<Term>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllTeachers = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name }), x => x.IsDeleted != true && x.IsTeacher == true, null, null, 0, 1000000, true),
                    AllAssistants = _uow.GetRepository<Assistant>().GetList((x => new Assistant { Id = x.Id, Name = x.Name }), x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllHeadOfEducations = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name }), x => x.IsDeleted != true && x.IsHeadofEducation == true, null, null, 0, 1000000, true),
                    AllAreas = _uow.GetRepository<Area>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllStrands = _uow.GetRepository<Strand>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllSkills = _uow.GetRepository<Skill>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllParamedicalServices = _uow.GetRepository<ParamedicalService>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllExtraCurriculars = _uow.GetRepository<ExtraCurricular>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllSkillEvaluations = _uow.GetRepository<SkillEvaluation>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    Setting = _uow.GetRepository<Setting>().Single(),
                };
                var mapper = _mapper.Map<IepHelperDto>(iepHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
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
                //if (iepSearchDto.RoomNumber != null)
                //{
                //    allIeps = allIeps.Where(x => x.RoomNumber.ToString().Contains(iepSearchDto.RoomNumber.ToString()));
                //}
                //if (iepSearchDto.StudentNameAr != null)
                //{
                //    allIeps = allIeps.Where(x => x.StudentNameAr.Contains(iepSearchDto.StudentNameAr));
                //}
                //if (iepSearchDto.StudentName != null)
                //{
                //    allIeps = allIeps.Where(x => x.StudentName.Contains(iepSearchDto.StudentName));
                //}
                //if (iepSearchDto.StudentCode != null)
                //{
                //    allIeps = allIeps.Where(x => x.StudentCode.ToString().Contains(iepSearchDto.StudentCode.ToString()));
                //}
              

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
        public ResponseDto GetIepById(int iepId)
        {
            try
            {
                if (iepId!=0)
                {
                    var iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true, null, x => x
               .Include(s => s.IepAssistants).ThenInclude(s => s.Assistant)
               .Include(s => s.IepParamedicalServices).ThenInclude(s => s.ParamedicalService)
               .Include(s => s.IepExtraCurriculars).ThenInclude(s => s.ExtraCurricular)
               .Include(s => s.Student).ThenInclude(s => s.Department)
               .Include(s => s.Teacher)
               .Include(s => s.HeadOfDepartmentNavigation)
               .Include(s => s.HeadOfEducationNavigation)
               .Include(s => s.AcadmicYear)
               .Include(s => s.Term)
               .Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills).ThenInclude(s => s.Skill)
               .Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.ObjectiveEvaluationProcesses).ThenInclude(s => s.SkillEvaluation)
               .Include(s => s.Goals).ThenInclude(s => s.Strand)
               .Include(s => s.Goals).ThenInclude(s => s.Area)
               );
                    var mapper = _mapper.Map<GetIepDto>(iep);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = " null"};
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddIep(IepDto iepDto)
        {
            try
            {
                if (iepDto!=null)
                {
                    iepDto.IsDeleted = false;
                    iepDto.CreatedOn = DateTime.Now;
                    var mapper = _mapper.Map<Iep>(iepDto);
                    _uow.GetRepository<Iep>().Add(mapper);
                    _uow.SaveChanges();
                    iepDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Iep Added  Seccessfuly", Data = iepDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null"};
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
                if (iepDto!=null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from IepAssistant where IEPId={iepDto.Id}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);
                    var mapper = _mapper.Map<Iep>(iepDto);
                    mapper.IsDeleted = false;
                    _uow.GetRepository<Iep>().Update(mapper);
                    _uow.SaveChanges();

                    transaction.Commit();

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
        public ResponseDto DeleteIep(List<IepDto> iepDto)
        {
            try
            {
                if (iepDto != null)
                {
                    foreach (var iep in iepDto)
                    {
                        iep.IsDeleted = true;
                        iep.DeletedOn = DateTime.Now;
                        var mapper = _mapper.Map<Iep>(iep);
                        _uow.GetRepository<Iep>().Update(mapper);
                    }
                  
                    _uow.SaveChanges();
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
        public ResponseDto IepStatus(int iepId, int status)
        {
            try
            {
                if (iepId != 0)
                {
                    Iep iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId);
                    iep.Status = status;
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
        public ResponseDto IepIsPublished(int iepId, bool isPublished)
        {
            try
            {
                if (iepId != 0)
                {
                    Iep iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId);
                    iep.IsPublished = isPublished;
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
                if (iepId!=0)
                {
                    var iepObjectives = _uow.GetRepository<Objective>().GetList(x => x.IsDeleted != true&& x.IepId== iepId );
                    if ( iepObjectives.Items.Count()>0)
                    {
                        var iepMasterdObjectives = iepObjectives.Items.Where(x => x.IsMasterd == true).ToList();
                        if (iepMasterdObjectives.Count() >0)
                        {
                             iepMasterdPercentage = ((decimal)(iepMasterdObjectives.Count()) /((decimal)iepObjectives.Items.Count()))*100;
                            return Math.Round( iepMasterdPercentage);
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

        public ResponseDto GetGoals()
        {
            try
            {
                var allGoals = _uow.GetRepository<Goal>().GetList(x => x.IsDeleted != true, null, x=> x.Include(s=> s.Strand).Include(s => s.Area), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<GetGoalDto>>(allGoals);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetGoalById(int goalId)
        {
            try
            {
                if (goalId!=0)
                {
                    var goal = _uow.GetRepository<Goal>().Single(x => x.Id == goalId && x.IsDeleted != true, null, x => x.Include(s => s.Objectives.Where(s => s.IsDeleted != true)).ThenInclude(s => s.ObjectiveEvaluationProcesses).ThenInclude(s => s.SkillEvaluation).Include(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills).ThenInclude(s => s.Skill));
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
        public ResponseDto GetGoalByIepId(int iepId)
        {
            try
            {
                if (iepId != 0)
                {
                    var goals = _uow.GetRepository<Goal>().GetList(x => x.Iepid == iepId && x.IsDeleted != true, null, x => x
               .Include(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills).ThenInclude(s => s.Skill)
               .Include(s => s.Objectives).ThenInclude(s => s.ObjectiveEvaluationProcesses).ThenInclude(s => s.SkillEvaluation)
               .Include(s => s.Strand)
               .Include(s => s.Area)
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
                if (goalDto!=null)
                {
                    goalDto.IsDeleted = false;
                    goalDto.CreatedOn = DateTime.Now;
                    if (goalDto.Objectives != null)
                    {
                        foreach (var objective in goalDto.Objectives)
                        {
                            objective.IsDeleted = false;
                            objective.CreatedOn = DateTime.Now;

                            if (objective.Activities != null && objective.Activities.Count() > 2)
                            {
                                var obj = _mapper.Map<Objective>(objective);
                                objective.IsMasterd = ObjectiveIsMasterd(obj);
                                if (objective.IsMasterd==true)
                                {
                                    objective.Date = DateTime.Now;
                                }
                            }
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
        public ResponseDto EditGoal(GoalDto goalDto)
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
                            _iesContext.SaveChanges();
                            var oldObjAfterDel = _iesContext.Objectives.Where(x => x.GoalId == mapper.Id).ToList();
                            if (oldObjAfterDel.Count() > 0)
                            {
                                foreach (var newObj in mapper.Objectives.ToList())
                                {
                                    if ((newObj.ObjectiveSkills == null || newObj.ObjectiveSkills.Count == 0) && newObj.Id != 0)  /// deit obj  and delete all skills
                                    {
                                        var listofobskills = _iesContext.ObjectiveSkills.Where(x => x.ObjectiveId == newObj.Id);
                                        _iesContext.ObjectiveSkills.RemoveRange(listofobskills);
                                        _iesContext.SaveChanges();
                                    }
                                    if ((newObj.ObjectiveSkills != null || newObj.ObjectiveSkills.Count != 0) && newObj.Id != 0)  /// deit obj  and edit all skills
                                    {
                                        var newObjSkillsInt = newObj.ObjectiveSkills.Select(o => o.Id).ToList();
                                        if (newObjSkillsInt.Count() > 0)
                                        {
                                            var listofobskills = _iesContext.ObjectiveSkills.Where(x => !newObjSkillsInt.Contains(x.Id) && x.ObjectiveId == newObj.Id).ToList();
                                            _iesContext.ObjectiveSkills.RemoveRange(listofobskills);
                                            _iesContext.SaveChanges();
                                        }
                                    }
                                    if ((newObj.ObjectiveEvaluationProcesses == null || newObj.ObjectiveEvaluationProcesses.Count == 0) && newObj.Id != 0)  /// deit obj  and delete all Evaluation
                                    {
                                        var lisObEvaluationProcesses = _iesContext.ObjectiveEvaluationProcesses.Where(x => x.ObjectiveId == newObj.Id).ToList();
                                        _iesContext.ObjectiveEvaluationProcesses.RemoveRange(lisObEvaluationProcesses);
                                        _iesContext.SaveChanges();
                                    }
                                    if ((newObj.ObjectiveEvaluationProcesses != null || newObj.ObjectiveEvaluationProcesses.Count() != 0) && newObj.Id != 0)  /// deit obj  and edit all Evaluation
                                    {
                                        var newObjEvalProcessesInt = newObj.ObjectiveEvaluationProcesses.Select(o => o.Id).ToList();
                                        if (newObjEvalProcessesInt.Count() > 0)
                                        {
                                            var listofEvaluations = _iesContext.ObjectiveEvaluationProcesses.Where(x => !newObjEvalProcessesInt.Contains(x.Id) && x.ObjectiveId == newObj.Id).ToList();
                                            _iesContext.ObjectiveEvaluationProcesses.RemoveRange(listofEvaluations);
                                            _iesContext.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            _iesContext.Objectives.RemoveRange(_iesContext.Objectives.Where(x => x.GoalId == mapper.Id));
                            _iesContext.SaveChanges();
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
                if (goalId!=0)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
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

        public ResponseDto GetObjectives()
        {
            try
            {
                var allObjectives = _uow.GetRepository<Objective>().GetList(x => x.IsDeleted != true, null, x=> x.Include(s=>s.ObjectiveSkills).ThenInclude(s => s.Skill).Include(s => s.ObjectiveEvaluationProcesses).ThenInclude(x=> x.SkillEvaluation), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<GetObjectiveDto>>(allObjectives);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetObjectiveById(int objectiveId)
        {
            try
            {
                var objective = _uow.GetRepository<Objective>().Single(x => x.Id == objectiveId && x.IsDeleted != true, null, x=> x.Include(s=> s.ObjectiveEvaluationProcesses).Include(s => s.ObjectiveSkills).Include(s => s.Activities));
                var mapper = _mapper.Map<ObjectiveDto>(objective);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetObjectiveByIEPId(int iepId)
        {
            try
            {
                var objective = _uow.GetRepository<Objective>().GetList(x => x.IepId == iepId && x.IsDeleted != true, null, x => x.Include(s => s.ObjectiveEvaluationProcesses).Include(s => s.ObjectiveSkills).Include(s => s.Activities));
                var mapper = _mapper.Map<PaginateDto<ObjectiveDto>>(objective);
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
                if (objectiveDto!=null)
                {
                    objectiveDto.IsDeleted = false;
                    objectiveDto.CreatedOn = DateTime.Now;
                    var mapper = _mapper.Map<Objective>(objectiveDto);
                    if (objectiveDto.Activities != null && objectiveDto.Activities.Count() > 2)
                    {
                        mapper.IsMasterd = ObjectiveIsMasterd(mapper);
                        if (mapper.IsMasterd==true)
                        {
                            mapper.Date = DateTime.Now;
                        }
                    }
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
        public ResponseDto EditObjective(ObjectiveDto objectiveDto)
        {
            try
            {
                if (objectiveDto != null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from Objective_EvaluationProcess where ObjectiveId ={objectiveDto.Id}" +
                        $"  delete from Objective_Skill where ObjectiveId ={ objectiveDto.Id}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);

                    if (objectiveDto.Activities != null)
                    {
                        var listint = objectiveDto.Activities.Select(x => x.Id);
                        _iesContext.Activities.RemoveRange(_iesContext.Activities.Where(x => !listint.Contains(x.Id) && x.ObjectiveId == objectiveDto.Id));
                        _uow.SaveChanges();
                    }

                    var mapper = _mapper.Map<Objective>(objectiveDto);
                    mapper.IsDeleted = false;
                    _uow.GetRepository<Objective>().Update(mapper);
                    _uow.SaveChanges();
                    objectiveDto.Id = mapper.Id;
                    transaction.Commit();

                    //check if object isMasterd
                    var newObjective = _uow.GetRepository<Objective>().Single(x => x.Id == mapper.Id && x.IsDeleted != true, null, x => x.Include(s => s.Activities));
                    if (newObjective != null && newObjective.Activities.Count() > 2)
                    {
                        newObjective.IsMasterd = ObjectiveIsMasterd(newObjective);
                        if (newObjective.IsMasterd==true)
                        {
                            newObjective.Date = DateTime.Now;
                        }
                        _uow.GetRepository<Objective>().Update(mapper);
                        _uow.SaveChanges();
                    }
                    return new ResponseDto { Status = 1, Message = "Objective Updated Seccessfuly", Data = objectiveDto };
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
                if (objectiveId!=0)
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
        public bool ObjectiveIsMasterd(Objective objective)
        {
            try
            {
                //// add new Objective
                if (objective.Id == 0 || objective.Activities != null  || objective.Activities.Any(x => x.Evaluation == 3))
                {
                    int eval = 0;
                    for (int i = 0; i < objective.Activities.Count(); i++)
                    {
                        if (objective.Activities.ToList()[i].Evaluation == 3)
                        {
                            eval++;
                            if(eval==3)
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
                //// edit Objective
                //if (objective.Id != 0 || objective.Activities != null || objective.Activities.Count() >= 2 || objective.Activities.Any(x => x.Evaluation == 3))
                //{
                //    //if Activities.Count() >= 2
                //    if (objective.Activities.Count() >= 2)
                //    {
                //        int eval = 0;
                //        for (int i = 0; i < objective.Activities.Count(); i++)
                //        {
                //            if (objective.Activities.ToList()[i].Evaluation == 3)
                //            {
                //                eval++;
                //                if (eval == 3)
                //                    return true;
                //            }
                //            else
                //                eval = 0;
                //        }
                //    }
                //}
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
                var allActivities = _uow.GetRepository<Activity>().GetList();
                var mapper = _mapper.Map<PaginateDto<ActivityDto>>(allActivities);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetActivityById(int activityId)
        {
            try
            {
                var activity = _uow.GetRepository<Activity>().Single(x => x.Id == activityId,null, x=> x.Include(s=> s.Objective));
                var mapper = _mapper.Map<ActivityDto>(activity);
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
                if (activityId>0)
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
        public ResponseDto GetIepParamedicalServiceByIepId(int iepId)
        {
            try
            {
                var iepParamedicalService = _uow.GetRepository<IepParamedicalService>().GetList(x => x.Iepid == iepId,null, x=> x.Include(x=>x.ParamedicalService));
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
        public ResponseDto EditIepParamedicalService(IepParamedicalServiceDto iepParamedicalServiceDto)
        {
            try
            {
                var mapper = _mapper.Map<IepParamedicalService>(iepParamedicalServiceDto);
                _uow.GetRepository<IepParamedicalService>().Update(mapper);
                _uow.SaveChanges();
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
                var cmd = $"delete from IEP_ParamedicalService where Id={iepParamedicalServiceId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Iep Paramedical Service Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

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
        public ResponseDto GetIepExtraCurricularByIepId(int iepId)
        {
            try
            {
                var iepExtraCurricular = _uow.GetRepository<IepExtraCurricular>().GetList(x => x.Iepid == iepId,null, x => x.Include(x => x.ExtraCurricular));
                var mapper = _mapper.Map < PaginateDto<IepExtraCurricularDto>>(iepExtraCurricular);
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
        public ResponseDto EditIepExtraCurricular(IepExtraCurricularDto iepExtraCurricularDto)
        {
            try
            {
                var mapper = _mapper.Map<IepExtraCurricular>(iepExtraCurricularDto);
                _uow.GetRepository<IepExtraCurricular>().Update(mapper);
                _uow.SaveChanges();
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
                var cmd = $"delete from IEP_ExtraCurricular where Id={iepExtraCurricularId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Iep Extra Curricular Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}

