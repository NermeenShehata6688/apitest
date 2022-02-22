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
        public ResponseDto GetStudentIepsItpsIxps(int studentId);
        public ResponseDto ChangePassword(int id, string oldPassword, string newPassword);

    }
}
