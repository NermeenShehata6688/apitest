using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class EventAttachement
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? EventId { get; set; }
        public bool? IsPublished { get; set; }
        public string? FileName { get; set; }

        public virtual Event? Event { get; set; }
        public virtual EventAttachmentBinary EventAttachmentBinary { get; set; } = null!;
    }
}
