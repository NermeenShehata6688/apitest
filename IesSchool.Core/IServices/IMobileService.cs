using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IMobileService
    {
        public bool IsParentExist(string UserName, string Password);
        public ResponseDto Login(string UserName, string Password);
        public ResponseDto GetParentById(int parentId);
        public ResponseDto GetEvents(GetMobileEventsDto getMobileEventsDto);
        public ResponseDto GetEventById(int eventId, int? parentId);
        public ResponseDto GetParentStudents(int parentId);
        public ResponseDto GetStudentsEventsByParentId(int parentId);
        public ResponseDto GetStudentIeps(int studentId);
        public ResponseDto GetStudentItps(int studentId);
        public ResponseDto GetStudentIxps(int studentId);
        public ResponseDto GetStudentIepsItpsIxps(int studentId);
        public ResponseDto ChangePassword(int id, string oldPassword, string newPassword);
        public FileStreamResult IepLpReportPDF(int iepId);
        public string IepLpReportPdfPreview(int iepId);
        public FileStreamResult IepReportPDF(int iepId);
        public string IepReportPdfPreview(int iepId);
        public FileStreamResult IepProgressReportPDF(int iepProgressReportId);
        public string IepProgressReportPdfPreview(int iepProgressReportId);
        public FileStreamResult ItpReportPDF(int itpId);
        public string ItpReportPdfPreview(int itpId);
        public FileStreamResult ItpProgressReportPDF(int itpProgressReportId);
        public string ItpProgressReportPdfPreview(int itpProgressReportId);
        public FileStreamResult IxpReportPDF(int ixpId);
        public string IxpReportPdfPreview(int ixpId);

    }
}
