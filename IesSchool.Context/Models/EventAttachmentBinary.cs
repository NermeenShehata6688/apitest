using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class EventAttachmentBinary
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }

        public virtual EventAttachement IdNavigation { get; set; } = null!;
    }
}
