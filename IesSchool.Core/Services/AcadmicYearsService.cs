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
    internal class AcadmicYearsService : IAcadmicYearsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public AcadmicYearsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetAcadmicYears()
        {
            try
            {
                var allAcadmicYears = _uow.GetRepository<AcadmicYear>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<AcadmicYearDto>>(allAcadmicYears);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAcadmicYearById(int acadmicYearId)
        {
            try
            {
                var acadmicYear = _uow.GetRepository<AcadmicYear>().Single(x => x.Id == acadmicYearId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<AcadmicYearDto>(acadmicYear);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddAcadmicYear(AcadmicYearDto acadmicYearDto)
        {
            try
            {
                acadmicYearDto.IsDeleted = false;
                acadmicYearDto.CreatedOn = DateTime.Now;
                if (acadmicYearDto.IsCurrent==true)
                {
                    var allAcademicYears = _uow.GetRepository<AcadmicYear>().GetList().Items.ToList();
                    allAcademicYears.ForEach(x => x.IsCurrent = false);
                    _uow.GetRepository<AcadmicYear>().Update(allAcademicYears);
                }
                var mapper = _mapper.Map<AcadmicYear>(acadmicYearDto);
                _uow.GetRepository<AcadmicYear>().Add(mapper);
                _uow.SaveChanges();
                if (acadmicYearDto.IsCurrent == true)
                {
                    var setting = _uow.GetRepository<Setting>().Single();
                    if (setting != null)
                    {
                        setting.CurrentYearId = mapper.Id;
                    }
                   
                    _uow.GetRepository<Setting>().Update(setting);
                    _uow.SaveChanges();
                }
                acadmicYearDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Acadmic Year Added  Seccessfuly", Data = acadmicYearDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditAcadmicYear(AcadmicYearDto acadmicYearDto)
        {
            try
            {
                var mapper = _mapper.Map<AcadmicYear>(acadmicYearDto);
                if (acadmicYearDto.IsCurrent == true)
                {
                    var allAcadmicYearsButCurrent = _uow.GetRepository<AcadmicYear>().GetList(x => x.Id != acadmicYearDto.Id).Items.ToList();
                    allAcadmicYearsButCurrent.ForEach(x => x.IsCurrent = false);
                    _uow.GetRepository<AcadmicYear>().Update(allAcadmicYearsButCurrent);
                }
                _uow.GetRepository<AcadmicYear>().Update(mapper);
                _uow.SaveChanges();
                acadmicYearDto.Id = mapper.Id;
                if (acadmicYearDto.IsCurrent == true)
                {
                    var setting = _uow.GetRepository<Setting>().Single();
                    if (setting != null)
                    {
                        setting.CurrentYearId = mapper.Id;
                    }

                    _uow.GetRepository<Setting>().Update(setting);
                    _uow.SaveChanges();
                }
                return new ResponseDto { Status = 1, Message = "Acadmic Year Updated Seccessfuly", Data = acadmicYearDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteAcadmicYear(int acadmicYearId)
        {
            try
            {
                AcadmicYear oAcadmicYear = _uow.GetRepository<AcadmicYear>().Single(x => x.Id == acadmicYearId);
                oAcadmicYear.IsDeleted = true;
                oAcadmicYear.DeletedOn = DateTime.Now;

                _uow.GetRepository<AcadmicYear>().Update(oAcadmicYear);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Acadmic Year Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

    }
}
