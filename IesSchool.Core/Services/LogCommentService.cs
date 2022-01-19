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
    internal class LogCommentService : ILogCommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
       

        public LogCommentService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
        }
        public ResponseDto GetStudentLogComments(int studentId)
        {
            try
            {
                var studentLogComments = _uow.GetRepository<LogComment>().GetList(x => x.IsDeleted != true && x.StudentId == studentId);
                var mapper = _mapper.Map<PaginateDto<LogCommentDto>>(studentLogComments);

                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetIEPLogComments(int iepId)
        {
            try
            {
                var iepLogComments = _uow.GetRepository<LogComment>().GetList(x => x.IsDeleted != true && x.IepId == iepId);
                var mapper = _mapper.Map<PaginateDto<LogCommentDto>>(iepLogComments);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddLogComment(LogCommentDto logCommentDto)
        {
            try
            {
                logCommentDto.IsDeleted = false;
                logCommentDto.CreatedOn = DateTime.Now;
                var mapper = _mapper.Map<LogComment>(logCommentDto);
                _uow.GetRepository<LogComment>().Add(mapper);
                _uow.SaveChanges();
                logCommentDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Log Comment Added  Seccessfuly", Data = logCommentDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditLogComment(LogCommentDto logCommentDto)
        {
            try
            {
                if (logCommentDto!= null)
                {
                    using var transaction = _iesContext.Database.BeginTransaction();
                    //var cmd = $"delete from Event_Teacher where EventId={oEventDto.Id}" +
                    //    $"delete from Event_Student where EventId={oEventDto.Id}";

                    //var mapper = _mapper.Map<LogComment>(logCommentDto);
                    //_uow.GetRepository<LogComment>().Update(mapper);
                    //_uow.SaveChanges();
                    //logCommentDto.Id = mapper.Id;
                    return new ResponseDto { Status = 1, Message = "Log Comment Updated Seccessfuly", Data = logCommentDto };
                }
                else
                {
                    return new ResponseDto { Status = 1, Message = "null", Data = logCommentDto };

                }

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteLogComment(int logCommentId)
        {
            try
            {
                LogComment oLogComment = _uow.GetRepository<LogComment>().Single(x => x.Id == logCommentId);
                oLogComment.IsDeleted = true;
                oLogComment.DeletedOn = DateTime.Now;

                _uow.GetRepository<LogComment>().Update(oLogComment);
                _uow.SaveChanges();
                return new ResponseDto { Status = 1, Message = "Log Comment Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
