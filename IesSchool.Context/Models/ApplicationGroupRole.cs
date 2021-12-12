using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ApplicationGroupRole
    {
        public string ApplicationGroupId { get; set; } = null!;
        public string ApplicationRoleId { get; set; } = null!;
        public int? Code { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; } = null!;
        public virtual AspNetRole ApplicationRole { get; set; } = null!;
    }
}
