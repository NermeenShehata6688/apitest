using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class UserAttachmentBinary
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }

        public virtual UserAttachment IdNavigation { get; set; } = null!;
    }
}
