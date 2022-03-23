using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class ObjectiveSkill
    {
        public int Id { get; set; }
        public int? ObjectiveId { get; set; }
        public int? SkillId { get; set; }

        public virtual Objective? Objective { get; set; }
        public virtual Skill? Skill { get; set; }
    }
}
