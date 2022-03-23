using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Area
    {
        public Area()
        {
            Goals = new HashSet<Goal>();
            Strands = new HashSet<Strand>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public int? Code { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Strand> Strands { get; set; }
    }
}
