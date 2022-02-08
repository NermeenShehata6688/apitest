using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.EntityFrameworkCore;

namespace IesSchool.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetDepartments()
        {
            try
            {
                var allDepartments = _uow.GetRepository<Department>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<DepartmentDto>>(allDepartments);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetDepartmentById(int departmentId)
        {
            try
            {
                var department = _uow.GetRepository<Department>().Single(x => x.Id == departmentId && x.IsDeleted != true,null,x=> x.Include(x => x.SkillAlowedDepartments).Include(s => s.Students).Include(s => s.Users));
                var mapper = _mapper.Map<DepartmentDto>(department);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddDepartment(DepartmentDto departmentDto)
        {
            try
            {
                departmentDto.IsDeleted = false;
                departmentDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Department>(departmentDto);
                _uow.GetRepository<Department>().Add(mapper);
                _uow.SaveChanges();
                departmentDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Department Added  Seccessfuly", Data = departmentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditDepartment(DepartmentDto departmentDto)
        {
            try
            {
                var mapper = _mapper.Map<Department>(departmentDto);
                _uow.GetRepository<Department>().Update(mapper);
                _uow.SaveChanges();
                departmentDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Department Updated Seccessfuly", Data = departmentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteDepartment(int departmentId)
        {
            try
            {
                Department oDepartment = _uow.GetRepository<Department>().Single(x => x.Id == departmentId);
                oDepartment.IsDeleted = true;
                oDepartment.DeletedOn = DateTime.Now;

                _uow.GetRepository<Department>().Update(oDepartment);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Department Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
