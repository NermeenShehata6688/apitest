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
                    AllStudents = _uow.GetRepository<VwStudent>().GetList(x => x.IsDeleted != true,null, null, 0, 100000, true),
                    AllAcadmicYears = _uow.GetRepository<AcadmicYear>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllTerms = _uow.GetRepository<Term>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllTeachers = _uow.GetRepository<User>().GetList(x => x.IsDeleted != true && x.IsTeacher == true, null, null, 0, 1000000, true),
                    AllAssistants = _uow.GetRepository<Assistant>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllHeadOfEducations = _uow.GetRepository<User>().GetList(x => x.IsDeleted != true && x.IsHeadofEducation == true, null, null, 0, 1000000, true),
                    AllAreas = _uow.GetRepository<Area>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 1000000, true),
                    AllStrands = _uow.GetRepository<Strand>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 1000000, true),
                    AllSkills = _uow.GetRepository<Skill>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 1000000, true),
                    AllParamedicalServices = _uow.GetRepository<ParamedicalService>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
                    AllExtraCurriculars = _uow.GetRepository<ExtraCurricular>().GetList(x => x.IsDeleted != true, null, null, 0, 1000000, true),
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

                if (iepSearchDto.AcadmicYearId != null)
                {
                    allIeps = allIeps.Where(x => x.AcadmicYearId == iepSearchDto.AcadmicYearId);
                }
                if (iepSearchDto.TermId != null)
                {
                    allIeps = allIeps.Where(x => x.TermId == iepSearchDto.TermId);
                }
                if (iepSearchDto.TeacherId != null)
                {
                    allIeps = allIeps.Where(x => x.TeacherId == iepSearchDto.TeacherId);
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
                if (iepSearchDto.RoomNumber != null)
                {
                    allIeps = allIeps.Where(x => x.RoomNumber.ToString().Contains(iepSearchDto.RoomNumber.ToString()));
                }
                if (iepSearchDto.IsPublished != null)
                {
                    allIeps = allIeps.Where(x => x.IsPublished == iepSearchDto.IsPublished);
                }
                if (iepSearchDto.StudentNameAr != null)
                {
                    allIeps = allIeps.Where(x => x.StudentNameAr.Contains(iepSearchDto.StudentNameAr));
                }
                if (iepSearchDto.StudentName != null)
                {
                    allIeps = allIeps.Where(x => x.StudentName.Contains(iepSearchDto.StudentName));
                }
                if (iepSearchDto.StudentCode != null)
                {
                    allIeps = allIeps.Where(x => x.StudentCode.ToString().Contains(iepSearchDto.StudentCode.ToString()));
                }
                if (iepSearchDto.DepartmentId != null)
                {
                    allIeps = allIeps.Where(x => x.DepartmentId == iepSearchDto.DepartmentId);
                }

                var lstIepDto = _mapper.Map<List<VwIepDto>>(allIeps);
                var mapper = new PaginateDto<VwIepDto> { Count = allIeps.Count(), Items = lstIepDto != null ? lstIepDto.Skip(iepSearchDto.Index ??= 0).Take(iepSearchDto.PageSize ??= 20).ToList() : lstIepDto.ToList() };
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
                var iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true,null,x=> x.Include(s=> s.IepAssistants).Include(s => s.IepParamedicalServices).Include(s => s.IepExtraCurriculars).Include(s => s.Goals));
                var mapper = _mapper.Map<GetIepDto>(iep);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
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
                iepDto.IsDeleted = false;
                iepDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Iep>(iepDto);
                _uow.GetRepository<Iep>().Add(mapper);
                _uow.SaveChanges();
                iepDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Iep Added  Seccessfuly", Data = iepDto };
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
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from IepAssistant where IEPId={iepDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                var mapper = _mapper.Map<Iep>(iepDto);

                _uow.GetRepository<Iep>().Update(mapper);
                _uow.SaveChanges();

                transaction.Commit();

                iepDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Iep Updated Seccessfuly", Data = iepDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteIep(int iepId)
        {
            try
            {
                Iep oIep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId);
                oIep.IsDeleted = true;
                oIep.DeletedOn = DateTime.Now;

                _uow.GetRepository<Iep>().Update(oIep);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Iep Deleted Seccessfuly" };
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
                Iep iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId);
                iep.Status = status;
                _uow.GetRepository<Iep>().Update(iep);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Iep Status Has Changed" };
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
                Iep iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId);
                iep.IsPublished = isPublished;
                _uow.GetRepository<Iep>().Update(iep);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Iep Is Published Status Has Changed" };
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
                var goal = _uow.GetRepository<Goal>().Single(x => x.Id == goalId && x.IsDeleted != true, null, x => x.Include(s => s.Objectives.Where(s => s.IsDeleted != true)).ThenInclude(s => s.ObjectiveEvaluationProcesses).ThenInclude(s => s.SkillEvaluation).Include(s => s.Objectives).ThenInclude(s => s.ObjectiveSkills));
                var mapper = _mapper.Map<GetGoalDto>(goal);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
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
                goalDto.IsDeleted = false;
                goalDto.CreatedOn = DateTime.Now;
                if (goalDto.Objectives!=null)
                {
                    foreach (var objective in goalDto.Objectives)
                    {
                        objective.IsDeleted = false;
                        objective.CreatedOn = DateTime.Now;

                        if (objective.Activities != null && objective.Activities.Count() > 2)
                        {
                            var obj = _mapper.Map<Objective>(objective);
                            objective.IsMasterd = ObjectiveIsMasterd(obj);
                        }
                    }
                }
                var mapper = _mapper.Map<Goal>(goalDto);
                _uow.GetRepository<Goal>().Add(mapper);
                _uow.SaveChanges();
                goalDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Goal Added  Seccessfuly", Data = goalDto };
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
                var mapper = _mapper.Map<Goal>(goalDto);
                _uow.GetRepository<Goal>().Update(mapper);

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
                        if (oldObjAfterDel.Count()>0)
                        {
                            foreach (var newObj in mapper.Objectives.ToList())
                            {
                                if ((newObj.ObjectiveSkills == null|| newObj.ObjectiveSkills.Count==0) && newObj.Id != 0)  /// deit obj  and delete all skills
                                {
                                    var listofobskills = _iesContext.ObjectiveSkills.Where(x => x.ObjectiveId == newObj.Id);
                                    _iesContext.ObjectiveSkills.RemoveRange(listofobskills);
                                    _iesContext.SaveChanges();
                                }
                                if ((newObj.ObjectiveSkills != null|| newObj.ObjectiveSkills.Count!=0) && newObj.Id != 0)  /// deit obj  and edit all skills
                                {
                                    var newObjSkillsInt = newObj.ObjectiveSkills.Select(o => o.Id).ToList();
                                    if (newObjSkillsInt.Count() > 0)
                                    {
                                        var listofobskills = _iesContext.ObjectiveSkills.Where(x => !newObjSkillsInt.Contains(x.Id) && x.ObjectiveId == newObj.Id).ToList();
                                        _iesContext.ObjectiveSkills.RemoveRange(listofobskills);
                                        _iesContext.SaveChanges();
                                    }
                                }
                                if ((newObj.ObjectiveEvaluationProcesses == null|| newObj.ObjectiveEvaluationProcesses.Count==0) && newObj.Id != 0)  /// deit obj  and delete all Evaluation
                                {
                                    var lisObEvaluationProcesses = _iesContext.ObjectiveEvaluationProcesses.Where(x => x.ObjectiveId == newObj.Id).ToList();
                                    _iesContext.ObjectiveEvaluationProcesses.RemoveRange(lisObEvaluationProcesses);
                                    _iesContext.SaveChanges();
                                }
                                if ((newObj.ObjectiveEvaluationProcesses != null|| newObj.ObjectiveEvaluationProcesses.Count()!=0) && newObj.Id != 0)  /// deit obj  and edit all Evaluation
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
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteGoal(int goalId)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from Goals where Id={goalId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                transaction.Commit();

                return new ResponseDto { Status = 1, Message = "Goal Deleted Seccessfuly" };
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
                var allObjectives = _uow.GetRepository<Objective>().GetList(x => x.IsDeleted != true, null, x=> x.Include(s=>s.ObjectiveSkills).Include(s => s.ObjectiveEvaluationProcesses).ThenInclude(x=> x.SkillEvaluation), 0, 100000, true);
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
        public ResponseDto AddObjective(ObjectiveDto objectiveDto)
        {
            try
            {
                objectiveDto.IsDeleted = false;
                objectiveDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Objective>(objectiveDto);
                if (objectiveDto.Activities != null && objectiveDto.Activities.Count() > 2)
                {
                    mapper.IsMasterd = ObjectiveIsMasterd(mapper);
                }
                _uow.GetRepository<Objective>().Add(mapper);
                _uow.SaveChanges();
                objectiveDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Objective Added  Seccessfuly", Data = objectiveDto };
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
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from Objective_EvaluationProcess where ObjectiveId ={objectiveDto.Id}" +
                    $"  delete from Objective_Skill where ObjectiveId ={ objectiveDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);

                if (objectiveDto.Activities!=null)
                {
                    var listint = objectiveDto.Activities.Select(x => x.Id);
                    _iesContext.Activities.RemoveRange(_iesContext.Activities.Where(x => !listint.Contains(x.Id) && x.ObjectiveId == objectiveDto.Id));
                    _uow.SaveChanges();
                }
               
                var mapper = _mapper.Map<Objective>(objectiveDto);
               
                _uow.GetRepository<Objective>().Update(mapper);
                _uow.SaveChanges();
                objectiveDto.Id = mapper.Id;
                transaction.Commit();

                //check if object isMasterd
                var newObjective = _uow.GetRepository<Objective>().Single(x => x.Id == mapper.Id && x.IsDeleted != true, null, x => x.Include(s => s.Activities));
                if (newObjective!=null && newObjective.Activities.Count()>2)
                {
                    newObjective.IsMasterd = ObjectiveIsMasterd(newObjective);
                    _uow.GetRepository<Objective>().Update(mapper);
                    _uow.SaveChanges();
                }
                return new ResponseDto { Status = 1, Message = "Objective Updated Seccessfuly", Data = objectiveDto };
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
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from Objective where Id={objectiveId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                transaction.Commit();

                return new ResponseDto { Status = 1, Message = "Objective Deleted Seccessfuly" };
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
                var mapper = _mapper.Map<Activity>(activityDto);
                _uow.GetRepository<Activity>().Add(mapper);
                _uow.SaveChanges();
                activityDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Activity Added  Seccessfuly", Data = activityDto };
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
                var mapper = _mapper.Map<Activity>(activityDto);
                _uow.GetRepository<Activity>().Update(mapper);
                _uow.SaveChanges();
                activityDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Activity Updated Seccessfuly", Data = activityDto };
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
                var cmd = $"delete from Activities where Id={activityId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Activity Deleted Seccessfuly" };
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
        public ResponseDto GetIepParamedicalServiceById(int iepParamedicalServiceId)
        {
            try
            {
                var iepParamedicalService = _uow.GetRepository<IepParamedicalService>().Single(x => x.Id == iepParamedicalServiceId);
                var mapper = _mapper.Map<IepParamedicalServiceDto>(iepParamedicalService);
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
        public ResponseDto GetIepExtraCurricularById(int iepExtraCurricularId)
        {
            try
            {
                var iepExtraCurricular = _uow.GetRepository<IepExtraCurricular>().Single(x => x.Id == iepExtraCurricularId);
                var mapper = _mapper.Map<IepExtraCurricularDto>(iepExtraCurricular);
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

