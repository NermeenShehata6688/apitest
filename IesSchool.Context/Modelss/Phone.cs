using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Phone
    {
        public int Id { get; set; }
        public string? Phone1 { get; set; }
        public string? Owner { get; set; }
        public int? StudentId { get; set; }

        public virtual Student? Student { get; set; }
    }
}
