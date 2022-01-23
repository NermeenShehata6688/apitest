using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    internal class EventAttachementAddDto
    {

        public int? EventId { get; set; }

        public virtual ICollection<IFormFile>? IFormFilels { get; set; }
        
    }
}
