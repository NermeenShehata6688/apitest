using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    internal class StatisticDto
    {
        public int? StusentsCount { get; set; }
        public int? SuspendendStusentsCount { get; set; }
        public int? ActivetusentsCount { get; set; }
        public int? UnActivetusentsCount { get; set; }

        public int? CurrentYearIepsCount { get; set; }
        public int? CurrentTermIepsCount { get; set; }
        public int? CurrentYearItpsCount { get; set; }
        public int? CurrentTermItpsCount { get; set; }
        public int? CurrentYearIxpsCount { get; set; }
        public int? CurrentTermIxpsCount { get; set; }

        public int? AreasCount { get; set; }
        public int? StrandsCount { get; set; }
        public int? SkillsCount { get; set; }

        public int? TeachersCount { get; set; }
        public int? TherapistsCount { get; set; }
        public int? ExtraTeachersCount { get; set; }
        public int? DepartmentsCount { get; set; }

    }
}
