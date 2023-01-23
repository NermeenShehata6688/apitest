using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ItpDto
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public int? TherapistId { get; set; }
        public int? TherapistDepartmentId { get; set; }
        public int? AcadmicYearId { get; set; }
        public int? TermId { get; set; }
        public DateTime? DateOfPreparation { get; set; }
        public DateTime? LastDateOfReview { get; set; }
        public int? HeadOfDepartmentId { get; set; }
        public int? HeadOfEducationId { get; set; }
        public bool? ParentsInvolvedInSettingUpSuggestions { get; set; }
        public bool? ReportCard { get; set; }
        public bool? ProgressReport { get; set; }
        public bool? ParentsMeeting { get; set; }
        public bool? Others { get; set; }
        public int? Status { get; set; }
        public string? FooterNotes { get; set; }
        public int? RoomNumber { get; set; }
        public string? CurrentLevel { get; set; }
        public string? StudentNotes { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
      //  public int? IepparamedicalServiceId { get; set; }

        public string? StudentName { get; set; }
        public string? ParamediaclName { get; set; }
        public string? TherapistName { get; set; }
        public string? AcadmicYearName { get; set; }
        public string? TermName { get; set; }
        public string? StudentCodeName { get; set; }
        public string? StudentBirthdayName { get; set; }
        public string? DepartmentName { get; set; }
        public string? TeacherName { get; set; }
        public string? HeadOfEducationName { get; set; }

        public virtual StudentDto? Student { get; set; }
        public virtual ICollection<ItpGoalDto>? ItpGoals { get; set; }
        public virtual ICollection<ItpProgressReportDto>? ItpProgressReports { get; set; }

    }
}
