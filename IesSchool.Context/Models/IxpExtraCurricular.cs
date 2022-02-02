﻿using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class IxpExtraCurricular
    {
        public int Id { get; set; }
        public int? IxpId { get; set; }
        public int? ExtraCurricularId { get; set; }
        public int? TeacherId { get; set; }
        public string? Goal { get; set; }
        public string? Strategy { get; set; }
        public int? Indication { get; set; }
        public DateTime? Date { get; set; }

        public virtual ExtraCurricular? ExtraCurricular { get; set; }
        public virtual Ixp? Ixp { get; set; }
        public virtual User? Teacher { get; set; }
    }
}