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
    internal class ItpService : IItpService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        public ItpService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
        }
        public ResponseDto GetItpsHelper()
        {
            try
            {
                ItpHelper itpHelper = new ItpHelper()
                {
                    AllDepartments = _uow.GetRepository<Department>().GetList(null, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                    AllStudents = _uow.GetRepository<VwStudent>().GetList((x => new VwStudent { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code }), null, null, null, 0, 100000, true),
                    AllAcadmicYears = _uow.GetRepository<AcadmicYear>().GetList(null, null, null, 0, 1000000, true),
                    AllTerms = _uow.GetRepository<Term>().GetList(null, null, null, 0, 1000000, true),
                    AllTherapist = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name }), x => x.IsTherapist == true, null, null, 0, 1000000, true),
                    AllHeadOfEducations = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name }), x => x.IsHeadofEducation == true, null, null, 0, 1000000, true),
                    AllParamedicalServices = _uow.GetRepository<ParamedicalService>().GetList(null, null, null, 0, 1000000, true),
                    AllStrategies = _uow.GetRepository<ParamedicalStrategy>().GetList(),
                };
                var mapper = _mapper.Map<ItpHelperDto>(itpHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItps()
        {
            try
            {
                var allItps = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ItpDto>>(allItps);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItpById(int itpId)
        {
            try
            {
                var itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId && x.IsDeleted != true, null,
                    x => x.Include(x => x.ItpObjectives).Include(x => x.ItpStrategies)
                     .Include(s => s.Student).ThenInclude(s => s.Department)
               .Include(s => s.Therapist)
               .Include(s => s.HeadOfDepartment)
               .Include(s => s.HeadOfEducation)
               .Include(s => s.AcadmicYear)
               .Include(s => s.Term));
                var mapper = _mapper.Map<ItpDto>(itp);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddItp(ItpDto itpDto)
        {
            try
            {
                if (itpDto != null)
                {
                    itpDto.IsDeleted = false;
                    itpDto.CreatedOn = DateTime.Now;
                    var mapper = _mapper.Map<Itp>(itpDto);
                    _uow.GetRepository<Itp>().Add(mapper);
                    _uow.SaveChanges();
                    itpDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Itp Added  Seccessfuly", Data = itpDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditItp(ItpDto itpDto)
        {
            if (itpDto != null)
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from ITP_Strategy where ItpId={itpDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
               
                var mapper = _mapper.Map<Itp>(itpDto);
                _uow.GetRepository<Itp>().Update(mapper);
                _uow.SaveChanges();
                itpDto.Id = mapper.Id;

                transaction.Commit();

                itpDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Itp Updated Seccessfuly", Data = itpDto };
            }
            else
            {
                return new ResponseDto { Status = 1, Message = "null" };
            }

        }
        public ResponseDto DeleteItp(List<ItpDto> itpDto)
        {
            try
            {
                if (itpDto != null)
                {
                    foreach (var itp in itpDto)
                    {
                        itp.IsDeleted = true;
                        itp.DeletedOn = DateTime.Now;
                        var mapper = _mapper.Map<Itp>(itp);
                        _uow.GetRepository<Itp>().Update(mapper);
                    }
                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Itp Deleted Seccessfuly" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }

            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto ItpStatus(int itpId, int status)
        {
            try
            {
                if (itpId != 0)
                {
                    Itp itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId);
                    itp.Status = status;
                    _uow.GetRepository<Itp>().Update(itp);
                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Itp Status Has Changed" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto ItpIsPublished(int itpId, bool isPublished)
        {
            try
            {
                if (itpId != 0)
                {
                    Itp itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId);
                    itp.IsPublished = isPublished;
                    _uow.GetRepository<Itp>().Update(itp);
                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Itp Is Published Status Has Changed" };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetItpObjectives()
        {
            try
            {
                var allItpObjectives = _uow.GetRepository<ItpObjective>().GetList(null, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<ItpObjectiveDto>>(allItpObjectives);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItpObjectiveByItpId(int itpId)
        {
            try
            {
                var itpObjectives = _uow.GetRepository<ItpObjective>().GetList(x => x.ItpId == itpId && x.IsDeleted != true, null);
                var mapper = _mapper.Map<PaginateDto<ItpObjectiveDto>>(itpObjectives);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddItpObjective(ItpObjectiveDto itpObjectiveDto)
        {
            try
            {
                var mapper = _mapper.Map<ItpObjective>(itpObjectiveDto);
                mapper.IsDeleted = false;
                mapper.CreatedOn = DateTime.Now;

                _uow.GetRepository<ItpObjective>().Add(mapper);
                _uow.SaveChanges();
                itpObjectiveDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Itp Objective Added  Seccessfuly", Data = itpObjectiveDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditItpObjective(ItpObjectiveDto itpObjectiveDto)
        {
            try
            {
                var mapper = _mapper.Map<ItpObjective>(itpObjectiveDto);
                _uow.GetRepository<ItpObjective>().Update(mapper);
                _uow.SaveChanges();
                itpObjectiveDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Itp Objective Updated Seccessfuly", Data = itpObjectiveDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteItpObjective(int itpObjectiveId)
        {
            try
            {
                ItpObjective oItpObjective = _uow.GetRepository<ItpObjective>().Single(x => x.Id == itpObjectiveId);
                oItpObjective.IsDeleted = true;
                oItpObjective.DeletedOn = DateTime.Now;

                _uow.GetRepository<ItpObjective>().Update(oItpObjective);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Itp Objective Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }


        }


        public ResponseDto GetItpProgressReportsByItpId(int itpId)
        {
            try
            {
                var itpProgressReports = _uow.GetRepository<ItpProgressReport>().GetList(x => x.ItpId == itpId && x.IsDeleted != true, null,
                    x => x.Include(x => x.Student).Include(x => x.AcadmicYear).Include(x => x.Term)
                    .Include(x => x.Teacher).Include(x => x.Therapist)
                    .Include(x => x.HeadOfEducation));

                var mapper = _mapper.Map<PaginateDto<ItpProgressReportDto>>(itpProgressReports);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetItpProgressReportById(int itpProgressReportId)
        {
            try
            {
                var itpProgressReport = _uow.GetRepository<ItpProgressReport>().Single(x => x.Id == itpProgressReportId && x.IsDeleted != true, null, 
                    x => x.Include(x => x.Student)
                  .Include(x => x.AcadmicYear).Include(x => x.Term)
                     .Include(x => x.Teacher)
                     .Include(x => x.ItpObjectiveProgressReports).ThenInclude(x => x.ItpObjective));
                var mapper = _mapper.Map<ItpProgressReportDto>(itpProgressReport);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddItpProgressReport(ItpProgressReportDto itpProgressReportDto)
        {
            try
            {
                itpProgressReportDto.IsDeleted = false;
                itpProgressReportDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<ItpProgressReport>(itpProgressReportDto);
                _uow.GetRepository<ItpProgressReport>().Add(mapper);
                _uow.SaveChanges();
                itpProgressReportDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Itp Progress Report Added  Seccessfuly", Data = itpProgressReportDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditItpProgressReport(ItpProgressReportDto itpProgressReportDto)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from ITP_ObjectiveProgressReport where ItpProgressReportId={itpProgressReportDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);

                var mapper = _mapper.Map<ItpProgressReport>(itpProgressReportDto);
                _uow.GetRepository<ItpProgressReport>().Update(mapper);
                _uow.SaveChanges();
                transaction.Commit();

                return new ResponseDto { Status = 1, Message = "Itp Progress Report Updated Seccessfuly", Data = itpProgressReportDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteItpProgressReport(int itpProgressReportId)
        {
            try
            {
                using var transaction = _iesContext.Database.BeginTransaction();
                var cmd = $"delete from ITP_ObjectiveProgressReport where ItpProgressReportId={itpProgressReportId}" ;
                _iesContext.Database.ExecuteSqlRaw(cmd);

                ItpProgressReport itpProgressReport = _uow.GetRepository<ItpProgressReport>().Single(x => x.Id == itpProgressReportId);
                itpProgressReport.IsDeleted = true;
                itpProgressReport.DeletedOn = DateTime.Now;

                var mapper = _mapper.Map<ItpProgressReport>(itpProgressReport);
                _uow.GetRepository<ItpProgressReport>().Update(mapper);
                _uow.SaveChanges();
                transaction.Commit();
                return new ResponseDto { Status = 1, Message = "Itp Progress Report Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto CreateItpProgressReport(int itpId)
        {
            try
            {
                if (itpId > 0)
                {
                    var itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId && x.IsDeleted != true, null, x => x
              
               .Include(s => s.Student)
               .Include(s => s.AcadmicYear)
               .Include(s => s.Term)
               .Include(s => s.Therapist)
               .Include(s => s.ItpObjectives));
                    ItpProgressReportDto itpProgressReportDto = new ItpProgressReportDto();
                    itpProgressReportDto.ItpId = itpId;

                    if (itp != null)
                    {
                        itpProgressReportDto.StudentCode = itp.Student == null ? 0 : itp.Student.Code;
                        itpProgressReportDto.StudentName = itp.Student == null ? "" : itp.Student.Name;
                        itpProgressReportDto.AcadmicYearName = itp.AcadmicYear == null ? "" : itp.AcadmicYear.Name;
                        itpProgressReportDto.TermName = itp.Term == null ? "" : itp.Term.Name;

                        if (itp.ItpObjectives.Count > 0)
                        {
                            itpProgressReportDto.ItpObjectiveProgressReports = new List<ItpObjectiveProgressReportDto>();
                                
                                for (int i = 0; i < itp.ItpObjectives.Count; i++)
                            {
                                itpProgressReportDto.ItpObjectiveProgressReports.Add(new ItpObjectiveProgressReportDto
                                {
                                    Id = 0,
                                    ItpProgressReportId = 0,
                                    ItpObjectiveId = itp.ItpObjectives.ToList()[i].Id == null ? 0 : itp.ItpObjectives.ToList()[i].Id,
                                    ItpObjectiveNote = itp.ItpObjectives.ToList()[i].ObjectiveNote == null ? "" : itp.ItpObjectives.ToList()[i].ObjectiveNote,
                                    Comment = ""
                                });
                            }
                        }
                    }
                    //var mapper = _mapper.Map<ItpProgressReportDto>(itp);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = itpProgressReportDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = " null" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
    }
}
