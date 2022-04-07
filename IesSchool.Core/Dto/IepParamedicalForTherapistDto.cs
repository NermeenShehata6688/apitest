using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    internal class IepParamedicalForTherapistDto
    {
        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ParamedicalServiceId { get; set; }
        public int? TherapistId { get; set; }
        public bool? IsItpCreated { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }

        public string? ParamedicalServiceName { get; set; }
        public string? IepYear { get; set; }
        public string? IepStudent { get; set; }
        public string? IepTerm { get; set; }
    }
}
