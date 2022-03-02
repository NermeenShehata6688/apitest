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
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IUnitOfWork _uow;
        private iesContext _iesContext;

        public EmailSenderService(IConfiguration config, UserManager<IdentityUser<int>> userManage, IUnitOfWork unitOfWork, iesContext iesContext)
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
                    string html = "";
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

                    // smtp.UseDefaultCredentials = true;
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

        #region NotNeededForNow
        //private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        //{
        //    // Get the unique identifier for this asynchronous operation.
        //    String token = (string)e.UserState;

        //    if (e.Cancelled)
        //    {
        //        Console.WriteLine("[{0}] Send canceled.", token);
        //    }
        //    if (e.Error != null)
        //    {
        //        Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
        //    }
        //    else
        //    {
        //        Console.WriteLine("Message sent.");
        //    }
        //    mailSent = true;
        //}
        //public async void SendEmail2(PasswordResetDto passwordResetDto)
        //{
        //    {
        //        var user = await _userManager.FindByEmailAsync(passwordResetDto.Email);
        //        if (user == null)
        //            return;
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //        var encodedToken = Encoding.UTF8.GetBytes(token);
        //        string validToken = WebEncoders.Base64UrlEncode(encodedToken);
        //        //string url = $"{_config["AppUrl"]}/ResetPassword?email={passwordResetDto.Email}$token={validToken}";

        //        SmtpClient client = new SmtpClient();

        //        MailAddress from = new MailAddress("nermeenshehata6688@gmail.com");
        //        // Set destinations for the email message.
        //        MailAddress to = new MailAddress(passwordResetDto.Email);
        //        // Specify the message content.
        //        MailMessage message = new MailMessage(from, to);
        //        message.Body = "This is a test email message sent by an application. ";
        //        // Include some non-ASCII characters in body and subject.
        //        string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
        //        message.Body += Environment.NewLine + someArrows;
        //        message.BodyEncoding = System.Text.Encoding.UTF8;
        //        message.Subject = "test message 1" + someArrows;
        //        message.SubjectEncoding = System.Text.Encoding.UTF8;
        //        // Set the method that is called back when the send operation ends.
        //        //  client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        //        // The userState can be any object that allows your callback
        //        // method to identify this send operation.
        //        // For this example, the userToken is a string constant.
        //        string userState = "test message1";

        //        client.Credentials = new NetworkCredential("nermeenshehata6688@gmail.com", "nora366nora");

        //        client.UseDefaultCredentials = true;
        //        client.Port = 587;
        //        client.EnableSsl = true;
        //        client.Host = "smtp.gmail.com";

        //        client.Send(message);

        //        //Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
        //        //string answer = Console.ReadLine();
        //        // If the user canceled the send, and mail hasn't been sent yet,
        //        //  then cancel the pending operation.
        //        //if (answer.StartsWith("c") && mailSent == false)
        //        //{
        //        //    client.SendAsyncCancel();
        //        //}
        //        // Clean up.
        //        message.Dispose();
        //        // message.sta
        //        // Console.WriteLine("Goodbye.");
        //    }
        //}
        //public async Task<UserDto> ResetPasswordAsync(PasswordResetDto passwordResetDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(passwordResetDto.Email);
        //    if (user == null)
        //        return null;

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    // Build the password reset link
        //    //var passwordResetLink = Url.Action("ResetPassword", "Account",
        //    //        new { email = model.Email, token = token }, Request.Scheme);

        //    //// Log the password reset link
        //    //logger.Log(LogLevel.Warning, passwordResetLink);

        //    //// Send the user to Forgot Password Confirmation view
        //    //return View("ForgotPasswordConfirmation");

        //    int x;

        //    var decodedToken = WebEncoders.Base64UrlDecode(passwordResetDto.Token);
        //    string normalToken = Encoding.UTF8.GetString(decodedToken);

        //    if (passwordResetDto.NewPassword != passwordResetDto.ConfirmPassword)
        //        return null;

        //    var result = await _userManager.ResetPasswordAsync(user, normalToken, passwordResetDto.NewPassword);

        //    if (result.Succeeded)
        //        return null;

        //    return null;
        //}

        //public async Task<UserDto> ForgetPasswordAsync(PasswordResetDto passwordResetDto)
        //{
        //    var user = await _userManager.FindByEmailAsync(passwordResetDto.Email);
        //    if (user == null)
        //        return null;

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //    var encodedToken = Encoding.UTF8.GetBytes(token);
        //    string validToken = WebEncoders.Base64UrlEncode(encodedToken);

        //    string url = $"{_config["AppUrl"]}/ResetPassword?email={passwordResetDto.Email}$token={validToken}";

        //    await _




        //    if (passwordResetDto.NewPassword != passwordResetDto.ConfirmPassword)
        //        return null;

        //    var result = await _userManager.ResetPasswordAsync(user, normalToken, passwordResetDto.NewPassword);

        //    if (result.Succeeded)
        //        return null;

        //    return null;
        //}
        //public async Task SendEmailAsync(string toEmail, string subject, string content)
        //{
        //    var apiKey = _configuration["SendGridAPIKey"];
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("test@authdemo.com", "JWT Auth Demo");
        //    var to = new EmailAddress(toEmail);
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        //    var response = await client.SendEmailAsync(msg);
        //}
        #endregion


    }
}