using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
    }
}
