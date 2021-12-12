using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IesSchool.Core.Dto
{
    public class AssistantHelper
    {
        public  IPaginate<Department> AllDepartments { get; set; }
        public IPaginate<Country> AllNationalities { get; set; }
        public IPaginate<VwUser> AllTeachers { get; set; }
    }
    public class AssistantHelperDto
    {
        public PaginateDto<DepartmentDto> AllDepartments { get; set; }
        public PaginateDto<CountryDto> AllNationalities { get; set; }
        public PaginateDto<VwUserDto> AllTeachers { get; set; }
    }
}
