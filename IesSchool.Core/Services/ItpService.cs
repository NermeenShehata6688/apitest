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
                    return new ResponseDto { Status = 1, Message = "Iep Deleted Seccessfuly" };
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
                    return new ResponseDto { Status = 1, Message = "Iep Status Has Changed" };
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
                    return new ResponseDto { Status = 1, Message = "Iep Is Published Status Has Changed" };
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
                var itpObjectives = _uow.GetRepository<ItpObjective>().GetList(x => x.ItpId == itpId, null);
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
                return new ResponseDto { Status = 1, Message = "Itp Objectiver Updated Seccessfuly", Data = itpObjectiveDto };
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
                var cmd = $"delete from IEP_ExtraCurricular where Id={itpObjectiveId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Itp Objective Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
