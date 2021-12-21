using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IesSchool.Context.Models
{
    public partial class ApplicationGroupRole
    {
        public int ApplicationGroupId { get; set; }
        public int ApplicationRoleId { get; set; }
        public int? Code { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; } = null;
        public virtual AspNetRole ApplicationRole { get; set; } = null;
    }
}
