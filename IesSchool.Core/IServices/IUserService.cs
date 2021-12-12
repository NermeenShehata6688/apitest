using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IUserService
    {
        public ResponseDto GetUsers(UserSearchDto userSearchDto);
        public ResponseDto GetAllTeachers();
        public ResponseDto GetAllTherapists();
        public ResponseDto GetTeacherAssignedStudents(int teacherId); 
        public ResponseDto GeTherapistAssignedStudents(int therapistId);
        public ResponseDto GetUsersHelper();
        public ResponseDto GetUserById(int userId);
        public ResponseDto AddUser(UserDto userDto);
        public ResponseDto EditUser(UserDto userDto);
        public ResponseDto DeleteUser(int userId);

        public ResponseDto GetUserAttachmentById(int userAttachmentId);
        public ResponseDto AddUserAttachment(IFormFile file, UserAttachmentDto userAttachmentDto);
        public ResponseDto EditUserAttachment(UserAttachmentDto userAttachmentDto);
        public ResponseDto DeleteUserAttachment(int userAttachmentId);

        public ResponseDto IsSuspended(int userId, bool isSuspended);

    }
}
