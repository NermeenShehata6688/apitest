using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface ILogCommentService
    {
        public ResponseDto GetStudentLogComments(int studentId);
        public ResponseDto GetIEPLogComments(int iepId);
        public ResponseDto AddLogComment(LogCommentDto logCommentDto);
        public ResponseDto EditLogComment(LogCommentDto logCommentDto);
        public ResponseDto DeleteLogComment(int logCommentId);
    }
}
