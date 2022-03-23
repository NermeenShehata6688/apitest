using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}
