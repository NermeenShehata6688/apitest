﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class AspNetUserRole  
        //:IdentityUserRole<int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string? Code { get; set; }

        public virtual AspNetRole Role { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
