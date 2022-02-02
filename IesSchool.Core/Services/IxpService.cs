﻿using AutoMapper;
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
    internal class IxpService : IIxpService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        public IxpService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
        }
        public ResponseDto GetIxpsHelper()
        {
            try
            {
                IxpHelper ixpHelper = new IxpHelper()
                {
                    AllDepartments = _uow.GetRepository<Department>().GetList(null, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                    AllStudents = _uow.GetRepository<VwStudent>().GetList((x => new VwStudent { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code, DepartmentId = x.DepartmentId, DateOfBirth = x.DateOfBirth }), null, null, null, 0, 100000, true),
                    AllAcadmicYears = _uow.GetRepository<AcadmicYear>().GetList(null, null, null, 0, 1000000, true),
                    AllTerms = _uow.GetRepository<Term>().GetList(null, null, null, 0, 1000000, true),
                    AllExTeacher = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, DepartmentId = x.DepartmentId }), x => x.IsExtraCurricular == true, null, null, 0, 1000000, true),
                    AllHeadOfEducations = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name }), x => x.IsHeadofEducation == true, null, null, 0, 1000000, true),
                    AllExtraCurriculars = _uow.GetRepository<ExtraCurricular>().GetList(null, null, null, 0, 1000000, true),
                    UserExtraCurricular = _uow.GetRepository<UserExtraCurricular>().GetList(null, null, null, 0, 1000000, true),
                };
                var mapper = _mapper.Map<IxpHelperDto>(ixpHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetIxps(IxpSearchDto ixpSearchDto)
        {
            try
            {
                var AllIxpsx = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true, null,
                    x => x.Include(s => s.Student).ThenInclude(s => s.Department)
                     .Include(s => s.AcadmicYear)
                     .Include(s => s.Term)
                     .Include(s => s.IxpExtraCurriculars), 0, 100000, true);
                var AllIxps = _mapper.Map<PaginateDto<IxpDto>>(AllIxpsx).Items;
                if (ixpSearchDto.Student_Id != null)
                {
                    AllIxps = AllIxps.Where(x => x.StudentId == ixpSearchDto.Student_Id).ToList();
                }
                if (ixpSearchDto.AcadmicYear_Id != null)
                {
                    AllIxps = AllIxps.Where(x => x.AcadmicYearId == ixpSearchDto.AcadmicYear_Id).ToList();
                }
                if (ixpSearchDto.ExtraCurricularName != null)
                {
                    AllIxps = AllIxps.Where(x => x.IxpExtraCurricularsName.Contains( ixpSearchDto.ExtraCurricularName)).ToList();
                }
                if (ixpSearchDto.Term_Id != null)
                {
                    AllIxps = AllIxps.Where(x => x.TermId == ixpSearchDto.Term_Id).ToList();
                }
                if (ixpSearchDto.Status != null)
                {
                    AllIxps = AllIxps.Where(x => x.Status == ixpSearchDto.Status).ToList();
                }
                if (ixpSearchDto.IsPublished != null)
                {
                    AllIxps = AllIxps.Where(x => x.IsPublished == ixpSearchDto.IsPublished).ToList();
                }
                if (ixpSearchDto.Index == null || ixpSearchDto.Index == 0)
                {
                    ixpSearchDto.Index = 0;
                }
                else
                {
                    ixpSearchDto.Index += 1;
                }
                var mapper = new PaginateDto<IxpDto> { Count = AllIxps.Count(), Items = AllIxps != null ? AllIxps.Skip(ixpSearchDto.Index == null || ixpSearchDto.PageSize == null ? 0 : ((ixpSearchDto.Index.Value - 1) * ixpSearchDto.PageSize.Value)).Take(ixpSearchDto.PageSize ??= 20).ToList() : AllIxps.ToList() };

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetIxpById(int ixpId)
        {
            try
            {
                var ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == ixpId && x.IsDeleted != true, null,
                    x => x
                    .Include(x => x.IxpExtraCurriculars).ThenInclude(x => x.ExtraCurricular)
                    .Include(x => x.IxpExtraCurriculars).ThenInclude(x => x.Teacher)
                     .Include(s => s.Student).ThenInclude(s => s.Department)
                     .Include(s => s.AcadmicYear)
                     .Include(s => s.Term));
                var mapper = _mapper.Map<IxpDto>(ixp);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddIxp(IxpDto ixpDto)
        {
            try
            {
                if (ixpDto != null)
                {
                    ixpDto.IsDeleted = false;
                    ixpDto.CreatedOn = DateTime.Now;
                    var mapper = _mapper.Map<Ixp>(ixpDto);
                    _uow.GetRepository<Ixp>().Add(mapper);
                    _uow.SaveChanges();
                    ixpDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Ixp Added  Seccessfuly", Data = ixpDto };
                }
                else
                    return new ResponseDto { Status = 1, Message = "null" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditIxp(IxpDto ixpDto)
        {
            if (ixpDto != null)
            {
                var mapper = _mapper.Map<Ixp>(ixpDto);
                _uow.GetRepository<Ixp>().Update(mapper);
                _uow.SaveChanges();
                ixpDto.Id = mapper.Id;
                ixpDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Ixp Updated Seccessfuly", Data = ixpDto };
            }
            else
            {
                return new ResponseDto { Status = 1, Message = "null" };
            }

        }
        public ResponseDto DeleteIxp(int ixpId)
        {
            try
            {

                if (ixpId > 0)
                {
                    var ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == ixpId);
                    ixp.IsDeleted = true;
                    ixp.DeletedOn = DateTime.Now;
                    var mapper = _mapper.Map<Ixp>(ixp);
                    _uow.GetRepository<Ixp>().Update(mapper);

                    _uow.SaveChanges();
                    return new ResponseDto { Status = 1, Message = "Ixp Deleted Seccessfuly" };
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
        public ResponseDto IxpStatus(int ixpId, int status)
        {
            try
            {
                if (ixpId != 0)
                {
                    Ixp ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == ixpId);
                    if (ixp != null)
                    {
                        ixp.Status = status;
                        _uow.GetRepository<Ixp>().Update(ixp);
                        _uow.SaveChanges();
                        return new ResponseDto { Status = 1, Message = "Ixp Status Has Changed" };
                    }
                    else
                    {
                        return new ResponseDto { Status = 1, Message = "null" };
                    }
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
        public ResponseDto IxpIsPublished(int ixpId, bool isPublished)
        {
            try
            {
                if (ixpId != 0)
                {
                    Ixp ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == ixpId);
                    if (ixp != null)
                    {
                        ixp.IsPublished = isPublished;
                        _uow.GetRepository<Ixp>().Update(ixp);
                        _uow.SaveChanges();
                        return new ResponseDto { Status = 1, Message = "Ixp Is Published Status Has Changed" };
                    }
                    else
                    {
                        return new ResponseDto { Status = 1, Message = "null" };
                    }
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
        public ResponseDto IxpDuplicate(int ixpId)
        {
            try
            {
                var ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == ixpId && x.IsDeleted != true, null,
                    x => x
                    .Include(x => x.IxpExtraCurriculars).ThenInclude(x => x.ExtraCurricular)
                    .Include(x => x.IxpExtraCurriculars).ThenInclude(x => x.Teacher)
                     .Include(s => s.Student).ThenInclude(s => s.Department)
                     .Include(s => s.AcadmicYear)
                     .Include(s => s.Term));
                var mapper = _mapper.Map<IxpDto>(ixp);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

        public ResponseDto GetIxpExtraCurricularByIxpId(int ixpId)
        {
            try
            {
                var ixpExtraCurricular = _uow.GetRepository<IxpExtraCurricular>().GetList(x => x.IxpId == ixpId,null,
                    x=> x.Include(x => x.ExtraCurricular).Include(x => x.Teacher)
                );
                var mapper = _mapper.Map<PaginateDto<IxpExtraCurricularDto>>(ixpExtraCurricular);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetIxpExtraCurricularById(int ixpExtraCurricularId)
        {
            try
            {
                if (ixpExtraCurricularId != 0)
                {
                    var ixpExtraCurricular = _uow.GetRepository<IxpExtraCurricular>().Single(x => x.Id == ixpExtraCurricularId , null, x => x.Include(s => s.ExtraCurricular).Include(s => s.Teacher));
                    var mapper = _mapper.Map<IxpExtraCurricularDto>(ixpExtraCurricular);
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
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
        public ResponseDto AddIxpExtraCurricular(IxpExtraCurricularDto ixpExtraCurricularDto)
        {
            try
            {
                if (ixpExtraCurricularDto != null)
                {
                      
                    var mapper = _mapper.Map<IxpExtraCurricular>(ixpExtraCurricularDto);
                    _uow.GetRepository<IxpExtraCurricular>().Add(mapper);
                    _uow.SaveChanges();
                    ixpExtraCurricularDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Ixp Extra Curricular Added  Seccessfuly", Data = ixpExtraCurricularDto };
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
        public ResponseDto EditIxpExtraCurricular(IxpExtraCurricularDto ixpExtraCurricularDto)
        {
            try
            {
                if (ixpExtraCurricularDto != null)
                {
                   
                    var mapper = _mapper.Map<IxpExtraCurricular>(ixpExtraCurricularDto);
                    _uow.GetRepository<IxpExtraCurricular>().Update(mapper);
                    _uow.SaveChanges();

                    ixpExtraCurricularDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Goal Updated Seccessfuly", Data = ixpExtraCurricularDto };
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
        public ResponseDto DeleteIxpExtraCurricular(int ixpExtraCurricularId)
        {
            try
            {
                if (ixpExtraCurricularId != 0)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    var cmd = $"delete from IXP_ExtraCurricular where Id={ixpExtraCurricularId}";
                    _iesContext.Database.ExecuteSqlRaw(cmd);

                    transaction.Commit();

                    return new ResponseDto { Status = 1, Message = "Goal Deleted Seccessfuly" };
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

    }
}