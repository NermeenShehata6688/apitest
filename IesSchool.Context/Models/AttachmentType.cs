using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class AttachmentType
    {
        public AttachmentType()
        {
            StudentAttachments = new HashSet<StudentAttachment>();
            UserAttachments = new HashSet<UserAttachment>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }

        public virtual ICollection<StudentAttachment> StudentAttachments { get; set; }
        public virtual ICollection<UserAttachment> UserAttachments { get; set; }
    }
}
