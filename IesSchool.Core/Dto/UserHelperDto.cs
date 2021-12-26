using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    internal class UserHelper
    {
        public IPaginate<Department> AllDepartments { get; set; }
        public IPaginate<Country> AllNationalities { get; set; }
        public IPaginate<Assistant> AllAssistants { get; set; }
        public IPaginate<Student> AllStudents { get; set; }
        public IPaginate<ParamedicalService> AllParamedicalServices { get; set; }
        public IPaginate<AttachmentType> AllAttachmentTypes { get; set; }
    }
    public class UserHelperDto
    {
        public PaginateDto<DepartmentDto> AllDepartments { get; set; }
        public PaginateDto<CountryDto> AllNationalities { get; set; }
        public PaginateDto<AssistantDto> AllAssistants { get; set; }
        public PaginateDto<StudentDto> AllStudents { get; set; }
        public PaginateDto<ParamedicalServiceDto> AllParamedicalServices { get; set; }
        public PaginateDto<AttachmentTypeDto> AllAttachmentTypes { get; set; }

    }
}
