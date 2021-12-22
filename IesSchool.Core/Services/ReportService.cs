using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using IesSchool.InfraStructure.Paging;
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
                    var iep = _uow.GetRepository<VwIep>().Single(x => x.Id == iepId && x.IsDeleted != true, null);
                    var mapper = _mapper.Map<IepLPReportDto>(iep);
                    var AllIepObjectives = _uow.GetRepository<Objective>().GetList(x => x.IsDeleted != true && x.IepId == iepId, null, x => x.Include(s => s.Activities).Include(s => s.ObjectiveSkills));
                    var mapperObj = _mapper.Map<Paginate<ObjectiveDto>>(AllIepObjectives);
                    mapper.ObjectiveDtos = mapperObj.Items;
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
