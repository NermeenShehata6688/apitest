using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ApplicationGroup
    {
        public ApplicationGroup()
        {
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
            ApplicationUserGroups = new HashSet<ApplicationUserGroup>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUserGroups { get; set; }
    }
}
