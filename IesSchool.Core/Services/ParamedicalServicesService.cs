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
    
    internal class ParamedicalServicesService : IParamedicalServicesService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ParamedicalServicesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetParamedicalServices()
        {
            try
            {
                var allParamedicalServices = _uow.GetRepository<ParamedicalService>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 10000, true);
                var mapper = _mapper.Map<PaginateDto<ParamedicalServiceDto>>(allParamedicalServices);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetParamedicalServiceById(int paramedicalServiceId)
        {
            try
            {
                var paramedicalService = _uow.GetRepository<ParamedicalService>().Single(x => x.Id == paramedicalServiceId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<ParamedicalServiceDto>(paramedicalService);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddParamedicalService(ParamedicalServiceDto paramedicalServiceDto)
        {
            try
            {
                paramedicalServiceDto.IsDeleted = false;
                paramedicalServiceDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<ParamedicalService>(paramedicalServiceDto);
                _uow.GetRepository<ParamedicalService>().Add(mapper);
                _uow.SaveChanges();
                paramedicalServiceDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Paramedical Service Added  Seccessfuly", Data = paramedicalServiceDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditParamedicalService(ParamedicalServiceDto paramedicalServiceDto)
        {
            try
            {
                var mapper = _mapper.Map<ParamedicalService>(paramedicalServiceDto);
                _uow.GetRepository<ParamedicalService>().Update(mapper);
                _uow.SaveChanges();
                paramedicalServiceDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Paramedical Service Updated Seccessfuly", Data = paramedicalServiceDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteParamedicalService(int paramedicalServiceId)
        {
            try
            {
                ParamedicalService oParamedicalService = _uow.GetRepository<ParamedicalService>().Single(x => x.Id == paramedicalServiceId);
                oParamedicalService.IsDeleted = true;
                oParamedicalService.DeletedOn = DateTime.Now;

                _uow.GetRepository<ParamedicalService>().Update(oParamedicalService);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Paramedical Service Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
