using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class MobileService : IMobileService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MobileService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public bool IsParentExist(string UserName, string Password)
        {
            try
            {
                if (UserName != null && Password != null)
                {
                   // obj.Password.CompareTo(pass) == 0/
                    //var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.Equals( Password, StringComparison.Ordinal) && x.IsSuspended != true);
                    var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.CompareTo( Password)==0 && x.IsSuspended != true);
                    if (user!= null)
                        return true;
                }
               
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ResponseDto GetParentById(int parentId)
        {
            try
            {
                var parent = _uow.GetRepository<User>().Single(x => x.Id == parentId && x.IsDeleted != true);
                var mapper = _mapper.Map<ParentDto>(parent);

                if (parent != null)
                {
                    parent.ImageBinary = null;
                    var parentStudents = _uow.GetRepository<Student>().GetList(x => x.ParentId == parent.Id && x.IsDeleted != true);
                    if (parentStudents.Items.Count()>0)
                    {
                        var mapperStudent = _mapper.Map<PaginateDto<StudentDto>>(parentStudents).Items;
                        if (mapperStudent.Count()>0)
                        {
                            mapper.Students = mapperStudent;
                        }
                    }
                    string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                    var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{mapper.Image}";
                    mapper.FullPath = fullpath;
                }

                
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
    }
}
