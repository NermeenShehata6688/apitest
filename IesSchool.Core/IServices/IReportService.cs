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
        public string IepReportHTML(int iepId);
        public string ProgressReportHTML(int iepProgressReportId);
        public string IepLpReportHTML(int iepId);
        public string BCPReportHTML(int? studentId, int? iepId);

        public FileStreamResult ItpReport(int itpId);
        public FileStreamResult ItpProgressReport(int itpProgressReportId);
        public string ItpReportHTML(int itpId);
        public string ItpProgressReportHTML(int itpProgressReportId);
        public FileStreamResult IxpReport(int ixpId);
        public string IxpReportHTML(int ixpId);

        public ResponseDto IepLpReportPdfPreview(int iepId);
        public ResponseDto IepReportPdfPreview(int iepId);
        public ResponseDto IepProgressReportPdfPreview(int iepProgressReportId);
        public ResponseDto ItpReportPdfPreview(int itpId);
        public ResponseDto ItpProgressReportPdfPreview(int itpProgressReportId);
        public ResponseDto IxpReportPdfPreview(int ixpId);
        public FileStreamResult StudentReport(StudentSearchDto studentSearchDto);

        public FileStreamResult TeacherStudentsReport(int teacherId);
    }
}
