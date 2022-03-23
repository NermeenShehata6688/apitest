using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Assistant
    {
        public Assistant()
        {
            IepAssistants = new HashSet<IepAssistant>();
            UserAssistants = new HashSet<UserAssistant>();
        }

        public int Id { get; set; }
        public int? NationalityId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public byte[]? ImageBinary { get; set; }
        public string? Image { get; set; }
        public bool? IsSuspended { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool? Gender { get; set; }
        public string? Mobile { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Country? Nationality { get; set; }
        public virtual ICollection<IepAssistant> IepAssistants { get; set; }
        public virtual ICollection<UserAssistant> UserAssistants { get; set; }
    }
}
