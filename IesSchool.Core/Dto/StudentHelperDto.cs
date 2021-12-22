using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    public class StudentHelper
    {
        public IPaginate<Department> AllDepartments { get; set; }
        public IPaginate<VwUser> AllTeachers { get; set; }
        public IPaginate<VwUser> AllTherapists { get; set; }
        public IPaginate<Country> AllNationalities { get; set; }
        public IPaginate<State> AllStates { get; set; }
        public IPaginate<City> AllAreas { get; set; }
        public IPaginate<Skill> AllSkills { get; set; }
        public IPaginate<AttachmentType> AllAttachmentTypes { get; set; }
        public IPaginate<WorkCategory> AllWorkCategorys { get; set; }
        public IPaginate<Religion> AllReligions { get; set; }

    }

    public class StudentHelperDto
    {
        public PaginateDto<DepartmentDto> AllDepartments { get; set; }
        public PaginateDto<VwUserDto> AllTeachers { get; set; }
        public PaginateDto<VwUserDto> AllTherapists { get; set; }
        public PaginateDto<CountryDto> AllNationalities { get; set; }
        public PaginateDto<StateDto> AllStates { get; set; }
        public PaginateDto<CityDto> AllAreas { get; set; }
        public PaginateDto<SkillDto> AllSkills { get; set; }
        public PaginateDto<AttachmentTypeDto> AllAttachmentTypes { get; set; }
        public PaginateDto<WorkCategoryDto> AllWorkCategorys { get; set; }
        public PaginateDto<ReligionDto> AllReligions { get; set; }


    }
}
