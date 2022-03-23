using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class UserAttachment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FileName { get; set; }
        public DateTime? IssuedIn { get; set; }
        public DateTime? ValidTill { get; set; }
        public int? AttachmentTypeId { get; set; }
        public int? UserId { get; set; }

        public virtual AttachmentType? AttachmentType { get; set; }
        public virtual User? User { get; set; }
        public virtual UserAttachmentBinary UserAttachmentBinary { get; set; } = null!;
    }
}
