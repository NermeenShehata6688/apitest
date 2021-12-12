using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface ITermsService
    {
        public ResponseDto GetTerms();
        public ResponseDto GetTermById(int termId);
        public ResponseDto AddTerm(TermDto termDto);
        public ResponseDto EditTerm(TermDto termDto);
        public ResponseDto DeleteTerm(int termId);
    }
}
