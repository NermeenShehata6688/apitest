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
    internal class CurriculumService : ICurriculumService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;

        public CurriculumService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
        }
        public ResponseDto GetAreas()
        {
            try
            {
                var allAreas = _uow.GetRepository<Area>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(x => x.Strands.Where(s=> s.IsDeleted!=true)).ThenInclude(n=> n.Skills.Where(s => s.IsDeleted != true)), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<AreaDto>>(allAreas);
                
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAreaById(int areaId)
        {
            try
            {
                var area = _uow.GetRepository<Area>().Single(x => x.Id == areaId && x.IsDeleted != true, null, x => x.Include(x => x.Strands.Where(s => s.IsDeleted != true)).ThenInclude(n => n.Skills.Where(s => s.IsDeleted != true)));
                var mapper = _mapper.Map<AreaDetailsDto>(area);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddArea(AreaDto areaDto)
        {
            try
            {
                areaDto.IsDeleted = false;
                areaDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Area>(areaDto);
                _uow.GetRepository<Area>().Add(mapper);
                _uow.SaveChanges();
                areaDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Area Added  Seccessfuly", Data = areaDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditArea(AreaDto areaDto)
        {
            try
            {
                var mapper = _mapper.Map<Area>(areaDto);
                _uow.GetRepository<Area>().Update(mapper);
                _uow.SaveChanges();
                areaDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Area Updated Seccessfuly", Data = areaDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteArea(int areaId)
        {
            try
            {
                Area oArea = _uow.GetRepository<Area>().Single(x => x.Id == areaId);
                oArea.IsDeleted = true;
                oArea.DeletedOn = DateTime.Now;

                _uow.GetRepository<Area>().Update(oArea);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Area Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetStrands()
        {
            try
            {
                var allStrands = _uow.GetRepository<Strand>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder),x=>x.Include(n => n.Skills.Where(s => s.IsDeleted != true)), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<StrandDto>>(allStrands);
                return new ResponseDto { Status = 1, Errormessage = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetStrandById(int strandId)
        {
            try
            {
                var strand = _uow.GetRepository<Strand>().Single(x => x.Id == strandId && x.IsDeleted != true,null, x => x.Include(n => n.Skills.Where(s => s.IsDeleted != true)));
                var mapper = _mapper.Map<StrandSkillsDto>(strand);
                return new ResponseDto { Status = 1, Errormessage = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddStrand(StrandDto strandDto)
        {
            try
            {
                strandDto.IsDeleted = false;
                strandDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Strand>(strandDto);
                _uow.GetRepository<Strand>().Add(mapper);
                _uow.SaveChanges();
                strandDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Strand Added  Seccessfuly", Data = strandDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditStrand(StrandDto strandDto)
        {
            try
            {
                var mapper = _mapper.Map<Strand>(strandDto);
                _uow.GetRepository<Strand>().Update(mapper);
                _uow.SaveChanges();
                strandDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Strand Updated Seccessfuly", Data = strandDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteStrand(int strandId)
        {
            try
            {
                Strand oStrand = _uow.GetRepository<Strand>().Single(x => x.Id == strandId);
                oStrand.IsDeleted = true;
                oStrand.DeletedOn = DateTime.Now;

                _uow.GetRepository<Strand>().Update(oStrand);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Strand Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetSkills()
        {
            try
            {
                var allSkills = _uow.GetRepository<Skill>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(s => s.SkillAlowedDepartments).ThenInclude(x => x.Department).Include(s => s.Strand).Include(s => s.Strand.Area), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<SkillDto>>(allSkills);
                return new ResponseDto { Status = 1, Errormessage = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetSkillById(int skillId)
        {
            try
            {
                var skill = _uow.GetRepository<Skill>().Single(x => x.Id == skillId && x.IsDeleted != true, null, x => x.Include(s => s.SkillAlowedDepartments).ThenInclude(x => x.Department).Include(s => s.Strand).Include(s => s.Strand.Area));
                var mapper = _mapper.Map<SkillDto>(skill);
                return new ResponseDto { Status = 1, Errormessage = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddSkill(SkillDto skillDto)
        {
            try
            {
                skillDto.IsDeleted = false;
                skillDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<Skill>(skillDto);
                _uow.GetRepository<Skill>().Add(mapper);
                _uow.SaveChanges();
                skillDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Skill Added  Seccessfuly", Data = skillDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditSkill(SkillDto skillDto)
        {
            try
            {
                var allSkillAlowedDepartments = _uow.GetRepository<SkillAlowedDepartment>().GetList(x => x.SkillId == skillDto.Id);
                var cmd = $"delete from Skill_AlowedDepartment where SkillId={skillDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                var mapper = _mapper.Map<Skill>(skillDto);

                try
                {
                    _uow.GetRepository<Skill>().Update(mapper);
                    _uow.SaveChanges();
                }
                catch (Exception)
                {
                    _uow.GetRepository<SkillAlowedDepartment>().Add(allSkillAlowedDepartments.Items);
                    _uow.SaveChanges();
                    throw;
                }
                skillDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Student Updated Seccessfuly", Data = skillDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
            //try
            //{
            //    var mapper = _mapper.Map<Skill>(skillDto);
            //    _uow.GetRepository<Skill>().Update(mapper);
            //    _uow.SaveChanges();
            //    skillDto.Id = mapper.Id;
            //    return new ResponseDto { Status = 1, Message = "Skill Updated Seccessfuly", Data = skillDto };
            //}
            //catch (Exception ex)
            //{
            //    return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            //}
        }
        public ResponseDto DeleteSkill(int skillId)
        {
            try
            {
                Skill oSkill = _uow.GetRepository<Skill>().Single(x => x.Id == skillId);
                oSkill.IsDeleted = true;
                oSkill.DeletedOn = DateTime.Now;

                _uow.GetRepository<Skill>().Update(oSkill);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Skill Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }

        public ResponseDto GetAreaByIdWithStrands(int areaId)
        {
            try
            {
                var area = _uow.GetRepository<Area>().Single(x => x.Id == areaId && x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(x => x.Strands.Where(s => s.IsDeleted != true)));
                var mapper = _mapper.Map<AreaDetailsDto>(area);
                return new ResponseDto { Status = 1, Errormessage = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAreaByIdWithStrandsAndSkills(int areaId)
        {
            try
            {
                var area = _uow.GetRepository<Area>().Single(x => x.Id == areaId && x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(x => x.Strands.Where(s => s.IsDeleted != true)).ThenInclude(n => n.Skills.Where(s => s.IsDeleted != true)));
                var mapper = _mapper.Map<AreaDetailsDto>(area);
                return new ResponseDto { Status = 1, Errormessage = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };



            }
        }
    }
}
