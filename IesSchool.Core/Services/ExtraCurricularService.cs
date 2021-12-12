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
    internal class ExtraCurricularService : IExtraCurricularService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ExtraCurricularService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetExtraCurriculars()
        {
            try
            {
                var allExtraCurriculars = _uow.GetRepository<ExtraCurricular>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ExtraCurricularDto>>(allExtraCurriculars);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetExtraCurricularById(int extraCurricularId)
        {
            try
            {
                var extraCurricular = _uow.GetRepository<ExtraCurricular>().Single(x => x.Id == extraCurricularId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<ExtraCurricularDto>(extraCurricular);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddExtraCurricular(ExtraCurricularDto extraCurricularDto)
        {
            try
            {
                extraCurricularDto.IsDeleted = false;
                extraCurricularDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<ExtraCurricular>(extraCurricularDto);
                _uow.GetRepository<ExtraCurricular>().Add(mapper);
                _uow.SaveChanges();
                extraCurricularDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Extra Curricular Added  Seccessfuly", Data = extraCurricularDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditExtraCurricular(ExtraCurricularDto extraCurricularDto)
        {
            try
            {
                var mapper = _mapper.Map<ExtraCurricular>(extraCurricularDto);
                _uow.GetRepository<ExtraCurricular>().Update(mapper);
                _uow.SaveChanges();
                extraCurricularDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Extra Curricular Updated Seccessfuly", Data = extraCurricularDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteExtraCurricular(int extraCurricularId)
        {
            try
            {
                ExtraCurricular oExtraCurricular = _uow.GetRepository<ExtraCurricular>().Single(x => x.Id == extraCurricularId);
                oExtraCurricular.IsDeleted = true;
                oExtraCurricular.DeletedOn = DateTime.Now;

                _uow.GetRepository<ExtraCurricular>().Update(oExtraCurricular);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Extra Curricular Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

    }

}
