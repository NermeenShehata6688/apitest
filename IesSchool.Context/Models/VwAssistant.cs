using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Context.Models
{
    public partial class VwAssistant
    {
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
        public string? Image { get; set; }
        public bool? IsSuspended { get; set; }
        public string? NationalityName { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool? Gender { get; set; }
    }
}
