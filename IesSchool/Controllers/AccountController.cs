using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static IesSchool.Core.Dto.MembershipDto;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AccountController : Controller
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly IUserApplicationGroupService _iaplicationGroupService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private IUserService _userService;

        public AccountController(
        IUserApplicationGroupService iaplicationGroupService,
            UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager,
            RoleManager<AspNetRole> roleManager,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            , IMapper mapper
             , IUserService userService

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _iaplicationGroupService = iaplicationGroupService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;

            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Accounts()
        {

            try
            {
                var result = _userManager.Users.ToList();
                foreach (var item in result)
                {
                    var asd = _userManager.GetRolesAsync(item);
                }
                var usersrole = result.Select(x => new
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    roles = string.Join(",", _iaplicationGroupService.GetGroups(x.Id).Select(c => c.Name)),
                    gruops = _iaplicationGroupService.GetGroups(x.Id).Select(c => c.Name).ToList()

                }).ToList();
                return Ok(new ResponseDto { Message = "Success", Data = usersrole, Status = 1 });
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpGet]
        public IEnumerable<object> GetRoles()
        {

            var roles = _roleManager.Roles.ToList();
            return roles;
        }

        [HttpPost]
        public async Task<ResponseDto> Login([FromBody] LoginDto model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
             
                if (result.Succeeded)
                {


                    var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Email);
                      var User=  _userService.GetJustUserById(appUser.Id);


                    List<string> roles = _userManager.GetRolesAsync(appUser).Result.ToList();
                    string token2 = CreateToken(appUser, roles );

                    //       var token = await GenerateJwtToken(model.Email, appUser);


                    //var authClaims = new List<Claim>
                    //    {
                    //        new Claim(ClaimTypes.Name, appUser.UserName),
                    //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //    };

                    //foreach (var userRole in roles)
                    //{
                    //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    //}
                    //var asd= _configuration["JWT:Secret"];
                    //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    //var token = new JwtSecurityToken(
                    //    issuer: _configuration["JWT:ValidIssuer"],
                    //    audience: _configuration["JWT:ValidAudience"],
                    //    //expires: DateTime.Now.AddHours(3),
                    //    expires: DateTime.Now.AddDays(3),
                    //    claims: authClaims,
                    //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    //    );

                    //    _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Name, appUser.UserName));
                    //var user = _userManager.GetUserAsync(User).Result;

                    //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    //var usersId = User.Claims.ToList();

                    //var CreateBy = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    // will give the user's userId

                    //var userName22 = User?.Identity?.Name;
                    //var userName2 = User.FindFirstValue(ClaimTypes.Name);
                    //var role = User.FindFirstValue(ClaimTypes.Role);

                    bool IsMangerOrHeadOfEducation = false;
                    if(User.IsManager??false)
                    {
                        IsMangerOrHeadOfEducation = true;
                    }
                    else if(User.IsHeadofEducation ?? false)
                    {
                        IsMangerOrHeadOfEducation = true;
                    }

                    return new ResponseDto {Status=1, Data = new{ roles = roles , token= token2,Id= appUser.Id, UserName = appUser.UserName, img=User.FullPath, IsMangerOrHeadOfEducation = IsMangerOrHeadOfEducation ,Name=User.Name,DeviceToken= User.DeviceToken } };
                }
                return new ResponseDto  { Errormessage = "Invalid Username or Password", Status = 0 };
            }
            catch (Exception lo)
            {

                return new ResponseDto { Errormessage = "Invalid Username or Password",Status=0, Data = lo };
            }
        }

        private string CreateToken(IdentityUser<int> user,List<string>roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };
            if (roles!=null)
            {
                foreach (var userRole in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                 _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
      
     
        [HttpPost]
        //[Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();

                }
                else
                {

                    //var userExists = await _userManager.FindByNameAsync(model.UserName);
                    var userExists = await _userManager.FindByIdAsync(model.Id.ToString());
                    if (userExists != null)
                        return Ok(new ResponseDto { Status = 5, Errormessage = "Error", Message = "User Already Exists " });

                    //ApplicationUser user = new ApplicationUser()
                    //{
                    //    FullName = model.FullName,
                    //    Email = model.Email,
                    //    SecurityStamp = Guid.NewGuid().ToString(),
                    //    UserName = model.UserName
                    //};

                    var user = new AspNetUser
                    {
                        UserName = model.UserName,
                        Id = model.Id
                        //Email = model.Email,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        _iaplicationGroupService.AddGroupToUser(model.Roles.Select(x => x.Id).ToArray(), user.Id);
                        return Ok(new ResponseDto { Status = 1, Message = "Registered  Seccessfuly!" });
                    }
                    if (!result.Succeeded)
                        return Ok(new ResponseDto { Errormessage = string.Join(",", result.Errors.Select(x => x.Description)) });

                    return Ok(new ResponseDto { Status = 0, Errormessage = "Registeration Faild ", Data = result.ToString() });

                }
            }
            catch (Exception ex)
            {

                return Ok(new ResponseDto { Status = 0, Errormessage = "Registeration Faild", Data = ex.Message.ToString() });

            }

            //return Ok(new ResponseDto { Status = 1, Message = "تم تسجيل المستخدم بنجاح!" });
        }

        [HttpPut]
        public async Task<object> Update([FromBody] RegisterDto model)
        {

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            user.UserName = model.Email;
            user.Email = model.Email;
            if (!string.IsNullOrEmpty(model.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var resetpassword = await _userManager.ResetPasswordAsync(user, token, model.Password);
            }
            var role = await _userManager.GetRolesAsync(user);
            var roleresult = await _userManager.RemoveFromRolesAsync(user, role);
            if (model.Roles != null && model.Roles.Length > 0)
            {
                roleresult = await _userManager.AddToRolesAsync(user, model.Roles);
            }
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return new { error = result.Errors.FirstOrDefault().Description };
        }





        [HttpGet]
        public IActionResult UpdateUserDeviceToken(int id, string deviceToken)
        {
            var user = _userService.UpdateUserDeviceToken(id, deviceToken);
            return Ok(user);
        }



        //[HttpPost]
        //public async Task<object> Delete([FromBody]RegisterDto model)
        //{
        //    //var user = new IdentityUser
        //    //{
        //    //    UserName = model.Email,
        //    //    Email = model.Email,
        //    //    Id = model.Id
        //    //};
        //    var user = await _userManager.FindByIdAsync(model.Id);
        //    var result = await _userManager.DeleteAsync(user);

        //    if (result.Succeeded)
        //    {
        //        //await _signInManager.SignInAsync(user, false);
        //        return true;
        //    }

        //    return new { error = "Invalid Username or Password" };
        //}




        [HttpGet]
        //[Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            else
            {
                try
                {
                    var res = _roleManager.Roles.OrderBy(x=>x.Name).ToList();
                    return Ok(new ResponseDto { Message = "Success", Data = res, Status = 1 });
                }
                catch (Exception ex)
                {

                    throw;
                }

            }


        }

        [HttpPost]
        //[Route("EditUser")]
        public async Task<IActionResult> EditUser(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            else
            {

                try
                {
                  var user = await _userManager.FindByNameAsync(model.UserName );
                    if (user!=null)
                    {

                  //  var user = await _userManager.FindByIdAsync(model.Id. );
                    //user.FullName = model.FullName;
                    //user.UserName = model.UserName;
                    //user.Email = model.Email;

                    //if (!string.IsNullOrEmpty(model.Password))
                    //{
                    //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    //    var resetpassword = await _userManager.ResetPasswordAsync(user, token, model.Password);
                    //}
                    _iaplicationGroupService.DeleteGroupFromUser(user.Id);
                    _iaplicationGroupService.AddGroupToUser(model.Roles.Select(x => x.Id).ToArray(), user.Id);

                    //var userq = await _userManager.UpdateAsync(user);

                    //var userRoles = await userManager.GetRolesAsync(user);
                    //await userManager.RemoveFromRolesAsync(user, userRoles);
                    //await userManager.AddToRolesAsync(user, model.Roles);
                    return Ok(new ResponseDto { Message = "Updated Successfully", Data = user, Status = 1 });
                    }
                    else
                    {
                        return Ok(new ResponseDto { Errormessage = "Failed To Update", Status =0 });

                    }
                }
                catch (Exception ex)
                {
                    return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "Failed To Update" + ex.Message.ToString(), Status = 0 });
                }
            }


        }
        [HttpPost]
        //[Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] RestPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {

                try
                {

                    var userr = _userManager.Users.First(x => x.UserName == model.UserName);
                  //  string resetToken = await _userManager.GeneratePasswordResetTokenAsync(userr);
                    //IdentityResult updateResult = await _userManager.ResetPasswordAsync(userr, resetToken, model.Password);
                    IdentityResult updateResult2 = await _userManager.ChangePasswordAsync(userr, model.OldPassword, model.NewPassword);
                    if (updateResult2.Succeeded)
                    {
                        return Ok(new ResponseDto {  Status = 1 ,Message = "Password Changed Succesfully !!"});
                    }
                    else
                    {
                        return Ok(new ResponseDto
                        { Status = 0, Errormessage = "faild To Change Password !!",
                            Data = string.Join(",", updateResult2.Errors.Select(x => x.Description))
                        });
                    }

                }
                catch (Exception ex)
                {

                    return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "Password Change Failed" + ex.Message.ToString(), Status = 0 });

                }

            }


        }
        [HttpPost]
        public async Task<IActionResult> RestPassword( RestPasswordModel model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {

                try
                {

                    var userr = _userManager.Users.First(x => x.UserName == model.UserName);
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(userr);
                   IdentityResult updateResult = await _userManager.ResetPasswordAsync(userr, resetToken, model.NewPassword);
                    if (updateResult.Succeeded)
                    {
                        return Ok(new ResponseDto {  Status = 1 ,Message = "Password Rest Succesfully"});
                    }
                    else
                    {
                        return Ok(new ResponseDto
                        {
                            Status = 0, Errormessage = "faild To Rest Password !!",
                            Data = string.Join(",", updateResult.Errors.Select(x => x.Description))
                        });
                    }

                }
                catch (Exception ex)
                {

                    return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "Password Change Failed" + ex.Message.ToString(), Status = 0 });

                }

            }


        }
        [HttpPost]
        //[Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    //var userr = await _userManager.FindByIdAsync(model.Id);
                    var result = await _userManager.DeleteAsync(user);
                    return Ok(new ResponseDto { Message = "Deleted Successfully", Status = 1 });
                }
                catch (Exception ex)
                {
                    return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "Faild To Delete" + ex.Message.ToString(), Status = 0 });

                }

            }


        }


        //GroupAuth


        //[HttpGet]
        //public IEnumerable<object> GetRoles()
        //{

        //    var roles = _roleManager.Roles.Select(x => x.Name).ToList();
        //    return roles;
        //}

        [HttpGet]
        public IEnumerable<object> GetObjectRoles()
        {

            var roles = _roleManager.Roles.Select(x => new
            {

                ApplicationRoleId = x.Id,
                x.Name,
            }).ToList();
            return roles;
        }


        [HttpPut]
        //[Route("UpdateUser")]

        public async Task<object> UpdateUser([FromBody] UpdateResources model)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                user.UserName = model.UserName;
                //user.Email = model.Email;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetpassword = await _userManager.ResetPasswordAsync(user, token, model.Password);
                }
                _iaplicationGroupService.DeleteGroupFromUser(user.Id);
                _iaplicationGroupService.AddGroupToUser(model.Roles.Select(x => x.Id).ToArray(), user.Id);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Ok(new ResponseDto { Message = "Updated Successfully", Status = 1 });

                }
                return Ok(new ResponseDto { Errormessage = result.Errors.FirstOrDefault().Description, Status = 0 });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "Failed To Delete" + ex.Message.ToString(), Status = 0 });

            }
        }


        [HttpPost]
        public async Task<object> Delete([FromBody] RegisterModel model)
        {
            //var user = new IdentityUser
            //{
            //    UserName = model.Email,
            //    Email = model.Email,
            //    Id = model.Id
            //};
                var user = await _userManager.FindByIdAsync(model.Id.ToString());

            //var user = await _userManager.FindByNameAsync(model.UserName);

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(user, false);
                return true;
            }

            return new { error = "Invalid Username or Password" };
        }



        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }


        [HttpPost]
        //[Route("AddGroup")]
        public IActionResult AddGroup([FromBody] ApplicationGroupDto model)
        {

            var mapper = _mapper.Map<ApplicationGroup>(model);

            var res = _iaplicationGroupService.Add(mapper);
            return Ok(res);

        }

        [HttpPost]
        //[Route("UpdateGroup")]
        public IActionResult UpdateGroup([FromBody] ApplicationGroupDto model)
        {
            var mapper = _mapper.Map<ApplicationGroup>(model);

            var res = _iaplicationGroupService.UpdateGroup(mapper);
            return Ok(res);
        }
        [HttpGet]

        //[Route("GetApplicationGroup")]
        public IEnumerable<ApplicationGroupDto> GetApplicationGroup()
        {

            var res = _iaplicationGroupService.GetGroups();
            return res;
        }

        [HttpGet]

        public ApplicationGroup FindGroup(int id)
        {

            return _iaplicationGroupService.FindGroup(id);

        }
        //[Route("DeleteGroup")]
        [HttpGet]

        public IActionResult DeleteGroup(int id)
        {

            var res = _iaplicationGroupService.DeleteGroup(id);
            return Ok(res);
        }

        [HttpPost]
        public void AddRolesGroup(int[] roles, int groupId)
        {

            _iaplicationGroupService.AddRoleToGroup(roles, groupId);

        }

        [HttpPost]
        public void RemoveRolesGroup(int roles, int groupId)
        {
            _iaplicationGroupService.DeleteRoleFromGroup(roles, groupId);
        }

        [HttpPost]
        public void AddGroupToUser(int[] groups, int user)
        {

            _iaplicationGroupService.AddGroupToUser(groups, user);

        }

       
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(PasswordResetDto passwordResetDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userService.ResetPasswordAsync(passwordResetDto);

        //        if (result != null)
        //            return Ok(result);

        //        return BadRequest(result);
        //    }

        //    return BadRequest("Invalid details");
        //}

    }


}

//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//public class AuthorizeAttribute : Attribute, IAuthorizationFilter
//{
//    public void OnAuthorization(AuthorizationFilterContext context)
//    {
//        var user = (User)context.HttpContext.Items["User"];
//        if (user == null)
//        {
//            // not logged in
//            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
//        }
//    }
//}

