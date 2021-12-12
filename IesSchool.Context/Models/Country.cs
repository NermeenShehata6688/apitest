using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class Country
    {
        public Country()
        {
            Assistants = new HashSet<Assistant>();
            States = new HashSet<State>();
            StudentBirthCountries = new HashSet<Student>();
            StudentFatherNationalities = new HashSet<Student>();
            StudentMotherNationalities = new HashSet<Student>();
            StudentNationalities = new HashSet<Student>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public short? DisplayOrder { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public byte[]? ImageBinary { get; set; }
        public string? Image { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<Assistant> Assistants { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<Student> StudentBirthCountries { get; set; }
        public virtual ICollection<Student> StudentFatherNationalities { get; set; }
        public virtual ICollection<Student> StudentMotherNationalities { get; set; }
        public virtual ICollection<Student> StudentNationalities { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
