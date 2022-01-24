using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class EventAttachmentBinary
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }

        public virtual EventAttachement IdNavigation { get; set; } = null!;
    }
}
