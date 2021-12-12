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
    internal class TermsService : ITermsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public TermsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetTerms()
        {
            try
            {
                var allTerms = _uow.GetRepository<Term>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<TermDto>>(allTerms);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetTermById(int termId)
        {
            try
            {
                var term = _uow.GetRepository<Term>().Single(x => x.Id == termId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<TermDto>(term);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddTerm(TermDto termDto)
        {
            try
            {
                termDto.IsDeleted = false;
                termDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Term>(termDto);
                _uow.GetRepository<Term>().Add(mapper);
                _uow.SaveChanges();
                termDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Term Added  Seccessfuly", Data = termDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditTerm(TermDto termDto)
        {
            try
            {
                var mapper = _mapper.Map<Term>(termDto);
                _uow.GetRepository<Term>().Update(mapper);
                _uow.SaveChanges();
                termDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Term Updated Seccessfuly", Data = termDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteTerm(int termId)
        {
            try
            {
                Term oTerm = _uow.GetRepository<Term>().Single(x => x.Id == termId);
                oTerm.IsDeleted = true;
                oTerm.DeletedOn = DateTime.Now;

                _uow.GetRepository<Term>().Update(oTerm);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Term Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
            throw new NotImplementedException();
        }
    }
}
