using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IAssistantService
    {
        public ResponseDto GetAssistantHelper();
        public ResponseDto GetAssistants(AssistantSearchDto assistantSearchDto);
        public ResponseDto GetAssistantById(int assistantId);
        public ResponseDto AddAssistant(AssistantDto assistantDto);
        public ResponseDto EditAssistant(AssistantDto assistantDto);
        public ResponseDto DeleteAssistant(int assistantId);
        public ResponseDto IsSuspended(int assistanttId, bool isSuspended);

    }
}
