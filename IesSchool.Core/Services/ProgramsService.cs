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
    internal class ProgramsService : IProgramsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProgramsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetPrograms()
        {
            try
            {
                var allPrograms = _uow.GetRepository<Program>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ProgramDto>>(allPrograms);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetProgramById(int ProgramId)
        {
            try
            {
                var Program = _uow.GetRepository<Program>().Single(x => x.Id == ProgramId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<ProgramDto>(Program);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddProgram(ProgramDto ProgramDto)
        {
            try
            {
                ProgramDto.IsDeleted = false;
                ProgramDto.CreatedOn = DateTime.Now;
                
                var mapper = _mapper.Map<Program>(ProgramDto);
                _uow.GetRepository<Program>().Add(mapper);
                _uow.SaveChanges();

                ProgramDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Program Added  Seccessfuly", Data = ProgramDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditProgram(ProgramDto ProgramDto)
        {
            try
            {
                var mapper = _mapper.Map<Program>(ProgramDto);
                
                _uow.GetRepository<Program>().Update(mapper);
                _uow.SaveChanges();
                ProgramDto.Id = mapper.Id;
                
                return new ResponseDto { Status = 1, Message = "Program Updated Seccessfuly", Data = ProgramDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteProgram(int ProgramId)
        {
            try
            {
                Program oProgram = _uow.GetRepository<Program>().Single(x => x.Id == ProgramId);
                oProgram.IsDeleted = true;
                oProgram.DeletedOn = DateTime.Now;

                _uow.GetRepository<Program>().Update(oProgram);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Program Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

    }
}
