using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface ICurriculumService
    {
        public ResponseDto GetAreas();
        public ResponseDto GetAreaById(int areaId);
        public ResponseDto AddArea(AreaDto areaDto);
        public ResponseDto EditArea(AreaDto areaDto);
        public ResponseDto DeleteArea(int areaId);

        public ResponseDto GetStrands();
        public ResponseDto GetStrandsGroupByArea();
        public ResponseDto GetStrandById(int strandId);
        public ResponseDto AddStrand(StrandDto strandDto);
        public ResponseDto EditStrand(StrandDto strandDto);
        public ResponseDto DeleteStrand(int strandId);

        public ResponseDto GetSkills();
        public ResponseDto GetSkillById(int skillId);
        public ResponseDto AddSkill(SkillDto skillDto);
        public ResponseDto EditSkill(SkillDto skillDto);
        public ResponseDto DeleteSkill(int skillId);

        public ResponseDto GetAreaByIdWithStrands(int areaId);
        public ResponseDto GetAreaByIdWithStrandsAndSkills(int areaId);


    }
}
