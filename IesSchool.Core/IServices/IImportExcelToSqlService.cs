using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IImportExcelToSqlService
    {
        public ResponseDto ImportStrandsExcel(IFormFile file);
    }
}
