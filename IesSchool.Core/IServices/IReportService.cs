using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.XlsIO;
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
        public FileStreamResult ProgressReport(int iepProgressReportId);
        public FileStreamResult IepReportHTML(int iepId);
    }
}
