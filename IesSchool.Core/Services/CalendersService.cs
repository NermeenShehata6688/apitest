using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;

namespace IesSchool.Core.Services
{
    public class CalendersService:ICalendersService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        public CalendersService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
        }
        public ResponseDto GetCalenders()
        {
            try
            {
                var allcalenders = _uow.GetRepository<Calender>().GetList(x => x.IsDeleted != true , null);
                var mapper = _mapper.Map<PaginateDto<CalenderDto>>(allcalenders);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetCalenderById(int id)
        {
            try
            {
                var entities = _uow.GetRepository<Calender>().Single(x => x.Id == id && x.IsDeleted != true, null);
                var mapper = _mapper.Map<CalenderDto>(entities);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddEditCalender(CalenderDto calenderDto)
        {
            try
            {
                if (!calenderDto.Id.HasValue)
                {
                    var mapper = _mapper.Map<Calender>(calenderDto);
                    mapper.CreatedOn= DateTime.Now;
                    _uow.GetRepository<Calender>().Add(mapper);
                    _uow.SaveChanges();
                    calenderDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Calender  Added  Seccessfuly", Data = calenderDto };
                }
                else {
                    var mapper = _mapper.Map<Calender>(calenderDto);
                    _uow.GetRepository<Calender>().Update(mapper);
                    _uow.SaveChanges();
                    calenderDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Calender  Updated Seccessfuly", Data = calenderDto };
                }
              
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
  
        public ResponseDto DeleteCalender(int id)
        {
            try
            {
                _iesContext.Calenders.Remove(_iesContext.Calenders.FirstOrDefault(x => x.Id == id)!);
                _iesContext.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Calender Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetCalendersCategories()
        {
            try
            {
                var entities = _uow.GetRepository<CalenderCategory>().GetList(x => x.IsDeleted != true, null);
                var mapper = _mapper.Map<PaginateDto<CalenderCategoryDto>>(entities);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public ResponseDto GetCalenderCategoryById(int id)
        {
            try
            {
                var entities = _uow.GetRepository<CalenderCategory>().Single(x => x.Id == id && x.IsDeleted != true, null);
                var mapper = _mapper.Map<CalenderCategoryDto>(entities);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public ResponseDto DeleteCalenderCategory(int id)
        {
            var entity = _uow.GetRepository<CalenderCategory>().Single(x => x.Id == id && x.IsDeleted != true, null);
            entity.IsDeleted= true;
            entity.IsDeleted= true;

            _uow.GetRepository<CalenderCategory>().Update(entity);
            _uow.SaveChanges();
            return new ResponseDto { Status = 1, Message = "Calender  Deleted Seccessfuly" };
        }

        public ResponseDto AddEditCalenderCategory(CalenderCategoryDto calenderDto)
        {
            try
            {
                if (!calenderDto.Id.HasValue)
                {
                    var mapper = _mapper.Map<CalenderCategory>(calenderDto);
                    _uow.GetRepository<CalenderCategory>().Add(mapper);
                    _uow.SaveChanges();
                    calenderDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Calender  Added  Seccessfuly", Data = calenderDto };
                }
                else
                {
                    var mapper = _mapper.Map<CalenderCategory>(calenderDto);
                    _uow.GetRepository<CalenderCategory>().Update(mapper);
                    _uow.SaveChanges();
                    calenderDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Calender  Updated Seccessfuly", Data = calenderDto };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
