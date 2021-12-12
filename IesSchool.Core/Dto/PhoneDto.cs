using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class PhoneDto
    {
        public int Id { get; set; }
        public string? Phone1 { get; set; }
        public string? Owner { get; set; }
        public int? StudentId { get; set; }
    }
}
