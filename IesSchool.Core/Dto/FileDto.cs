using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class FileDto
    {
        public string? Name { get; set; }
        public string? FileName { get; set; }
        public string? virtualPath { get; set; }
        public DateTime? UploadedOn { get; set; }
    }
}
