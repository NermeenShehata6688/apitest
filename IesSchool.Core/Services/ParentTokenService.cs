using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class ParentTokenService : IParentTokenService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ParentTokenService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetParentToken()
        {
            try
            {
                var allParentTokens = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, DeviceToken = x.DeviceToken, IsDeleted = x.IsDeleted }), null, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<UserDto>>(allParentTokens);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetParentTokenByParentId(int parentId)
        {
            try
            {
                var ParentTokens = _uow.GetRepository<User>().Single(x => x.Id == parentId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<UserDto>(ParentTokens);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
    }
}
