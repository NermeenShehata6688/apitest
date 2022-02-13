using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class MembershipDto
    {

        public partial class ApplicationGroupDto
        {
            public ApplicationGroupDto()
            {
                ApplicationGroupRoles = new HashSet<ApplicationGroupRoleDto>();
                ApplicationUserGroups = new HashSet<ApplicationUserGroupDto>();
            }

            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }

            public virtual ICollection<ApplicationGroupRoleDto> ApplicationGroupRoles { get; set; } = null;
            public virtual ICollection<ApplicationUserGroupDto> ApplicationUserGroups { get; set; } = null;
        }



        public partial class ApplicationGroupRoleDto
        {
            public int ApplicationGroupId { get; set; }
            public int ApplicationRoleId { get; set; }
            public int? Code { get; set; }

            //public virtual ApplicationGroupDto ApplicationGroup { get; set; } = null;
           public virtual AspNetRoleDto? ApplicationRole { get; set; } = null;
        }






        public partial class AspNetRoleDto
        {
            public AspNetRoleDto()
            {
                //ApplicationGroupRoles = new HashSet<AspNetRoleDto>();
                //AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            }

            public int Id { get; set; }
            public string? Name { get; set; }
            public string? NormalizedName { get; set; }
            public string? ConcurrencyStamp { get; set; }

            //public virtual ICollection<ApplicationGroupRoles> ApplicationGroupRoles { get; set; }
            //public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        }


        public partial class ApplicationUserGroupDto
        {
            public int ApplicationUserId { get; set; }
            public int ApplicationGroupId { get; set; }
            public int? Code { get; set; }

            public virtual ApplicationGroupDto ApplicationGroup { get; set; } = null!;
            //public virtual AspNetUser ApplicationUser { get; set; } = null!;
        }
    }
}
