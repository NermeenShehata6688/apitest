using IesSchool.Context.Models;
using IesSchool.Core.Dto;

namespace IesSchool.Core.IServices
{
    public interface IUserApplicationGroupService
    {
        ResponseDto DeleteRoleFromGroup(string roleId, string groupId);
        ResponseDto AddRoleToGroup(string[] roleId, string groupId);
        ResponseDto AddGroupToUser(string[] groupIds, string userid);
        ResponseDto DeleteGroup(string id);
        IEnumerable<ApplicationGroup> GetGroups();
        ApplicationGroup FindGroup(string id);
        ResponseDto UpdateGroup(ApplicationGroup model);
        ResponseDto Add(ApplicationGroup model);
        ResponseDto DeleteGroupFromUser(string userId);
        IEnumerable<ApplicationGroup> GetGroups(string id);
        IEnumerable<ApplicationGroup> GetGroupsDetails(string id);
    }
}
