using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
 
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IUnitOfWork _uow;
        private iesContext _iesContext;

        public EmailSenderService(IConfiguration config, UserManager<AspNetUser> userManage, IUnitOfWork unitOfWork, iesContext iesContext)
        {
            _config = config;
            _userManager = userManage;
            _uow = unitOfWork;
            _iesContext = iesContext;
        }
        public  ResponseDto SendEmail(string email)
        {
            try
            {
                string msg = "";
                var user = _uow.GetRepository<User>().Single(x => x.Email == email);
                if (user == null)
                {
                    msg = "Your Email Is Not Registerd";
                    return new ResponseDto { Status = 0, Message = msg };

                }
                string fromEmail = _config.GetSection("SmtpSettings").GetSection("SenderEmail").Value;
                string emailPass = _config.GetSection("SmtpSettings").GetSection("Password").Value;

                using (MailMessage mm = new MailMessage(fromEmail, email))
                {
                    string url = _config["AppUrl"] + "/api/ChangePassword/Index/"+ user.Id;
                    string html = "hi ";
                    html = "<table cellpadding = \"4\">" +
                                  "<tr>" +
                                      "<td><span>Dear " + user.Name + " ,</span></td>" +
                                  "</tr>" +
                                  "<tr>" +
                                      "<td><span>To reset your password, please go to the following page:</span></td>" +
                                  "</tr>" +
                                  "<tr>" +
                                      "<td><a href=" + url + ">Reset Your Password</a></td>" +
                                  "</tr>" +
                                  "<tr>" +
                                      "<td><span>Then login with your Email: <b>" + email + "</b></span></td>" +
                                  "</tr>" +
                                  "<tr>" +
                                      "<td>Thank You<br />IES Team" +
                                      "</td>" +
                                  "</tr>" +

                              "</table>";

                    mm.Subject = "IES - Change Your Password";
                    mm.Body = html;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Credentials = new NetworkCredential(fromEmail, emailPass);

                    //smtp.UseDefaultCredentials = true;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";

                    smtp.Send(mm);

                    mm.Dispose();
                    msg = "Ies Team Sent You Email. Please Check to Change Your Password ";
                    return new ResponseDto { Status =1, Message = msg };

                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetUserPassword(PasswordResetDto passwordResetDto)
        {
            try
            {
                if (passwordResetDto.Id==null)
                {
                    return false;
                }
                var user = _uow.GetRepository<User>().Single(x => x.Id == passwordResetDto.Id);
                if (user != null)
                {
                    if (passwordResetDto.NewPassword != null)
                    {
                        var cmd = $"UPDATE[User] SET ParentPassword=N'{passwordResetDto.NewPassword}' where Id={passwordResetDto.Id}";
                        _iesContext.Database.ExecuteSqlRaw(cmd);
                        //_iesContext.Dispose();
                    }
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}