using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class CityDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? StateId { get; set; }
        public short? DisplayOrder { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public string? Code { get; set; }

       public virtual StateDto? State { get; set; }
    }
}
