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
        #region Dapper
        public  Task<ResponseDto> GetIxpsHelperDapper();

        #endregion
        public ResponseDto GetIxpsHelper();
        public ResponseDto GetIxpslate(IxpSearchDto ixpSearchDto);
        public Task<ResponseDto> GetIxps(IxpSearchDto ixpSearchDto);
        public Task<ResponseDto> GetIxpById(int ixpId);
        public Task<ResponseDto> AddIxp(IxpDto ixpDto);
        public  Task<ResponseDto> EditIxp(IxpDto ixpDto);
        public  Task<ResponseDto> UpdateIxpComent(IxpDto ixpDto);
        public ResponseDto DeleteIxp(int ixpId);
        public ResponseDto IxpStatus(StatusDto statusDto);
        public ResponseDto IxpIsPublished(IsPuplishedDto isPuplishedDto);
        public ResponseDto IxpDuplicate(int ixpId);

        // public ResponseDto GetIxpExtraCurriculars();
        public ResponseDto GetIxpExtraCurricularByIxpId(int ixpId);
        public ResponseDto GetIxpExtraCurricularById(int ixpExtraCurricularId);
        public ResponseDto AddIxpExtraCurricular(IxpExtraCurricularDto ixpExtraCurricularDto);
        public ResponseDto EditIxpExtraCurricular(IxpExtraCurricularDto ixpExtraCurricularDto);
        public ResponseDto DeleteIxpExtraCurricular(int ixpExtraCurricularId);

        public ResponseDto GetIepsForExTeacher(int teacherId);
        public ResponseDto CreateIxp(int iepExtraCurricularId);
    }
}
