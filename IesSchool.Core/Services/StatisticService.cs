using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public StatisticService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto GetStatistics()
        {
            try
            {
                StatisticDto statisticDto = new StatisticDto();
                var allStudents = _uow.GetRepository<Student>().GetList(x => x.IsDeleted != true).Items;
                if (allStudents.Count>0)
                {
                    statisticDto.StusentsCount = allStudents.Count();
                    statisticDto.SuspendendStusentsCount = allStudents.Where(x=> x.IsSuspended==true).Count();
                    statisticDto.ActivetusentsCount = allStudents.Where(x=> x.IsActive==true).Count();
                    statisticDto.UnActivetusentsCount = allStudents.Where(x=> x.IsActive==false).Count();
                }
                var setting = _uow.GetRepository<Setting>().Single();
                var ieps = _uow.GetRepository<Iep>().GetList(x => x.IsDeleted != true).Items;

                if (ieps.Count > 0)
                {
                    statisticDto.CurrentYearIepsCount = ieps.Where(x => x.AcadmicYearId == setting.CurrentYearId).Count();
                    statisticDto.CurrentTermIepsCount = ieps.Where(x => x.TermId == setting.CurrentTermId).Count();
                }
                var itps = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true).Items;

                if (itps.Count > 0)
                {
                    statisticDto.CurrentYearItpsCount = itps.Where(x => x.AcadmicYearId == setting.CurrentYearId).Count();
                    statisticDto.CurrentTermItpsCount = itps.Where(x => x.TermId == setting.CurrentTermId).Count();
                }

                return new ResponseDto { Status = 1, Message = "Success", Data = statisticDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
    }
}
