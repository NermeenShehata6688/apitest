using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using static IesSchool.Core.Dto.MembershipDto;

namespace IesSchool.Core.IServices
{
    public interface IUserApplicationGroupService
    {
        ResponseDto DeleteRoleFromGroup(int roleId, int groupId);
        ResponseDto AddRoleToGroup(int[] roleId, int groupId);
        ResponseDto AddGroupToUser(int[] groupIds, int userid);
        ResponseDto DeleteGroup(int id);
        IEnumerable<ApplicationGroupDto> GetGroups();
        ApplicationGroup FindGroup(int id);
        ResponseDto UpdateGroup(ApplicationGroup model);
        ResponseDto Add(ApplicationGroup model);
        ResponseDto DeleteGroupFromUser(int userId);
        IEnumerable<ApplicationGroup> GetGroups(int id);
        IEnumerable<ApplicationGroup> GetGroupsDetails(int id);
    }
}
