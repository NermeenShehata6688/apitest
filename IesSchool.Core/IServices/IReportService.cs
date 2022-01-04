using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IReportService
    {
        public ResponseDto GetReporstHelper();
        public FileStreamResult IepLpReport(int iepId); 
        public FileStreamResult IepReport(int iepId); 
        public FileStreamResult BCPReport(int? studentId, int? iepId); 
    }
}
