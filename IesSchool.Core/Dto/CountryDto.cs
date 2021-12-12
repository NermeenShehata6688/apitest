using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class CountryDto
    {
        public CountryDto()
        {
            //StateDtos = new HashSet<StateDto>();
        }
        public int Id { get; set; }
        public int StateNum { get; set; }
        public string? Name { get; set; }
        public bool? IsDeleted { get; set; }
        public short? DisplayOrder { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}
