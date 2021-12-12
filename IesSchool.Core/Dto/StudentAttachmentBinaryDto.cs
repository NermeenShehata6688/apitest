using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class StudentAttachmentBinaryDto
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }
        public int? Code { get; set; }
    }
}
