using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class StudentAttachmentBinary
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }
        public int? Code { get; set; }

        public virtual StudentAttachment IdNavigation { get; set; } = null!;
    }
}
