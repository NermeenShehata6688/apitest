using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CountryId { get; set; }
        public short? DisplayOrder { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public string? Code { get; set; }

        public virtual Country? Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
