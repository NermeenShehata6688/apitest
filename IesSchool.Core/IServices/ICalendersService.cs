using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface ICalendersService
    {
        public ResponseDto GetCalenders();
        public ResponseDto GetCalenderById(int id);
        public ResponseDto AddEditCalender(CalenderDto acadmicYearDto);
        public ResponseDto DeleteCalender(int id);

        public ResponseDto GetCalendersCategories();
        public ResponseDto GetCalenderCategoryById(int id);
        public ResponseDto AddEditCalenderCategory(CalenderCategoryDto calenderCategoryDto);

        public ResponseDto DeleteCalenderCategory(int id);




    }
}
