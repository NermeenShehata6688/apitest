using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class UserAttachmentBinaryDto
    {
        public int Id { get; set; }
        public byte[]? FileBinary { get; set; }

    }
}
