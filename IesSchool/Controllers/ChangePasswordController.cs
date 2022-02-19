using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IesSchool.Controllers
{

    [Route("api/[controller]/[action]")]
    public class ChangePasswordController : Controller
    {
        private IEmailSenderService _emailSenderService;
        public ChangePasswordController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }
        // GET: RestPasswordController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: RestPasswordController/Details/5
        [HttpPost]
        public ActionResult ResetUserPassword(PasswordResetDto passwordResetDto)
        {
            var all = _emailSenderService.ResetUserPassword(passwordResetDto);
            if (all ==true)
            {
               // TempData["name"] = "Bill";
                TempData["msg"] = "Password has been Changed, Try to LogIn ";
            }
            else
                TempData["msg"] = "Error, Try Again";

            return View("Index");

        }
    }
}
