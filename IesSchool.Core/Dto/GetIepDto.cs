using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class GetIepDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AcadmicYearId { get; set; }
        public int TermId { get; set; }
        public DateTime? DateOfPreparation { get; set; }
        public int? TeacherId { get; set; }
        public DateTime? LastDateOfReview { get; set; }
        public int? HeadOfDepartment { get; set; }
        public int? HeadOfEducation { get; set; }
        public bool? ParentsInvolvedInSettingUpSuggestions { get; set; }
        public bool? ReportCard { get; set; }
        public bool? ProgressReport { get; set; }
        public bool? ParentsMeeting { get; set; }
        public bool? Others { get; set; }
        public int? Status { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? FooterNotes { get; set; }
        public int? RoomNumber { get; set; }
        public string? StudentNotes { get; set; }
        public bool? IsPublished { get; set; }

        //for mapping
        public string? StudentName { get; set; }
        public string? StudentCodeName { get; set; }
        public string? StudentBirthdayName { get; set; }
        public string? DepartmentName { get; set; }
        public string? AcadmicYearName { get; set; }
        public string? TermName { get; set; }
        public string? TeacherName { get; set; }
        public string? HeadOfEducationName { get; set; }
        public string? HeadOfDepartmentName { get; set; }


        public virtual ICollection<IepAssistantDto>? IepAssistants { get; set; }
        public virtual ICollection<IepExtraCurricularDto>? IepExtraCurriculars { get; set; }
        public virtual ICollection<IepParamedicalServiceDto>? IepParamedicalServices { get; set; }
        public virtual ICollection<GoalDto>? Goals { get; set; }
        public virtual ICollection<IepProgressReportDto>? IepProgressReports { get; set; }
    }
}
