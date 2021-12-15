using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class UserAssistant
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AssistantId { get; set; }

        public virtual Assistant? Assistant { get; set; }
        public virtual User? User { get; set; }
    }
}
