using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class EventAttachementDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? EventId { get; set; }
        public bool? IsPublished { get; set; }
        public string? FileName { get; set; }
        public IFormFile? file { get; set; }
    }
}
