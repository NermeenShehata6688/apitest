using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class SkillEvaluationService : ISkillEvaluationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public SkillEvaluationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public ResponseDto GetSkillEvaluations()
        {
            try
            {
                var allSkillEvaluations = _uow.GetRepository<SkillEvaluation>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 10000, true);
                var mapper = _mapper.Map<PaginateDto<SkillEvaluationDto>>(allSkillEvaluations);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetSkillEvaluationById(int skillEvaluationId)
        {
            try
            {
                var skillEvaluation = _uow.GetRepository<SkillEvaluation>().Single(x => x.Id == skillEvaluationId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<SkillEvaluationDto>(skillEvaluation);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddSkillEvaluation(SkillEvaluationDto skillEvaluationDto)
        {
            try
            {
                skillEvaluationDto.IsDeleted = false;
                skillEvaluationDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<SkillEvaluation>(skillEvaluationDto);
                _uow.GetRepository<SkillEvaluation>().Add(mapper);
                _uow.SaveChanges();
                skillEvaluationDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "skill Evaluation Added  Seccessfuly", Data = skillEvaluationDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditSkillEvaluation(SkillEvaluationDto skillEvaluationDto)
        {
            try
            {
                var mapper = _mapper.Map<SkillEvaluation>(skillEvaluationDto);
                _uow.GetRepository<SkillEvaluation>().Update(mapper);
                _uow.SaveChanges();
                skillEvaluationDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "skill Evaluationo Updated Seccessfuly", Data = skillEvaluationDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteSkillEvaluation(int skillEvaluationId)
        {
            try
            {
                SkillEvaluation oSkillEvaluation = _uow.GetRepository<SkillEvaluation>().Single(x => x.Id == skillEvaluationId);
                oSkillEvaluation.IsDeleted = true;
                oSkillEvaluation.DeletedOn = DateTime.Now;

                _uow.GetRepository<SkillEvaluation>().Update(oSkillEvaluation);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Skill Evaluation Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };

            }
        }
    }
}
