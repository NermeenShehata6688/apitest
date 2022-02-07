using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IepsItpsIxpsDto
    {
        public virtual ICollection<GetIepDto>? Ieps { get; set; }
        public virtual ICollection<ItpDto>? Itps { get; set; }
        public virtual ICollection<IxpDto>? Ixps { get; set; }

    }
}
