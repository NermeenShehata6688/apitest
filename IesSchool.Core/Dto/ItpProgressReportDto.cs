﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ItpProgressReportDto
    {
        public int Id { get; set; }
        public int? ItpId { get; set; }
        public int? StudentId { get; set; }
        public int? AcadmicYearId { get; set; }
        public int? TermId { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public DateTime? Date { get; set; }
        public string? GeneralComment { get; set; }
        public string? OtherComment { get; set; }
        public int? TherapistId { get; set; }
        public int? TeacherId { get; set; }
        public int? HeadOfEducationId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public string? StudentCode { get; set; }
        public string? StudentName { get; set; }
        public string? AcadmicYearName { get; set; }
        public string? TermName { get; set; }
        public string? TherapistName { get; set; }
        public string? ParamedicalServiceName { get; set; }
       // public string? HeadOfEducationName { get; set; }

        //public string? StudentBirthDay { get; set; }
        //public string? StudentDepartmentName { get; set; }

        public virtual ICollection<ItpObjectiveProgressReportDto>? ItpObjectiveProgressReports { get; set; }
    }
}
