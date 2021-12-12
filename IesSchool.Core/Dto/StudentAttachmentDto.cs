using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class StudentAttachmentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FileName { get; set; }
        public DateTime? IssuedIn { get; set; }
        public DateTime? ValidTill { get; set; }
        public int? AttachmentTypeId { get; set; }
        public int? StudentId { get; set; }
       
       // public virtual StudentAttachmentBinaryDto IdNavigation { get; set; } = null!;

    }


}
