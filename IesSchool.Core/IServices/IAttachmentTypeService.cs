using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IAttachmentTypeService
    {
        public ResponseDto GetAttachmentTypes();
        public ResponseDto GetAttachmentTypeById(int attachmentTypeId);
        public ResponseDto AddAttachmentType(AttachmentTypeDto attachmentTypeDto);
        public ResponseDto EditAttachmentType(AttachmentTypeDto attachmentTypeDto);
        public ResponseDto DeleteAttachmentType(int attachmentTypeId);
    }
}
