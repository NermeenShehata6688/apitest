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
                var allStudents = _uow.GetRepository<Student>().GetList(x => x.IsDeleted != true, null,null, 0, 100000, true).Items;
                statisticDto.StusentsCount = allStudents.Count();

                if (allStudents.Count>0)
                {
                    statisticDto.SuspendendStusentsCount = allStudents.Where(x=> x.IsSuspended==true).Count();
                    statisticDto.ActivetusentsCount = allStudents.Where(x=> x.IsActive==true).Count();
                    statisticDto.UnActivetusentsCount = allStudents.Where(x=> x.IsActive!=true).Count();
                }
                var setting = _uow.GetRepository<Setting>().Single();
                var ieps = _uow.GetRepository<Iep>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true).Items;
                if (ieps.Count > 0)
                {
                    statisticDto.CurrentYearIepsCount = ieps.Where(x => x.AcadmicYearId == setting.CurrentYearId).Count();
                    statisticDto.CurrentTermIepsCount = ieps.Where(x => x.TermId == setting.CurrentTermId).Count();
                }

                var itps = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true).Items;
                if (itps.Count > 0)
                {
                    statisticDto.CurrentYearItpsCount = itps.Where(x => x.AcadmicYearId == setting.CurrentYearId).Count();
                    statisticDto.CurrentTermItpsCount = itps.Where(x => x.TermId == setting.CurrentTermId).Count();
                }
                var ixps = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true).Items;
               
                    statisticDto.CurrentYearIxpsCount = itps.Where(x => x.AcadmicYearId == setting.CurrentYearId).Count();
                    statisticDto.CurrentTermIxpsCount = itps.Where(x => x.TermId == setting.CurrentTermId).Count();
                
                 statisticDto.AreasCount = _uow.GetRepository<Area>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true).Items.Count();
                 statisticDto.StrandsCount = _uow.GetRepository<Strand>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true).Items.Count();
                 statisticDto.SkillsCount = _uow.GetRepository<Skill>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true).Items.Count();

                var allUsers = _uow.GetRepository<User>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true).Items;
                if (allUsers.Count () >0)
                {
                    statisticDto.TeachersCount = allUsers.Where(x => x.IsTeacher == true).Count();
                    statisticDto.TherapistsCount = allUsers.Where(x => x.IsTherapist == true).Count();
                    statisticDto.ExtraTeachersCount = allUsers.Where(x => x.IsExtraCurricular == true).Count();

                }
                statisticDto.DepartmentsCount = _uow.GetRepository<Department>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true).Items.Count();
               
                var acadmic = _uow.GetRepository<AcadmicYear>().GetList(x => x.IsDeleted != true, null, x=> x
                .Include(x => x.Ieps.Where(x=> x.IsDeleted!=true)).Include(x => x.Itps.Where(x => x.IsDeleted != true)).Include(x => x.Ixps.Where(x => x.IsDeleted != true)), 0, 100000, true);
                if (acadmic!= null && acadmic.Items.Count()>0)
                {
                    statisticDto.AcadmicYearChartDto = _mapper.Map<PaginateDto<AcadmicYearChartDto>>(acadmic);
                    statisticDto.AcadmicYearChartDto.Items = statisticDto.AcadmicYearChartDto.Items.TakeLast(4).ToList();
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
