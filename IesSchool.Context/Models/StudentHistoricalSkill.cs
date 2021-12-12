using System;
using System.Collections.Generic;

namespace IesSchool.Context.Models
{
    public partial class StudentHistoricalSkill
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? HistoricalSkilld { get; set; }

        public virtual Skill? HistoricalSkilldNavigation { get; set; }
        public virtual Student? Student { get; set; }
    }
}
