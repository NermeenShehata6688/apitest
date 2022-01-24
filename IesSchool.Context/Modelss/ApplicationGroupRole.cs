using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ApplicationGroupRole
    {
        public int ApplicationGroupId { get; set; }
        public int ApplicationRoleId { get; set; }
        public int? Code { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; } = null!;
        public virtual AspNetRole ApplicationRole { get; set; } = null!;
    }
}
