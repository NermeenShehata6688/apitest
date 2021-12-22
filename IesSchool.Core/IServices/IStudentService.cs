using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Http;

namespace IesSchool.Core.IServices
{
    public interface IStudentService
    {
        public ResponseDto GetStudentHelper();
        public ResponseDto GetStudents(StudentSearchDto studentSearchHelperDto);
        public ResponseDto GetStudentById(int studentId);
        public ResponseDto AddStudent(IFormFile file, StudentDto studentDto);
        public ResponseDto EditStudent(IFormFile file, StudentDto studentDto);
        public ResponseDto DeleteStudent(int studentId); 
        public ResponseDto GetStudentIeps(int studentId);

        public ResponseDto GetStudentTherapistsById(int studentTherapistId);
        public ResponseDto AddStudentTherapist(StudentTherapistDto studentTherapistDto);
        public ResponseDto EditStudentTherapist(StudentTherapistDto studentTherapistDto);
        public ResponseDto DeleteStudentTherapist(int studentTherapistId);


        public ResponseDto GetStudentHistoricalSkillsById(int studentHistoricalSkillId);
        public ResponseDto AddStudentHistoricalSkill(List<StudentHistoricalSkillDto> studentHistoricalSkillDto);
        public ResponseDto EditStudentHistoricalSkill(StudentHistoricalSkillDto studentHistoricalSkillDto);
        public ResponseDto DeleteStudentHistoricalSkill(int studentHistoricalSkillId);

        public ResponseDto GetStudentAttachmentsById(int studentAttachmentId);
        public ResponseDto AddStudentAttachment(IFormFile file, StudentAttachmentDto studentAttachmentDto);
        public ResponseDto EditStudentAttachment(StudentAttachmentDto studentAttachmentDto);
        public ResponseDto DeleteStudentAttachment(int studentAttachmentId);

        public ResponseDto GetStudentPhonesById(int phoneId);
        public ResponseDto AddStudentPhone(PhoneDto phoneDto);
        public ResponseDto EditStudentPhone(PhoneDto phoneDto);
        public ResponseDto DeleteStudentPhone(int phoneId);
        public ResponseDto IsSuspended(int studentId, bool isSuspended);

    }
}
