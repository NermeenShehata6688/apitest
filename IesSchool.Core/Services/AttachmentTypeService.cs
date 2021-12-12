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
    internal class AttachmentTypeService : IAttachmentTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private iesContext _iesContext;
        public AttachmentTypeService(IUnitOfWork unitOfWork, IMapper mapper, iesContext iesContext)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _iesContext = iesContext;
        }
        public ResponseDto GetAttachmentTypes()
        {
            try
            {
                var allAttachmentTypes = _uow.GetRepository<AttachmentType>().GetList(null, null, null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<AttachmentTypeDto>>(allAttachmentTypes);
                return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetAttachmentTypeById(int attachmentTypeId)
        {
            try
            {
                var attachmentType = _uow.GetRepository<AttachmentType>().Single(x => x.Id == attachmentTypeId , null);
                var mapper = _mapper.Map<AttachmentTypeDto>(attachmentType);
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto AddAttachmentType(AttachmentTypeDto attachmentTypeDto)
        {
            try
            {
                var mapper = _mapper.Map<AttachmentType>(attachmentTypeDto);
                _uow.GetRepository<AttachmentType>().Add(mapper);
                _uow.SaveChanges();
                attachmentTypeDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Attachment Type Added  Seccessfuly", Data = attachmentTypeDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto EditAttachmentType(AttachmentTypeDto attachmentTypeDto)
        {
            try
            {
                var mapper = _mapper.Map<AttachmentType>(attachmentTypeDto);
                _uow.GetRepository<AttachmentType>().Update(mapper);
                _uow.SaveChanges();
                attachmentTypeDto.Id = mapper.Id;
                return new ResponseDto { Status = 1, Message = "Attachment Type Updated Seccessfuly", Data = attachmentTypeDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
        public ResponseDto DeleteAttachmentType(int attachmentTypeId)
        {
            try
            {
                var cmd = $"delete from AttachmentType where Id={attachmentTypeId}";
                _iesContext.Database.ExecuteSqlRaw(cmd);
                return new ResponseDto { Status = 1, Message = "Student Attachment Deleted Seccessfuly" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }
        }
    }
}
