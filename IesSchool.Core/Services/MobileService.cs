using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
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
        public MobileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public bool IsParentExist(string UserName, string Password)
        {
            try
            {
                if (UserName != null && Password != null)
                {
                    var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword == Password);
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
    }
}
