using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class AspNetUserRole
    {
        public int UserId { get; set; }
        public string RoleId { get; set; } = null!;

        public virtual AspNetRole Role { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
