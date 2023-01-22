using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class AspNetUser : IdentityUser<int>
    {
        public AspNetUser()
        {
            ApplicationUserGroups = new HashSet<ApplicationUserGroup>();
            //AspNetUserClaims = new HashSet<AspNetUserClaim>();
            //AspNetUserLogins = new HashSet<AspNetUserLogin>();
        }

     
        //public string? DeviceToken { get; set; }
    
        public virtual User IdNavigation { get; set; } = null!;
        public virtual ICollection<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        //public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
    }
}
