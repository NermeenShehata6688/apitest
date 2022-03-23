using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class IepAssistant
    {
        public int Id { get; set; }
        public int? AssistantId { get; set; }
        public int? Iepid { get; set; }

        public virtual Assistant? Assistant { get; set; }
        public virtual Iep? Iep { get; set; }
    }
}
