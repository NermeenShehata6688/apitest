using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class UserAttachmentDto
    {
        public int Id { get; set; }
        //public string? Name { get; set; }
        //public string? FileName { get; set; }
        public DateTime? IssuedIn { get; set; }
        public DateTime? ValidTill { get; set; }
        public int? AttachmentTypeId { get; set; }
        public int? UserId { get; set; }
        //public IFormFile File { get; set; }
    }
}
