using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class StudentAttachmentBinary
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }
        public int? Code { get; set; }

        public virtual StudentAttachment IdNavigation { get; set; } = null!;
    }
}
