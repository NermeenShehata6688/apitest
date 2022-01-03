using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class VwSkill
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public int? Code { get; set; }
        public int? DisplayOrder { get; set; }
        public int? Level { get; set; }
        public int? SkillNumber { get; set; }
        public int? StrandId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public string? StrandName { get; set; }
        public string? AreaName { get; set; }
        public string? AreaNameAr { get; set; }
        public string? StrandNameAr { get; set; }
    }
}
