using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IExtraCurricularService
    {
        public ResponseDto GetExtraCurriculars();
        public ResponseDto GetExtraCurricularById(int extraCurricularId);
        public ResponseDto AddExtraCurricular(ExtraCurricularDto extraCurricularDto);
        public ResponseDto EditExtraCurricular(ExtraCurricularDto extraCurricularDto);
        public ResponseDto DeleteExtraCurricular(int extraCurricularId);
    }
}
