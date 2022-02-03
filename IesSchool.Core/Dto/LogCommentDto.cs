using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class LogCommentDto
    {
        public int Id { get; set; }
        public int? IepId { get; set; }
        public int? StudentId { get; set; }
        public int? UserId { get; set; }
        public DateTime? LogDate { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? UserImage { get; set; }
        public string? UserImagePath { get; set; }
    }
}
