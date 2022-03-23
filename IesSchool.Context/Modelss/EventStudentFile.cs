using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class EventStudentFile
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? EventStudentId { get; set; }
        public bool? IsPublished { get; set; }
        public string? FileName { get; set; }

        public virtual EventStudent? EventStudent { get; set; }
        public virtual EventStudentFileBinary EventStudentFileBinary { get; set; } = null!;
    }
}
