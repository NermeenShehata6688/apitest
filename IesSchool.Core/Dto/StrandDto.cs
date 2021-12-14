using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class StrandDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public int? Code { get; set; }
        public int? DisplayOrder { get; set; }
        public int? AreaId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public int? SkillsCount { get; set; }
        public string? AreaName { get; set; }
        
    }
}
