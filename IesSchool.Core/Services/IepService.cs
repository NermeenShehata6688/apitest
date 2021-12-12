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
        public ResponseDto GetIeps()
        {
            try
            {
                var allIeps = _uow.GetRepository<Iep>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<IepDto>>(allIeps);
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
                var iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true,null,x=> x.Include(s=> s.IepAssistants));
                var mapper = _mapper.Map<IepDto>(iep);
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
                var allIepAssistants = _uow.GetRepository<IepAssistant>().GetList(x => x.AssistantId == iepDto.Id);
                var cmd = $"delete from IepAssistant where IEPId={iepDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                var mapper = _mapper.Map<Iep>(iepDto);

                try
                {
                    _uow.GetRepository<Iep>().Update(mapper);
                    _uow.SaveChanges();
                }
                catch (Exception)
                {
                    _uow.GetRepository<IepAssistant>().Add(allIepAssistants.Items);
                    _uow.SaveChanges();
                    throw;
                }
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

        public ResponseDto GetGoals()
        {
            try
            {
                var allGoals = _uow.GetRepository<Goal>().GetList(x => x.IsDeleted != true, null, x=> x.Include(s=> s.Area).Include(s => s.Strand), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<GoalDto>>(allGoals);
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
                var goal = _uow.GetRepository<Goal>().Single(x => x.Id == goalId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<GoalDto>(goal);
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
                _uow.SaveChanges();
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
                Goal oGoal = _uow.GetRepository<Goal>().Single(x => x.Id == goalId);
                oGoal.IsDeleted = true;
                oGoal.DeletedOn = DateTime.Now;

                _uow.GetRepository<Goal>().Update(oGoal);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Goal Deleted Seccessfuly" };
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
                IepParamedicalService oIepParamedicalService = _uow.GetRepository<IepParamedicalService>().Single(x => x.Id == iepParamedicalServiceId);
               

                _uow.GetRepository<IepParamedicalService>().Update(oIepParamedicalService);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Iep Paramedical Service Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
