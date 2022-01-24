using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IItpService
    {
        public ResponseDto GetItps();
        public ResponseDto GetItpById(int itpId);
        public ResponseDto AddItp(ItpDto itpDto);
        public ResponseDto EditItp(ItpDto itpDto);
        public ResponseDto DeleteItp(List<ItpDto> itpDto);
        public ResponseDto ItpStatus(int itpId, int status);
        public ResponseDto ItpIsPublished(int itpId, bool isPublished);
    }
}
