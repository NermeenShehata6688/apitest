using AutoMapper;
using Dapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.EntityFrameworkCore;
using Olsys.Business.Data;
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
                IxpHelper ixpHelper = new IxpHelper();
                //IxpHelper ixpHelper = new IxpHelper()
                //{
                //    AllDepartments = _uow.GetRepository<Department>().GetList(null, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
                //    AllStudents = _uow.GetRepository<VwStudent>().GetList((x => new VwStudent { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code, DepartmentId = x.DepartmentId, DateOfBirth = x.DateOfBirth, IsDeleted = x.IsDeleted }), null, null, null, 0, 100000, true),
                //    AllAcadmicYears = _uow.GetRepository<AcadmicYear>().GetList(null, null, null, 0, 1000000, true),
                //    AllTerms = _uow.GetRepository<Term>().GetList(null, null, null, 0, 1000000, true),
                //    AllExTeacher = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, DepartmentId = x.DepartmentId, IsDeleted = x.IsDeleted }), x => x.IsExtraCurricular == true, null, null, 0, 1000000, true),
                //    AllHeadOfEducations = _uow.GetRepository<User>().GetList((x => new User { Id = x.Id, Name = x.Name, IsDeleted = x.IsDeleted }), x => x.IsHeadofEducation == true, null, null, 0, 1000000, true),
                //    AllExtraCurriculars = _uow.GetRepository<ExtraCurricular>().GetList(null, null, null, 0, 1000000, true),
                //    UserExtraCurricular = _uow.GetRepository<UserExtraCurricular>().GetList(null, null, null, 0, 1000000, true),
                //};
                //var mapper = _mapper.Map<IxpHelperDto>(ixpHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = ixpHelper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public async Task<ResponseDto> GetIxpsHelperDapper()
        {
            try
            {
                IxpHelper ixpHelper = new IxpHelper();
                using (System.Data.IDbConnection dbConnection = ConnectionManager.GetConnection())
                {
                    dbConnection.Open();

                    var AllDepartments = await dbConnection.QueryAsync<Department>(SqlGeneralBuilder.Select_All_Department());
                    ixpHelper.AllDepartments = new PaginateDto<Department>
                    {
                        Items = AllDepartments.OrderBy(x => x.DisplayOrder).ToList(),
                        Count = AllDepartments.Count()
                    };

                    var allStudents =await dbConnection.QueryAsync<VwStudent>(SqlGeneralBuilder.Select_All_Students());
                    ixpHelper.AllStudents = new PaginateDto<VwStudent>
                    {
                        Items = allStudents.ToList(),
                        Count = allStudents.Count()
                    };
                    var allAcadmicYears =await dbConnection.QueryAsync<AcadmicYear>(SqlGeneralBuilder.Select_All_AcadimicYears());
                    ixpHelper.AllAcadmicYears = new PaginateDto<AcadmicYear>
                    {
                        Items = allAcadmicYears.ToList(),
                        Count = allAcadmicYears.Count()
                    };

                    var allTerms =await dbConnection.QueryAsync<Term>(SqlGeneralBuilder.Select_AllTerms());
                    ixpHelper.AllTerms = new PaginateDto<Term>
                    {
                        Items = allTerms.ToList(),
                        Count = allTerms.Count()
                    };

                    var AllExTeacher = (await dbConnection.QueryAsync<User>(SqlGeneralBuilder.Select_AllExtraCurricularsTeacher())).ToList();
                    ixpHelper.AllExTeacher = new PaginateDto<User>
                    {
                        Items = AllExTeacher,
                        Count = AllExTeacher.Count()
                    };
                    var AllHeadOfEducations = (await dbConnection.QueryAsync<User>(SqlGeneralBuilder.Select_AllHeadOfEducation())).ToList();
                    ixpHelper.AllHeadOfEducations = new PaginateDto<User>
                    {
                        Items = AllHeadOfEducations,
                        Count = AllHeadOfEducations.Count()
                    };
                    var AllExtraCurriculars = (await dbConnection.QueryAsync<ExtraCurricular>(SqlGeneralBuilder.Select_All_ExtraCurriculars())).ToList();
                    ixpHelper.AllExtraCurriculars = new PaginateDto<ExtraCurricular>
                    {
                        Items = AllExtraCurriculars,
                        Count = AllExtraCurriculars.Count()
                    };
                    var UserExtraCurricular = (await dbConnection.QueryAsync<UserExtraCurricular>(SqlGeneralBuilder.Select_All_UserExtraCurriculars())).ToList();
                    ixpHelper.UserExtraCurricular = new PaginateDto<UserExtraCurricular>
                    {
                        Items = UserExtraCurricular,
                        Count = UserExtraCurricular.Count()
                    };
                    dbConnection.Close();

                }

                return new ResponseDto { Status = 1, Message = "Success", Data = ixpHelper };
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
                      .Include(s => s.ExtraCurricular)
                       .Include(s => s.ExTeacher)
                     , 0, 100000, true);
                var AllIxps = _mapper.Map<PaginateDto<IxpDto>>(AllIxpsx).Items;
                if (ixpSearchDto.Student_Id != null)
                {
                    AllIxps = AllIxps.Where(x => x.StudentId == ixpSearchDto.Student_Id).ToList();
                }
                if (ixpSearchDto.AcadmicYear_Id != null)
                {
                    AllIxps = AllIxps.Where(x => x.AcadmicYearId == ixpSearchDto.AcadmicYear_Id).ToList();
                }
                if (ixpSearchDto.ExtraCurricularTeacher_Id != null)
                {
                    AllIxps = AllIxps.Where(x => x.ExTeacherId== ixpSearchDto.ExtraCurricularTeacher_Id).ToList();
                    //AllIxps = AllIxps.Where(x => x.ExtraCurricularTeacherIds.Contains(ixpSearchDto.ExtraCurricularTeacher_Id == null ? 0 : ixpSearchDto.ExtraCurricularTeacher_Id.Value)).ToList();
                }
                if (ixpSearchDto.Term_Id != null)
                {
                    AllIxps = AllIxps.Where(x => x.TermId == ixpSearchDto.Term_Id).ToList();
                }
                if (ixpSearchDto.ExtraCurricular_Id != null )
                {
                    AllIxps = AllIxps.Where(x => x.ExtraCurricularId== ixpSearchDto.ExtraCurricular_Id).ToList();
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
                    .Include(x => x.IxpExtraCurriculars)
                    //.Include(x => x.IxpExtraCurriculars).ThenInclude(x => x.Teacher)
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
                    using var transaction = _iesContext.Database.BeginTransaction();

                    ixpDto.IsDeleted = false;
                    ixpDto.CreatedOn = DateTime.Now;
                    var mapper = _mapper.Map<Ixp>(ixpDto);
                    _uow.GetRepository<Ixp>().Add(mapper);
                    _uow.SaveChanges();

                    if (ixpDto.FooterNotes != null)
                    {
                        if (mapper.Id>0)
                        {
                            var iepProgressExtra = _uow.GetRepository<ProgressReportExtraCurricular>().GetList(x => x.IepextraCurricularId == mapper.Id && x.IsDeleted != true);

                            if (iepProgressExtra != null && iepProgressExtra.Items.Count() > 0)
                            {
                                var iepProgressExtraLast = iepProgressExtra.Items.OrderByDescending(x => x.CreatedOn).First();

                                iepProgressExtraLast.Comment = ixpDto.FooterNotes;
                                _uow.GetRepository<ProgressReportExtraCurricular>().Update(iepProgressExtraLast);
                                _uow.SaveChanges();
                            }
                        }
                    }

                    var cmd = $"UPDATE IEP_ExtraCurricular SET IsIxpCreated = 1  Where Id =" + ixpDto.Id;
                    _iesContext.Database.ExecuteSqlRaw(cmd);
                    transaction.Commit();

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
        public async Task<ResponseDto> EditIxp(IxpDto ixpDto)
        {
            if (ixpDto != null)
            {
                var mapper = _mapper.Map<Ixp>(ixpDto);
                _uow.GetRepositoryAsync<Ixp>().UpdateAsync(mapper);
                _uow.SaveChanges();
                if (ixpDto.FooterNotes != null)
                {
                    if (mapper.Id > 0)
                    {
                        var iepProgressExtra =await _uow.GetRepositoryAsync<ProgressReportExtraCurricular>().GetListAsync(x => x.IepextraCurricularId == mapper.Id && x.IsDeleted != true);

                        if (iepProgressExtra != null && iepProgressExtra.Items.Count() > 0)
                        {
                            var iepProgressExtraLast = iepProgressExtra.Items.OrderByDescending(x => x.CreatedOn).First();

                            iepProgressExtraLast.Comment = ixpDto.FooterNotes;
                            _uow.GetRepository<ProgressReportExtraCurricular>().Update(iepProgressExtraLast);
                            _uow.SaveChanges();
                        }
                    }
                }
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
        public ResponseDto IxpStatus(StatusDto statusDto)
        {
            try
            {
                if (statusDto.Id != 0)
                {
                    Ixp ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == statusDto.Id);
                    if (ixp != null)
                    {
                        ixp.Status = statusDto.StatusNo;
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
        public ResponseDto IxpIsPublished(IsPuplishedDto isPuplishedDto)
        {
            try
            {
                if (isPuplishedDto.Id != 0)
                {
                    Ixp ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == isPuplishedDto.Id);
                    if (ixp != null)
                    {
                        ixp.IsPublished = isPuplishedDto.IsPuplished;
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
                if (ixpId > 0)
                {
                    var ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == ixpId && x.IsDeleted != true, null,
                                       x => x.Include(x => x.IxpExtraCurriculars));

                    if (ixp != null)
                    {
                        ixp.Id = 0;
                        if (ixp.IxpExtraCurriculars.Count() > 0)
                        {
                            ixp.IxpExtraCurriculars.ToList().ForEach(x => x.Id = 0);
                        }

                        ixp.Status = 0;
                        _uow.GetRepository<Ixp>().Add(ixp);
                        _uow.SaveChanges();

                        var mapper = _mapper.Map<IxpDto>(ixp);
                        return new ResponseDto { Status = 1, Message = " IXP has been Duplicated", Data = mapper };
                    }
                    else
                    {
                        return new ResponseDto { Status = 0, Message = " null" };
                    }
                }


                else
                {
                    return new ResponseDto { Status = 0, Message = " null" };
                }
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
                    //x=> x.Include(x => x.ExtraCurricular)
                    //.Include(x => x.Teacher)
                    null
                    , 0, 100000, true
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
                    var ixpExtraCurricular = _uow.GetRepository<IxpExtraCurricular>().Single(x => x.Id == ixpExtraCurricularId , null, null
                        //x => x.Include(s => s.ExtraCurricular).Include(s => s.Teacher)
                        );
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

        public ResponseDto GetIepsForExTeacher(int teacherId)
        {
            try
            {
                var iepExtraCurriculars = _uow.GetRepository<IepExtraCurricular>().GetList(x => x.ExTeacherId == teacherId && x.IsIxpCreated != true && x.IsDeleted != true, null,
                 x => x.Include(x => x.Iep).ThenInclude(x => x.Student)
                 .Include(x => x.Iep).ThenInclude(x => x.AcadmicYear)
                 .Include(x => x.Iep).ThenInclude(x => x.Term)
                 .Include(x => x.ExtraCurricular), 0, 100000, true);

                var mapper = _mapper.Map<PaginateDto<IepExtraTeacherDto>>(iepExtraCurriculars);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto CreateIxp(int iepExtraCurricularId)
        {
            try
            {
                var iepExtraCurricular = _uow.GetRepository<IepExtraCurricular>().Single(x => x.Id == iepExtraCurricularId && x.IsIxpCreated != true, null,
                 x => x.Include(x => x.Iep).ThenInclude(x => x.Student)
                 .Include(x => x.Iep).ThenInclude(x => x.AcadmicYear)
                 .Include(x => x.Iep).ThenInclude(x => x.Term));

                var mapper = _mapper.Map<IepExtraCreateIxpDto>(iepExtraCurricular);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }

    }
}
