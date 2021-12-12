using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ApplicationGroup
    {
        public ApplicationGroup()
        {
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
            ApplicationUserGroups = new HashSet<ApplicationUserGroup>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUserGroups { get; set; }
    }
}
