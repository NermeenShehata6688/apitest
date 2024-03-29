﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ItpSearchDto
    {
       
        public int? Status { get; set; }
        public bool? IsPublished { get; set; }
        public int? Student_Id { get; set; }
        public int? Teacher_Id { get; set; }
        public int? AcadmicYear_Id { get; set; }
        public int? Term_Id { get; set; }
        public int? Therapist_Id { get; set; }
        public int? ParamedicalService_Id { get; set; }
        public int? Index { get; set; }
        public int? PageSize { get; set; }
    }
}
