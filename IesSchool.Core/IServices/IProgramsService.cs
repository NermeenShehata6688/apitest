using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IProgramsService
    {
        public ResponseDto GetPrograms();
        public ResponseDto GetProgramById(int programId);
        public ResponseDto AddProgram(ProgramDto programDto);
        public ResponseDto EditProgram(ProgramDto programDto);
        public ResponseDto DeleteProgram(int programId);
    }
}
