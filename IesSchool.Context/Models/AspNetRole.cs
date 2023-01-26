﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IesSchool.Context.Models
{
    public partial class AspNetRole
        //: IdentityRole<int>
    {
        public AspNetRole()
        {
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
        [JsonIgnore]

        public virtual ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        [JsonIgnore]

        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
    }
}
