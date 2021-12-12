using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface ILocationService
    {
        public ResponseDto AllCountries();
        public ResponseDto AllStates();
        public ResponseDto AllCities();
        public ResponseDto GetCitiesByStateId(int stateId);
        public ResponseDto AllStatesWithCities();
        public ResponseDto AddCountry(CountryDto countryDto);
        public ResponseDto AddState(StateDto stateDto);
        public ResponseDto AddCity(CityDto cityDto);
        public ResponseDto DeleteCountry(int countryId);
        public ResponseDto DeleteState(int stateId);
        public ResponseDto DeleteCity(int cityId);
        public ResponseDto UpdateCountry(CountryDto countryDto);
        public ResponseDto UpdateState(StateDto stateDto);
        public ResponseDto UpdateCity(CityDto cityDto);

    }
}
