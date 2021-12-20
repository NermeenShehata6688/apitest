using IesSchool.Context.Models;
using IesSchool.InfraStructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Dto
{
    internal class EventHelper
    {
        public IPaginate<Department> AllDepartments { get; set; }
        public IPaginate<User> AllTeachers { get; set; }
        public IPaginate<Student> AllStudents { get; set; }
        public IPaginate<EventType> AllEventTypes { get; set; }
        public IPaginate<Event> AllEvents { get; set; }

    }
    public class EventHelperDto
    {
        public PaginateDto<DepartmentDto> AllDepartments { get; set; }
        public PaginateDto<UserDto> AllTeachers { get; set; }
        public PaginateDto<StudentDto> AllStudents { get; set; }
        public PaginateDto<EventTypeDto> AllEventTypes { get; set; }
        public PaginateDto<EventDto> AllEvents { get; set; }
    }
}
