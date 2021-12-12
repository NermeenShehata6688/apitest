using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using IesSchool.InfraStructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    public class LocationServices : ILocationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LocationServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto AllCountries()
        {
            try
            {
                var allcountries = _uow.GetRepository<Country>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<CountryDto>>(allcountries);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {

                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AllStates()
        {
            try
            {
                var allStates = _uow.GetRepository<State>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<StateDto>>(allStates);
                return new ResponseDto { Status = 1, Message = " Seccess, Get All States", Data = mapper };
            }
            catch (Exception ex)
            {

                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AllCities()
        {
            try
            {
                var allStates = _uow.GetRepository<City>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 20, true);
                var mapper = _mapper.Map<PaginateDto<CityDto>>(allStates);
                return new ResponseDto { Status = 1, Message = " Seccess, Get All Cities", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetCitiesByStateId(int stateId)
        {
            try
            {
                var state = _uow.GetRepository<State>().Single(x => x.Id == stateId && x.IsDeleted!=true ,null, x => (x.Include(x => x.Cities.Where(x=>x.IsDeleted!=true) )));
                var mapper = _mapper.Map<StateDto>(state);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AllStatesWithCities()
        {
            try
            {
                var allStates = _uow.GetRepository<State>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(x => x.Cities.Where(x=> x.IsDeleted!=true)), 0, 10000, true);
                var mapper = _mapper.Map<PaginateDto<StateDto>>(allStates);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };

            }
        }
        public ResponseDto AddCountry(CountryDto countryDto)
        {
            try
            {
                countryDto.IsDeleted = false;
                countryDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Country>(countryDto);
                _uow.GetRepository<Country>().Add(mapper);
                _uow.SaveChanges();
                countryDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = " Country Added  Seccessfuly", Data = countryDto };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto AddState(StateDto stateDto)
        {
            try
            {
                stateDto.IsDeleted = false;
                stateDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<State>(stateDto);
                _uow.GetRepository<State>().Add(mapper);
                _uow.SaveChanges();
                stateDto.Id=mapper.Id;
                return new ResponseDto { Status = 1, Message = " State Added  Seccessfuly", Data = stateDto };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto AddCity(CityDto cityDto)
        {
            try
            {
                cityDto.IsDeleted = false;
                cityDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<City>(cityDto);
                _uow.GetRepository<City>().Add(mapper);
                _uow.SaveChanges();
                cityDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "City Added  Seccessfuly", Data = cityDto };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteCountry(int countryId)
        {
            try
            {
                Country oCountry = _uow.GetRepository<Country>().Single(x => x.Id == countryId);
                oCountry.IsDeleted = true;
                oCountry.DeletedOn = DateTime.Now;

                //var mapper = _mapper.Map<City>(city);
                _uow.GetRepository<Country>().Update(oCountry);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "County Deleted Seccessfuly" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteState(int stateId)
        {
            try
            {
                State oState = _uow.GetRepository<State>().Single(x => x.Id == stateId);
                oState.IsDeleted = true;
                oState.DeletedOn = DateTime.Now;

                //var mapper = _mapper.Map<City>(city);
                _uow.GetRepository<State>().Update(oState);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "State Deleted Seccessfuly" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteCity(int cityId)
        {
            try
            {
                City oCity = _uow.GetRepository<City>().Single(x => x.Id == cityId);
                oCity.IsDeleted = true;
                oCity.DeletedOn = DateTime.Now;

                //var mapper = _mapper.Map<City>(city);
                _uow.GetRepository<City>().Update(oCity);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "City Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto UpdateCountry(CountryDto countryDto)
        {
            try
            {
                var mapper = _mapper.Map<Country>(countryDto);
                _uow.GetRepository<Country>().Update(mapper);
                _uow.SaveChanges();
                countryDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Country Updated Seccessfuly", Data = countryDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto UpdateState(StateDto stateDto)
        {
            try
            {
                var mapper = _mapper.Map<State>(stateDto);
                _uow.GetRepository<State>().Update(mapper);
                _uow.SaveChanges();
                stateDto.Id=mapper.Id;  
                return new ResponseDto { Status = 1, Message = " State Updated Seccessfuly", Data = stateDto };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto UpdateCity(CityDto cityDto)
        {
            try
            {
                var mapper = _mapper.Map<City>(cityDto);
                _uow.GetRepository<City>().Update(mapper);
                _uow.SaveChanges();
                cityDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "City Updated Seccessfuly", Data = cityDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
