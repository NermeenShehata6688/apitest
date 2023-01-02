using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class CalenderDto
    {
        public int? Id { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public string? Details { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? Color { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? End { get; set; }
        public DateTime? Start { get; set; }
        public int? CalenderCategoryId { get; set; }
        public virtual CalenderCategoryDto? CalenderCategory { get; set; }
    }
}
