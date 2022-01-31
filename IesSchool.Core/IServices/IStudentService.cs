using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Http;

namespace IesSchool.Core.IServices
{
    public interface IStudentService
    {
        public ResponseDto GetStudentHelper();
        public ResponseDto GetStudents(StudentSearchDto studentSearchHelperDto);
        public ResponseDto GetStudentById(int studentId);
        public ResponseDto AddStudent(StudentDto studentDto, IFormFile? file);
        public ResponseDto EditStudent(StudentDto studentDto, IFormFile? file);
        public ResponseDto DeleteStudent(int studentId); 
        public ResponseDto GetStudentIeps(int studentId);

        public ResponseDto GetStudentTherapistsById(int studentTherapistId);
        public ResponseDto AddStudentTherapist(StudentTherapistDto studentTherapistDto);
        public ResponseDto EditStudentTherapist(StudentTherapistDto studentTherapistDto);
        public ResponseDto DeleteStudentTherapist(int studentTherapistId);


        public ResponseDto GetHistoricalSkillsBystudentId(int studentId);
        public ResponseDto AddStudentHistoricalSkill(List<StudentHistoricalSkillDto> studentHistoricalSkillDto);
        public ResponseDto EditStudentHistoricalSkill(StudentHistoricalSkillDto studentHistoricalSkillDto);
        public ResponseDto DeleteStudentHistoricalSkill(int studentHistoricalSkillId);

        public ResponseDto GetAttachmentsByStudentId(int studentId);
        public ResponseDto AddStudentAttachment(IFormFile file, StudentAttachmentDto studentAttachmentDto);
        public ResponseDto EditStudentAttachment(StudentAttachmentDto studentAttachmentDto);
        public ResponseDto DeleteStudentAttachment(int studentAttachmentId);

        public ResponseDto GetPhonesByStudentId(int studentId);
        public ResponseDto AddStudentPhone(PhoneDto phoneDto);
        public ResponseDto EditStudentPhone(PhoneDto phoneDto);
        public ResponseDto DeleteStudentPhone(int phoneId);
        public ResponseDto IsSuspended(int studentId, bool isSuspended);
        public ResponseDto IsActive(int studentId, bool isActive);
        public bool IsStudentCodeExist(int StudentCode, int? StudentId);
        public ResponseDto GetStudentHistoricalSkills(int studentId);

    }
}
