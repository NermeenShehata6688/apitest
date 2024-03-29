﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class EventMobileDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public DateTime? Date { get; set; }
        public int? EventTypeId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsPublished { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? DepartmentName { get; set; }
        public string? EventTypeName { get; set; }


        public virtual ICollection<EventAttachementDto>? EventAttachements { get; set; }
        public virtual ICollection<EventStudentDto>? EventStudents { get; set; }
    }
}
