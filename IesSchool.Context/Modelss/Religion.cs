using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Religion
    {
        public Religion()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
