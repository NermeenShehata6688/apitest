using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class IepAssistantDto
    {
        public int Id { get; set; }
        public int? AssistantId { get; set; }
        public int? Iepid { get; set; }
        public string? AssistantName { get; set; }
    }
}
