﻿using System;
using System.Collections.Generic;

namespace IesSchool.Context.Modelss
{
    public partial class Religion
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
