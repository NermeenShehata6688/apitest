using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Iep
    {
        public Iep()
        {
            Goals = new HashSet<Goal>();
            IepAssistants = new HashSet<IepAssistant>();
            IepExtraCurriculars = new HashSet<IepExtraCurricular>();
            IepParamedicalServices = new HashSet<IepParamedicalService>();
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AcadmicYearId { get; set; }
        public int TermId { get; set; }
        public DateTime? DateOfPreparation { get; set; }
        public int? TeacherId { get; set; }
        public DateTime? LastDateOfReview { get; set; }
        public int? HeadOfDepartment { get; set; }
        public int HeadOfEducation { get; set; }
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

        public virtual AcadmicYear AcadmicYear { get; set; } = null!;
        public virtual User? HeadOfDepartmentNavigation { get; set; }
        public virtual User HeadOfEducationNavigation { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
        public virtual User? Teacher { get; set; }
        public virtual Term Term { get; set; } = null!;
        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<IepAssistant> IepAssistants { get; set; }
        public virtual ICollection<IepExtraCurricular> IepExtraCurriculars { get; set; }
        public virtual ICollection<IepParamedicalService> IepParamedicalServices { get; set; }
    }
}
