
namespace IesSchool.Core.Dto
{
    public class HistoricalSkillsDto
    {
        public int? GoalId { get; set; }
        public int Id { get; set; }
        public string? ObjectiveNote { get; set; }
        public int? ObjectiveNumber { get; set; }
        public string? Indication { get; set; }
       
        public bool? IsDeleted { get; set; }
        public bool? IsMasterd { get; set; }
        

        public int?[] ObjSkillsNumbers { get; set; }
        public string? AreaName { get; set; }
        public string? StrandName { get; set; }

        public string? YearName { get; set; }
        public string? TermName { get; set; }

    }
}


