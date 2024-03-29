﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Context.Models
{
    public partial class Program
    {
        public Program()
        {
            Areas = new HashSet<Area>();
            Goals = new HashSet<Goal>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
    }
}
