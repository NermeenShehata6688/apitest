using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LogCommentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseDto GetStudentLogComments(int studentId)
        {
            try
            {
                var studentLogComments = _uow.GetRepository<LogComment>().GetList(x => x.IsDeleted != true && x.StudentId == studentId, null,
                    x => x.Include(x => x.User));            

                var mapper = _mapper.Map<PaginateDto<LogCommentDto>>(studentLogComments);

                foreach (var log in mapper.Items)
                {
                    if (log.UserImage != null)
                    {
                        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                        if (File.Exists("wwwRoot/tempFiles/" + log.UserImage))
                        {
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{log.UserImage}";
                            log.UserImagePath = fullpath;
                        }
                    }
                }

                // var lstToSend = GetFullPathAndBinary(lstUserDto);

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
                logCommentDto.LogDate = DateTime.Now;
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
                    var mapper = _mapper.Map<LogComment>(logCommentDto);
                    _uow.GetRepository<LogComment>().Update(mapper);
                    _uow.SaveChanges();
                    logCommentDto.Id = mapper.Id;
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
        //public UserDto GetFullPathAndBinary(UserDto allUsers)
        //{
        //    try
        //    {
        //        if (allUsers.Count() > 0)
        //        {
        //            foreach (var item in allUsers)
        //            {
        //                if (File.Exists("wwwRoot/tempFiles/" + item.Image))
        //                {
        //                    string host = _httpContextAccessor.HttpContext.Request.Host.Value;

        //                    var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.Image}";
        //                    //var target = Path.Combine(Environment.CurrentDirectory, "wwwRoot/tempFiles"+$"{item.Image}");
        //                    item.FullPath = fullpath;
        //                }
        //                else
        //                {
        //                    if (item != null && item.Image != null)
        //                    {
        //                        var user = _uow.GetRepository<User>().Single(x => x.Id == item.Id && x.IsDeleted != true, null, null);
        //                        if (user.ImageBinary != null)
        //                        {
        //                            System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + item.Image, user.ImageBinary);
        //                        }
        //                        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
        //                        var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.Image}";
        //                        item.FullPath = fullpath;
        //                    }
        //                }

        //            }
        //        }
        //        return allUsers;
        //    }
        //    catch (Exception ex)
        //    {
        //        return allUsers;
        //    }
        //}
    }
}
