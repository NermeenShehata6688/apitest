﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ProgressReportParamedicalDto
    {
        public int Id { get; set; }
        public int? ProgressReportId { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public int? IepParamedicalSerciveId { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public string? ParamedicalServiceName { get; set; }
        public string? ParamedicalServiceNameAr { get; set; }
        public string? StudentName { get; set; }
        public string? AcadmicYearName { get; set; }
        public string? TermName { get; set; }

    }
}
