using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class Itp
    {
        public Itp()
        {
            ItpGoalObjectives = new HashSet<ItpGoalObjective>();
            ItpGoals = new HashSet<ItpGoal>();
            ItpProgressReports = new HashSet<ItpProgressReport>();
            ItpStrategies = new HashSet<ItpStrategy>();
        }

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

        public virtual AcadmicYear? AcadmicYear { get; set; }
        public virtual User? HeadOfDepartment { get; set; }
        public virtual User? HeadOfEducation { get; set; }
        public virtual IepParamedicalService IdNavigation { get; set; } = null!;
        public virtual ParamedicalService? ParamedicalService { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Term? Term { get; set; }
        public virtual User? Therapist { get; set; }
        public virtual Department? TherapistDepartment { get; set; }
        public virtual ICollection<ItpGoalObjective> ItpGoalObjectives { get; set; }
        public virtual ICollection<ItpGoal> ItpGoals { get; set; }
        public virtual ICollection<ItpProgressReport> ItpProgressReports { get; set; }
        public virtual ICollection<ItpStrategy> ItpStrategies { get; set; }
    }
}
