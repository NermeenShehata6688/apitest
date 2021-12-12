using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class StateDto
    {
        public StateDto()
        {
            Cities = new HashSet<CityDto>();
            //Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CountryId { get; set; }
        public bool? IsDeleted { get; set; }
        public short? DisplayOrder { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<CityDto> Cities { get; set; }
    }
}
