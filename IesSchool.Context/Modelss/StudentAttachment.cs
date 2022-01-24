using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class StudentAttachment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FileName { get; set; }
        public DateTime? IssuedIn { get; set; }
        public DateTime? ValidTill { get; set; }
        public int? AttachmentTypeId { get; set; }
        public int? StudentId { get; set; }

        public virtual AttachmentType? AttachmentType { get; set; }
        public virtual Student? Student { get; set; }
        public virtual StudentAttachmentBinary StudentAttachmentBinary { get; set; } = null!;
    }
}
