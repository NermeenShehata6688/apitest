using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class EventStudentFileBinary
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }

        public virtual EventStudentFile IdNavigation { get; set; } = null!;
    }
}
