using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.EntityFrameworkCore;


namespace IesSchool.Core.Services
{
    public class UserApplicationGroupService : IUserApplicationGroupService
    {

        public iesContext _context { get; }


        public UserApplicationGroupService(iesContext context)
        {
            _context = context;
        }


        [Obsolete]
        public ResponseDto AddRoleToGroup(int[] roleId, int groupId)
        {
            foreach (var item in roleId)
            {
                _context.ApplicationGroupRoles.Add(new ApplicationGroupRole
                {
                    ApplicationGroupId = groupId,
                    ApplicationRoleId = item
                });
            }
            _context.SaveChanges();
            return new ResponseDto { Status = 1, Message = "تم انشاء المجموعة بنجاح  بنجاح" };

        }
        public ResponseDto Add(ApplicationGroup newrecord)
        {
            try
            {
                //newrecord.Id = Guid.NewGuid().ToString();
                _context.ApplicationGroups.Add(newrecord);
                _context.SaveChanges();
                return new ResponseDto { Status = 1, Message = "تم انشاء المجموعة بنجاح  بنجاح" };
            }
            catch (Exception ex)
            {

                return new ResponseDto { Status = 0, Errormessage = "فشلت عملية الانشاء", Data = ex.ToString() };

            }





        }
        public ResponseDto AddGroupToUser(int[] groupIds, int userid)
        {
        

            using (var context = _context.Database.BeginTransaction())
            {
                foreach (var item in groupIds)
                {
                    _context.ApplicationUserGroups.Add(new ApplicationUserGroup
                    {
                        ApplicationGroupId = item,
                        ApplicationUserId = userid
                    });

                }
                var roles = _context.ApplicationGroupRoles.Where(x => groupIds.Contains(x.ApplicationGroupId)).Select(s => s.ApplicationRole).Distinct();
                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        _context.AspNetUserRoles.Add(new AspNetUserRole
                        {
                            RoleId = role.Id,
                            UserId = userid,
                        });
                    }
                }
                _context.SaveChanges();

                context.Commit();
                return new ResponseDto { Status = 1, Message = "تم انشاء المجموعة بنجاح  بنجاح" };

            }

        }
        [Obsolete]
        public ResponseDto DeleteRoleFromGroup(string roleId, string groupId)
        {
            using (var context = _context.Database.BeginTransaction())
            {

                var cmd = "delete from AspNetUserRoles where UserId in (SELECT ApplicationUserGroup.ApplicationUserId" +
                            " FROM ApplicationGroupRole INNER JOIN" +
                            " ApplicationUserGroup ON ApplicationGroupRole.ApplicationGroupId = ApplicationUserGroup.ApplicationGroupId" +

                         $"where ApplicationGroupRole.ApplicationGroupId = '{groupId}') and RoleId = '{roleId}';";
                cmd += $"DELETE FROM ApplicationGroupRole where ApplicationGroupId = '{groupId}' and ApplicationRoleId = '{roleId}'";
                //_ = _context.Database.ExecuteSqlCommand(cmd);
                _ = _context.Database.ExecuteSqlRaw(cmd);

                context.Commit();
            }
            return new ResponseDto { Status = 1, Message = "تم انشاء المجموعة بنجاح  بنجاح" };

        }

        [Obsolete]
        public ResponseDto DeleteGroup(int groupId)
        {
            using (var context = _context.Database.BeginTransaction())
            {

                var cmd = "";

                var group = _context.ApplicationGroupRoles.Where(g => g.ApplicationGroupId == groupId);
                foreach (var item in group)
                {
                    cmd += "delete from AspNetUserRoles where UserId in (SELECT ApplicationUserGroup.ApplicationUserId" +
                            " FROM ApplicationGroupRole INNER JOIN" +
                            " ApplicationUserGroup ON ApplicationGroupRole.ApplicationGroupId = ApplicationUserGroup.ApplicationGroupId" +

                         $" where ApplicationGroupRole.ApplicationGroupId = '{groupId}') and RoleId = '{item.ApplicationRoleId}';";
                }

                cmd += $"DELETE FROM ApplicationGroupRole where ApplicationGroupId = '{groupId}';";
                cmd += $"DELETE FROM ApplicationUserGroup where ApplicationGroupId = '{groupId}';";
                cmd += $"DELETE FROM ApplicationGroup where Id = '{groupId}'";
                //_ = _context.Database.ExecuteSqlCommand(cmd);
                _ = _context.Database.ExecuteSqlRaw(cmd);
                context.Commit();
                return new ResponseDto { Status = 1, Message = "تم حذف المجموعة  بنجاح" };

            }
        }

        public IEnumerable<ApplicationGroup> GetGroups()
        {
            var res = _context.ApplicationGroups.Include(x => x.ApplicationGroupRoles).ThenInclude(c => c.ApplicationRole).ToList();
            return res;
        }
        public IEnumerable<ApplicationGroup> GetGroupsDetails(string id)
        {
            var res = _context.ApplicationGroups.Include(x => x.ApplicationGroupRoles).ThenInclude(c => c.ApplicationRole).ToList();
            return res;
        }

        public ApplicationGroup FindGroup(int id)
        {
            var res = _context.ApplicationGroups.AsNoTracking().Where(x => x.Id == id).Include(x => x.ApplicationUserGroups).Include(x => x.ApplicationGroupRoles).ThenInclude(c => c.ApplicationRole).FirstOrDefault();
            return res;
        }

        public ResponseDto UpdateGroup(ApplicationGroup model)
        {
            try
            {
               using (var context = _context.Database.BeginTransaction())
                {
                    var cmd = "";
                 //   _context.ApplicationGroups.Update(model);

                    var group = FindGroup(model.Id);
                    group.Name = model.Name;
                    foreach (var item in group.ApplicationGroupRoles)
                    {
                        cmd += "delete from AspNetUserRoles where UserId in (SELECT ApplicationUserGroup.ApplicationUserId" +
                                " FROM ApplicationGroupRole INNER JOIN" +
                                " ApplicationUserGroup ON ApplicationGroupRole.ApplicationGroupId = ApplicationUserGroup.ApplicationGroupId" +

                             $" where ApplicationGroupRole.ApplicationGroupId = '{model.Id}') and RoleId = '{item.ApplicationRoleId}';";
                    }

                    cmd += $"DELETE FROM ApplicationGroupRole where ApplicationGroupId = '{model.Id}'";
                    //_ = _context.Database.ExecuteSqlCommand(cmd);
                    _ = _context.Database.ExecuteSqlRaw(cmd);
                _ = _context.SaveChanges();

                    foreach (var item in model.ApplicationGroupRoles)
                    {
                        item.ApplicationGroupId = model.Id;
                    }
                    _context.ApplicationGroupRoles.AddRange(model.ApplicationGroupRoles);
                    _context.SaveChanges();
                    int[] groupid = { group.Id };
                    foreach (var item in group.ApplicationUserGroups)
                    {
                    //_context.ApplicationUserGroups.Add(new ApplicationUserGroup
                    //{
                    //    ApplicationGroupId = group.Id,
                    //    ApplicationUserId = item.ApplicationUserId
                    //});
                    //_ = _context.SaveChanges();

                    var roles = model.ApplicationGroupRoles.Select(s => s.ApplicationRoleId).Distinct().ToList();
                        if (roles != null)
                        {
                            foreach (var role in roles)
                            {
                                _context.AspNetUserRoles.Add(new AspNetUserRole
                                {
                                    RoleId = role,
                                    UserId = item.ApplicationUserId,
                                });
                            }
                        }
                    }
                _ = _context.SaveChanges();
                    context.Commit();
                    return new ResponseDto { Status = 1, Message = "تم تعديل المجموعة بنجاح  بنجاح" };
               }
            }
            catch (Exception ex)
            {

                return new ResponseDto { Status = 0, Errormessage = " فشل تعديل المجموعة", Data = ex.ToString() };

            }
        }

        public ResponseDto DeleteGroupFromUser(int userId)
        {
            var cmd = $"delete from ApplicationUserGroup where ApplicationUserId='{userId}'; delete from AspNetUserRoles where UserId ='{userId}' ";
           _ = _context.Database.ExecuteSqlRaw(cmd);
            return new ResponseDto { Status = 1, Message = "تم انشاء المجموعة بنجاح  بنجاح" };

        }

        public IEnumerable<ApplicationGroup> GetGroups(int id)
        {

            var query = $"SELECT ApplicationGroup.Id, ApplicationGroup.Name, ApplicationGroup.Description " +
                    " FROM ApplicationGroup INNER JOIN " +
                    " ApplicationUserGroup ON ApplicationGroup.Id = ApplicationUserGroup.ApplicationGroupId " +
                    $"where ApplicationUserId = '{id}'";
            //var res = _context.ApplicationGroup.FromSqlRaw(query);
            var res = _context.ApplicationGroups.FromSqlRaw(query);
            //var res = _context.ApplicationGroup.ToArray();
            return res;


        }

        public ResponseDto AddGroupToUser(string[] groupIds, string userid)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<ApplicationGroup> GetGroups(string id)
        //{
        //    throw new NotImplementedException();
        //}

        public ResponseDto DeleteRoleFromGroup(int roleId, int groupId)
        {
            throw new NotImplementedException();
        }

        //public ResponseDto DeleteGroupFromUser(int userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<ApplicationGroup> GetGroups(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<ApplicationGroup> GetGroupsDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
