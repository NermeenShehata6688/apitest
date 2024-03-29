﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ProgressReportExtraCurricularDto
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? ExtraCurricularId { get; set; }
        public string? Comment { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public int? IepextraCurricularId { get; set; }

        public string? ExtraCurricularName { get; set; }
        public string? ExtraCurricularNameAr { get; set; }
    }
}
