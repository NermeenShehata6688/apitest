using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ApplicationUserGroup
    {
        public int ApplicationUserId { get; set; }
        public int ApplicationGroupId { get; set; }
        public int? Code { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; } = null!;
        public virtual AspNetUser ApplicationUser { get; set; } = null!;
    }
}
