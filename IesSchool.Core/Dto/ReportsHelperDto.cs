using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{

    public class ReportsHelper
    {
        public IPaginate<Area> AllAreas { get; set; }
        public IPaginate<Strand> AllStrands { get; set; }
    }
    public class ReportsHelperDto
    {
        public PaginateDto<AreaDto> AllAreas { get; set; }
        public PaginateDto<StrandDto> AllStrands { get; set; }
    }
}

