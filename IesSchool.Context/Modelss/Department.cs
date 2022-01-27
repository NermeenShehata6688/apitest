using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Department
    {
        public Department()
        {
            Itps = new HashSet<Itp>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<Itp> Itps { get; set; }
    }
}
