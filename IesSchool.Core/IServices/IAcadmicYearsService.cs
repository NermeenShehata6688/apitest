using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IAcadmicYearsService
    {
        public ResponseDto GetAcadmicYears();
        public ResponseDto GetAcadmicYearById(int acadmicYearId);
        public ResponseDto AddAcadmicYear(AcadmicYearDto acadmicYearDto);
        public ResponseDto EditAcadmicYear(AcadmicYearDto acadmicYearDto);
        public ResponseDto DeleteAcadmicYear(int acadmicYearId);
    }
}
