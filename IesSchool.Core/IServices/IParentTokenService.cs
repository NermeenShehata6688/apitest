using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IParentTokenService
    {
        public ResponseDto GetParentToken();
        public ResponseDto GetParentTokenByParentId(int parentId);
    }
}
