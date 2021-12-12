using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string? Deatils { get; set; }
        public DateTime? Date { get; set; }
        public string? CreatedBy { get; set; }
        public int? ObjectiveId { get; set; }
        public int? Evaluation { get; set; }
        public string? ObjectiveNote { get; set; }
        //public int? Index { get; set; }
        //public int? PageSize { get; set; }
    }
}
