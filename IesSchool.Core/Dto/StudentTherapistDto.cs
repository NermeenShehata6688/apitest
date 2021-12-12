
namespace IesSchool.Core.Dto
{
    public class StudentTherapistDto
    {
        public int Id { get; set; }
        public int? TherapistId { get; set; }
        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentNameAr { get; set; }
        public int? DepartmentId { get; set; }
        public int? StudentCode { get; set; }

    }
}
