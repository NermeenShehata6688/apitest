using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IIxpService
    {
        public ResponseDto GetIxpsHelper();
        public ResponseDto GetIxps(IxpSearchDto ixpSearchDto);
        public ResponseDto GetIxpById(int ixpId);
        public ResponseDto AddIxp(IxpDto ixpDto);
        public ResponseDto EditIxp(IxpDto ixpDto);
        public ResponseDto DeleteIxp(int ixpId);
        public ResponseDto IxpStatus(StatusDto statusDto);
        public ResponseDto IxpIsPublished(int ixpId, bool isPublished);
        public ResponseDto IxpDuplicate(int ixpId);

        // public ResponseDto GetIxpExtraCurriculars();
        public ResponseDto GetIxpExtraCurricularByIxpId(int ixpId);
        public ResponseDto GetIxpExtraCurricularById(int ixpExtraCurricularId);
        public ResponseDto AddIxpExtraCurricular(IxpExtraCurricularDto ixpExtraCurricularDto);
        public ResponseDto EditIxpExtraCurricular(IxpExtraCurricularDto ixpExtraCurricularDto);
        public ResponseDto DeleteIxpExtraCurricular(int ixpExtraCurricularId);
    }
}
