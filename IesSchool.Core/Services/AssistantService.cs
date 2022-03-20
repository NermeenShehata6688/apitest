using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.EntityFrameworkCore;
namespace IesSchool.Core.Services
{
    internal class AssistantService : IAssistantService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private  iesContext _iesContext;

        public AssistantService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;   
        }
        public ResponseDto GetAssistantHelper()
        {
            try
            {
                AssistantHelper assistantHelper = new AssistantHelper()
                {
                    AllDepartments = _uow.GetRepository<Department>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(x => x.SkillAlowedDepartments).Include(s => s.Students.Where(x => x.IsDeleted != true)).Include(s => s.Users.Where(x => x.IsDeleted != true)), 0, 10000, true),
                    AllNationalities = _uow.GetRepository<Country>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 10000, true),
                    AllTeachers = _uow.GetRepository<VwUser>().GetList(x => x.IsDeleted != true && x.IsTeacher == true, null, null, 0, 10000, true)
                };
                var mapper = _mapper.Map<AssistantHelperDto>(assistantHelper);

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAssistants(AssistantSearchDto assistantSearchDto)
        {
            try
            {
                var allAssistants = _uow.GetRepository<VwAssistant>().Query("select * from Vw_Assistants where IsDeleted != 1");

                if (assistantSearchDto.NationalityId != null)
                {
                    allAssistants = allAssistants.Where(x => x.NationalityId== assistantSearchDto.NationalityId);
                }
                if (assistantSearchDto.Code != null)
                {
                    allAssistants = allAssistants.Where(x => x.Code.Contains(assistantSearchDto.Code));
                }
                if (assistantSearchDto.Name != null)
                {
                    allAssistants = allAssistants.Where(x => x.Name.Contains(assistantSearchDto.Name));
                }
                if (assistantSearchDto.Email != null)
                {
                    allAssistants = allAssistants.Where(x => x.Email.Contains(assistantSearchDto.Email));
                }
                if (assistantSearchDto.DepartmentId != null)
                {
                    allAssistants = allAssistants.Where(x => x.DepartmentId == assistantSearchDto.DepartmentId);
                }
                if (assistantSearchDto.IsSuspended != null)
                {
                    allAssistants = allAssistants.Where(x => x.IsSuspended == assistantSearchDto.IsSuspended);
                }

                var listVwAssistantDto = _mapper.Map<List<VwAssistantDto>>(allAssistants);
                var mapper = new PaginateDto<VwAssistantDto> { Count = listVwAssistantDto.Count(), Items = listVwAssistantDto, Index= assistantSearchDto.Index,Pages= assistantSearchDto.PageSize };
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAssistantById(int assistantId)
        {
            try
            {
                var assistant = _uow.GetRepository<Assistant>().Single(x => x.Id == assistantId && x.IsDeleted != true,null, x => x.Include(x => x.UserAssistants));
                var mapper = _mapper.Map<AssistantDto>(assistant);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddAssistant(AssistantDto assistantDto)
        {
            try
            {
                var mapper = _mapper.Map<Assistant>(assistantDto);
                mapper.IsDeleted = false;
                mapper.CreatedOn = DateTime.Now;
                _uow.GetRepository<Assistant>().Add(mapper);
                _uow.SaveChanges();

                assistantDto.Id = mapper.Id;

                return new ResponseDto { Status = 1, Message = "Assistant Added  Seccessfuly", Data = assistantDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditAssistant(AssistantDto assistantDto)
        {
            try
            {
                var allUserAssistant = _uow.GetRepository<UserAssistant>().GetList(x=>x.AssistantId==assistantDto.Id);
                var cmd = $"delete from User_Assistant where AssistantId={assistantDto.Id}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                var mapper = _mapper.Map<Assistant>(assistantDto);

                try
                {
                    _uow.GetRepository<Assistant>().Update(mapper);
                    _uow.SaveChanges();
                }
                catch (Exception)
                {
                    _uow.GetRepository<UserAssistant>().Add(allUserAssistant.Items);
                    _uow.SaveChanges();
                    throw;
                }
                assistantDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Assistant Updated Seccessfuly", Data = assistantDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteAssistant(int assistantId)
        {
            try
            {
                Assistant oAssistant = _uow.GetRepository<Assistant>().Single(x => x.Id == assistantId);
                oAssistant.IsDeleted = true;
                oAssistant.DeletedOn = DateTime.Now;
                _uow.GetRepository<Assistant>().Update(oAssistant);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Assistant Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto IsSuspended(int assistanttId, bool isSuspended)
        {
            try
            {
                Assistant assistant = _uow.GetRepository<Assistant>().Single(x => x.Id == assistanttId);
                assistant.IsSuspended = isSuspended;
                _uow.GetRepository<Assistant>().Update(assistant);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Assistant Is Suspended State Has Changed" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
