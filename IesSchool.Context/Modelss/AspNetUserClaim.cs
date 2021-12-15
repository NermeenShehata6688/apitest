using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class AspNetUserClaim
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
    }
}
