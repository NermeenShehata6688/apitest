using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    internal class IepExtraTeacherDto
    {
        public int Id { get; set; }
        public int? Iepid { get; set; }
        public int? ExtraCurricularId { get; set; }
        public int? ExTeacherId { get; set; }
        public bool? IsIxpCreated { get; set; }
        public string? ExtraCurricularName { get; set; }
        public string? ExTeacherName { get; set; }
        public string? IepYear { get; set; }
        public string? IepStudent { get; set; }
        public string? IepTerm { get; set; }
    }
}
