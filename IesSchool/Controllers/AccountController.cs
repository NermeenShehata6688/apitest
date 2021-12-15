//using IesSchool.Context.Models;
//using IesSchool.Core.Dto;
//using IesSchool.Core.IServices;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace IesSchool.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]

//    public class AccountController : Controller
//        {
//            private readonly SignInManager<IdentityUser<int>> _signInManager;
//            private readonly UserManager<IdentityUser<int>> _userManager;
//            private readonly IConfiguration _configuration;
//            private readonly RoleManager<IdentityRole<int>> _roleManager;
//            private readonly IUserApplicationGroupService _iaplicationGroupService;
//            private readonly IHttpContextAccessor _httpContextAccessor;

//            public AccountController(
//                IUserApplicationGroupService iaplicationGroupService,
//                UserManager<IdentityUser<int>> userManager,
//                SignInManager<IdentityUser<int>> signInManager,
//                RoleManager<IdentityRole<int>> roleManager,
//                IConfiguration configuration,
//                IHttpContextAccessor httpContextAccessor
//                )
//            {
//                _userManager = userManager;
//                _signInManager = signInManager;
//                _configuration = configuration;
//                _roleManager = roleManager;
//                _iaplicationGroupService = iaplicationGroupService;
//                _httpContextAccessor = httpContextAccessor;


//            }

//            [HttpGet]
//            public IActionResult Accounts()
//            {

//                try
//                {


//                    var result = _userManager.Users.ToList();
//                    foreach (var item in result)
//                    {
//                        var asd = _userManager.GetRolesAsync(item);
//                    }
//                    var usersrole = result.Select(x => new
//                    {
//                        Id = x.Id,
//                        UserName = x.UserName,
//                        Email = x.Email,
//                        //roles = string.Join(",", _iaplicationGroupService.GetGroups(x.Id).Select(c => c.Name)),
//                        //gruops = _iaplicationGroupService.GetGroups(x.Id).Select(c => c.Name).ToList()

//                    }).ToList();
//                    return Ok(new ResponseDto { Message = "Success", Data = usersrole, Status = 1 });
//                }
//                catch (Exception ex)
//                {

//                    throw;
//                }
//            }


//            [HttpGet]
//            public IEnumerable<object> GetRoles()
//            {

//                var roles = _roleManager.Roles.ToList();
//                return roles;
//            }

//            [HttpPost]
//            public async Task<object> Login([FromBody] LoginDto model)
//            {
//                try
//                {
//                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
//                    if (result.Succeeded)
//                    {

//                        var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Email);
//                        var roles = _userManager.GetRolesAsync(appUser).Result;
//                        var token = await GenerateJwtToken(model.Email, appUser);
//                        //_userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Name, appUser.UserName));
//                        var user = _userManager.GetUserAsync(User).Result;

//                        //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                        //var usersId = User.Claims.ToList();

//                        var CreateBy = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//                        // will give the user's userId
//                        return new { token = token, roles = roles };
//                    }
//                    return new { error = "Invalid Username or Password" };
//                }
//                catch (Exception lo)
//                {

//                    return new { error = "Invalid Username or Password", ex = lo };
//                }
//            }

//            //[HttpPost]
//            //public async Task<object> Register([FromBody]RegisterDto model)
//            //{
//            //    var user = new IdentityUser
//            //    {
//            //        UserName = model.Email,
//            //        Email = model.Email,

//            //    };

//            //    var result = await _userManager.CreateAsync(user, model.Password);

//            //    if (result.Succeeded)
//            //    {
//            //        //var roles =  _userManager.AddToRolesAsync(user, model.Roles);
//            //        var usere = await _userManager.FindByIdAsync(user.Id);
//            //        var roleresult = await _userManager.AddToRolesAsync(usere, model.Roles);

//            //        await _signInManager.SignInAsync(user, false);
//            //        return await GenerateJwtToken(model.Email, user);
//            //    }

//            //    return new { error = string.Join(",", result.Errors.Select(x=>x.Description)) };
//            //}


//            [HttpPost]
//            //[Route("register")]
//            public async Task<IActionResult> Register([FromBody] RegisterModel model)
//            {
//                try
//                {
//                    if (!ModelState.IsValid)
//                    {
//                        return BadRequest();

//                    }
//                    else
//                    {

//                        var userExists = await _userManager.FindByNameAsync(model.UserName);



//                        if (userExists != null)
//                            return Ok(new ResponseDto { Status = 5, Errormessage = "Error", Message = "هذا المستخدم موجود بالفعل" });

//                        //ApplicationUser user = new ApplicationUser()
//                        //{
//                        //    FullName = model.FullName,
//                        //    Email = model.Email,
//                        //    SecurityStamp = Guid.NewGuid().ToString(),
//                        //    UserName = model.UserName
//                        //};

//                        var user = new IdentityUser
//                        {
//                            UserName = model.UserName,
//                            //Email = model.Email,
//                        };
//                        var result = await _userManager.CreateAsync(user, model.Password);

//                        if (result.Succeeded)
//                        {
//                            _iaplicationGroupService.AddGroupToUser(model.Roles.Select(x => x.Id).ToArray(), user.Id);
//                            return Ok(new ResponseDto { Status = 1, Message = "تم تسجيل المستخدم بنجاح!" });
//                        }
//                        if (!result.Succeeded)
//                            return Ok(new ResponseDto { Errormessage = string.Join(",", result.Errors.Select(x => x.Description)) });

//                        return Ok(new ResponseDto { Status = 0, Errormessage = "فشلت عملية التسجيل برجاء مراجعة البيانات المرسلة", Data = result.ToString() });

//                    }
//                }
//                catch (Exception ex)
//                {

//                    return Ok(new ResponseDto { Status = 0, Errormessage = "فشلت عملية التسجيل برجاء مراجعة البيانات المرسلة", Data = ex.Message.ToString() });

//                }

//                //return Ok(new ResponseDto { Status = 1, Message = "تم تسجيل المستخدم بنجاح!" });
//            }

//            [HttpPut]
//            public async Task<object> Update([FromBody] RegisterDto model)
//            {

//                var user = await _userManager.FindByIdAsync(model.Id);
//                user.UserName = model.Email;
//                user.Email = model.Email;
//                if (!string.IsNullOrEmpty(model.Password))
//                {
//                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

//                    var resetpassword = await _userManager.ResetPasswordAsync(user, token, model.Password);
//                }
//                var role = await _userManager.GetRolesAsync(user);
//                var roleresult = await _userManager.RemoveFromRolesAsync(user, role);
//                if (model.Roles != null && model.Roles.Length > 0)
//                {
//                    roleresult = await _userManager.AddToRolesAsync(user, model.Roles);
//                }
//                var result = await _userManager.UpdateAsync(user);

//                if (result.Succeeded)
//                {
//                    return true;
//                }

//                return new { error = result.Errors.FirstOrDefault().Description };
//            }


//            //[HttpPost]
//            //public async Task<object> Delete([FromBody]RegisterDto model)
//            //{
//            //    //var user = new IdentityUser
//            //    //{
//            //    //    UserName = model.Email,
//            //    //    Email = model.Email,
//            //    //    Id = model.Id
//            //    //};
//            //    var user = await _userManager.FindByIdAsync(model.Id);
//            //    var result = await _userManager.DeleteAsync(user);

//            //    if (result.Succeeded)
//            //    {
//            //        //await _signInManager.SignInAsync(user, false);
//            //        return true;
//            //    }

//            //    return new { error = "Invalid Username or Password" };
//            //}

//            private async Task<object> GenerateJwtToken(string email, IdentityUser user)
//            {
//                var claims = new List<Claim>
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, email),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim(ClaimTypes.NameIdentifier, user.Id)
//            };

//                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
//                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

//                var token = new JwtSecurityToken(
//                    _configuration["JwtIssuer"],
//                    _configuration["JwtIssuer"],
//                    claims,
//                    expires: expires,
//                    signingCredentials: creds
//                );

//                return new JwtSecurityTokenHandler().WriteToken(token);
//            }

//            //////////////asssssssssssssdddddddddddddddddddddddd

//            [HttpGet]
//            //[Route("GetAllRoles")]
//            public IActionResult GetAllRoles()
//            {
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest();

//                }
//                else
//                {
//                    try
//                    {
//                        var res = _roleManager.Roles.ToList();
//                        return Ok(new ResponseDto { Message = "Success", Data = res, Status = 1 });
//                    }
//                    catch (Exception ex)
//                    {

//                        throw;
//                    }

//                }


//            }

//            [HttpPost]
//            //[Route("EditUser")]
//            public async Task<IActionResult> EditUser([FromBody] RegisterModel model)
//            {
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest();

//                }
//                else
//                {

//                    try
//                    {
//                        var user = await _userManager.FindByIdAsync(model.Id);
//                        //user.FullName = model.FullName;
//                        user.UserName = model.UserName;
//                        user.Email = model.Email;

//                        if (!string.IsNullOrEmpty(model.Password))
//                        {
//                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

//                            var resetpassword = await _userManager.ResetPasswordAsync(user, token, model.Password);
//                        }
//                        _iaplicationGroupService.DeleteGroupFromUser(user.Id);
//                        _iaplicationGroupService.AddGroupToUser(model.Roles.Select(x => x.Id).ToArray(), user.Id);

//                        var userq = await _userManager.UpdateAsync(user);

//                        //var userRoles = await userManager.GetRolesAsync(user);
//                        //await userManager.RemoveFromRolesAsync(user, userRoles);
//                        //await userManager.AddToRolesAsync(user, model.Roles);
//                        return Ok(new ResponseDto { Message = "تم التعديل بنجاح", Data = user, Status = 1 });
//                    }
//                    catch (Exception ex)
//                    {
//                        return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "فشل في عملية التعديل" + ex.Message.ToString(), Status = 0 });
//                    }
//                }


//            }
//            [HttpPost]
//            //[Route("ChangePassword")]
//            public async Task<IActionResult> ChangePassword([FromBody] RegisterModel model)
//            {
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest();
//                }
//                else
//                {

//                    try
//                    {

//                        var userr = _userManager.Users.First(x => x.Email == model.Email);
//                        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(userr);
//                        IdentityResult updateResult = await _userManager.ResetPasswordAsync(userr, resetToken, model.Password);
//                        return Ok(new ResponseDto { Message = "تم تغير كلمة المرور بنجاح", Status = 1 });

//                    }
//                    catch (Exception ex)
//                    {

//                        return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "فشل في عملية تغيير كلمة المرور" + ex.Message.ToString(), Status = 0 });

//                    }

//                }


//            }
//            [HttpPost]
//            //[Route("DeleteUser")]
//            public async Task<IActionResult> DeleteUser([FromBody] RegisterModel model)
//            {
//                if (!ModelState.IsValid)
//                {
//                    return BadRequest();
//                }
//                else
//                {
//                    try
//                    {
//                        var userr = await _userManager.FindByIdAsync(model.Id);
//                        var result = await _userManager.DeleteAsync(userr);
//                        return Ok(new ResponseDto { Message = "تم الحذف بنجاح", Status = 1 });
//                    }
//                    catch (Exception ex)
//                    {
//                        return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "فشل في عملية الحذف  " + ex.Message.ToString(), Status = 0 });

//                    }

//                }


//            }


//            //GroupAuth


//            //[HttpGet]
//            //public IEnumerable<object> GetRoles()
//            //{

//            //    var roles = _roleManager.Roles.Select(x => x.Name).ToList();
//            //    return roles;
//            //}

//            [HttpGet]
//            public IEnumerable<object> GetObjectRoles()
//            {

//                var roles = _roleManager.Roles.Select(x => new
//                {

//                    ApplicationRoleId = x.Id,
//                    x.Name,
//                }).ToList();
//                return roles;
//            }


//            [HttpPut]
//            //[Route("UpdateUser")]

//            public async Task<object> UpdateUser([FromBody] UpdateResources model)
//            {

//                try
//                {


//                    var user = await _userManager.FindByIdAsync(model.Id);
//                    user.UserName = model.UserName;
//                    //user.Email = model.Email;
//                    if (!string.IsNullOrEmpty(model.Password))
//                    {
//                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

//                        var resetpassword = await _userManager.ResetPasswordAsync(user, token, model.Password);
//                    }
//                    _iaplicationGroupService.DeleteGroupFromUser(user.Id);
//                    _iaplicationGroupService.AddGroupToUser(model.Roles.Select(x => x.Id).ToArray(), user.Id);

//                    var result = await _userManager.UpdateAsync(user);

//                    if (result.Succeeded)
//                    {
//                        return Ok(new ResponseDto { Message = "تم التعديل  بنجاح", Status = 1 });

//                    }
//                    return Ok(new ResponseDto { Errormessage = result.Errors.FirstOrDefault().Description, Status = 0 });
//                }
//                catch (Exception ex)
//                {
//                    return Ok(new ResponseDto { Data = ex.ToString(), Errormessage = "فشل في عملية الحذف  " + ex.Message.ToString(), Status = 0 });

//                }
//            }


//            [HttpPost]
//            public async Task<object> Delete([FromBody] RegisterModel model)
//            {
//                //var user = new IdentityUser
//                //{
//                //    UserName = model.Email,
//                //    Email = model.Email,
//                //    Id = model.Id
//                //};
//                var user = await _userManager.FindByIdAsync(model.Id);
//                var result = await _userManager.DeleteAsync(user);

//                if (result.Succeeded)
//                {
//                    //await _signInManager.SignInAsync(user, false);
//                    return true;
//                }

//                return new { error = "Invalid Username or Password" };
//            }



//            private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
//            {
//                foreach (var role in roles)
//                {
//                    var roleClaim = new Claim(ClaimTypes.Role, role);
//                    claims.Add(roleClaim);
//                }
//            }


//            [HttpPost]
//            //[Route("AddGroup")]
//            public IActionResult AddGroup([FromBody] ApplicationGroup model)
//            {
//                var res = _iaplicationGroupService.Add(model);
//                return Ok(res);

//            }

//            [HttpPost]
//            //[Route("UpdateGroup")]
//            public IActionResult UpdateGroup([FromBody] ApplicationGroup model)
//            {
//                var res = _iaplicationGroupService.UpdateGroup(model);
//                return Ok(res);
//            }
//            [HttpGet]

//            //[Route("GetApplicationGroup")]
//            public IEnumerable<ApplicationGroup> GetApplicationGroup()
//            {

//                var res = _iaplicationGroupService.GetGroups();
//                return res;
//            }

//        [HttpGet]

//        public ApplicationGroup FindGroup(string id)
//            {

//                return _iaplicationGroupService.FindGroup(id);

//            }
//        //[Route("DeleteGroup")]
//        [HttpGet]

//        public IActionResult DeleteGroup(string id)
//            {

//                var res = _iaplicationGroupService.DeleteGroup(id);
//                return Ok(res);
//            }

//            [HttpPost]
//            public void AddRolesGroup(string[] roles, string groupId)
//            {

//                _iaplicationGroupService.AddRoleToGroup(roles, groupId);

//            }

//            [HttpPost]
//            public void RemoveRolesGroup(string roles, string groupId)
//            {
//                _iaplicationGroupService.DeleteRoleFromGroup(roles, groupId);
//            }

//            [HttpPost]
//            public void AddGroupToUser(string[] groups, string user)
//            {

//                _iaplicationGroupService.AddGroupToUser(groups, user);

//            }




//        }


//}
