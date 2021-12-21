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
    internal class ReportService : IReportService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto IepLpReport(int iepId)
        {
            try
            {
                if (iepId != null || iepId != 0)
                {
                    var iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true, null, x => x.Include(s => s.Goals).ThenInclude(s => s.Objectives).ThenInclude(s => s.Activities));
                    var mapper = _mapper.Map<IepLPReportDto>(iep);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
                }
                else
                    return new ResponseDto { Status = 0, Errormessage = " Not Found"};
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
    }
}
