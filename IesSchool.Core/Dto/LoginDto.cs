using IesSchool.Context.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IesSchool.Core.Dto.MembershipDto;

namespace IesSchool.Core.Dto
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }


    public class RegisterModel

    {
        [Required]
        public int Id { get; set; } = 0;
  
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        //[EmailAddress]
        //[Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string FullName { get; set; }
        //public string Id { get; set; }
        //public string []  Roles{ get; set; }
        public IEnumerable<ApplicationGroupDto> Roles { get; set; }
    }
    public class UpdateResources
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }
        public IEnumerable<ApplicationGroup> Roles { get; set; }
    }


    public class RegisterDto
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Please enter at least 6 characters", MinimumLength = 6)]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
