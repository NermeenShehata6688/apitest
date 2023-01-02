using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class CalenderCategoryDto
    {
        public int ?Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public int? Color { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<CalenderDto> Calenders { get; set; }
    }
}
