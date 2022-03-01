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
    internal class SettingService: ISettingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public SettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetSetting()
        {

            try
            {
                var setting = _uow.GetRepository<Setting>().Single(null,null,x=> x.Include( x => x.CurrentYear).Include(x => x.CurrentTerm));
                var mapper = _mapper.Map<SettingDto>(setting);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto EditSetting(SettingDto settingDto)
        {
            try
            {
                var mapper = _mapper.Map<Setting>(settingDto);
                _uow.GetRepository<Setting>().Update(mapper);
                _uow.SaveChanges();

                settingDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Setting Updated Seccessfuly", Data = settingDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto AddAboutUs(SettingDto settingDto)
        {
            try
            {
                var mapper = _mapper.Map<Setting>(settingDto);
                _uow.GetRepository<Setting>().Update(mapper);
                _uow.SaveChanges();

                settingDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "AboutUs has Changed Seccessfuly", Data = settingDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto AboutUs()
        {
            try
            {

                var setting = _uow.GetRepository<Setting>().Single();
                var mapper = _mapper.Map<SettingDto>(setting);
                if (mapper != null)
                    return new ResponseDto { Status = 1, Message = "Seccess", Data = mapper.AboutUs };
                else
                    return new ResponseDto { Status = 0, Errormessage = "null" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = "faild ", Data = ex };

            }
        }
    }
}
