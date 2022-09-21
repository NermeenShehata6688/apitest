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
        public ResponseDto GetTherapistAssignedStudents(int therapistId);
        public ResponseDto GetTherapistParamedicalServices(int therapistId);
        public ResponseDto GetUsersHelper();
        public ResponseDto GetUserById(int userId);
        public  Task<ResponseDto> AddUser(IFormFile file, UserDto userDto);
        public Task<ResponseDto> AddUser2( UserDto userDto);
        public ResponseDto EditUser(IFormFile file, UserDto userDto);
        public ResponseDto EditUser2(UserDto userDto);
        public ResponseDto DeleteUser(int userId);

        public ResponseDto GetUserAttachmentByUserId(int userId);
        public ResponseDto AddUserAttachment(IFormFile file, UserAttachmentDto userAttachmentDto);
        public ResponseDto EditUserAttachment(UserAttachmentDto userAttachmentDto);
        public ResponseDto DeleteUserAttachment(int userAttachmentId);
        public bool IsUserNameExist(string UserName, int? userId);
        public bool IsUserCodeExist(string UserCode, int? userId);
        public ResponseDto IsSuspended(int userId, bool isSuspended);
        public ResponseDto IsActive(IsActiveDto isActiveDto);
        public ResponseDto GetUserForProfileById(int userId);
        public ResponseDto GetAllParents(UserSearchDto userSearchDto);
        public UserDto GetJustUserById(int userId);
        public bool IsParentUserNameExist(string UserName, int? userId);
        public bool IsParentCivilIdExist(string CivilId, int? userId);
        public bool IsParentEmailExist(string Email, int? userId);

        //public  Task<UserDto> ResetPasswordAsync(PasswordResetDto passwordResetDto);
    }
}
