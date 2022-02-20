﻿using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public  interface IEmailSenderService
    {
        public void SendEmail(PasswordResetDto passwordResetDto);
        public bool ResetUserPassword(PasswordResetDto passwordResetDto);
    }
}