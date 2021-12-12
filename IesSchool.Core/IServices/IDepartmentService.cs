using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IDepartmentService
    {
        public ResponseDto GetDepartments();
        public ResponseDto GetDepartmentById(int departmentId);
        public ResponseDto AddDepartment(DepartmentDto departmentDto);
        public ResponseDto EditDepartment(DepartmentDto departmentDto);
        public ResponseDto DeleteDepartment(int departmentId);
    }
}
