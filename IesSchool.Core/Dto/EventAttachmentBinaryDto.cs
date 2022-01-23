using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class EventAttachmentBinaryDto
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }
    }
}
