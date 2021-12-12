using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class ApplicationUserGroup
    {
        public int ApplicationUserId { get; set; }
        public string ApplicationGroupId { get; set; } = null!;
        public int? Code { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; } = null!;
        public virtual AspNetUser ApplicationUser { get; set; } = null!;
    }
}
