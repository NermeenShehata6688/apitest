using Olsys.Business.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Olsys.Business.Data
{
    public static class SqlGeneralBuilder
    {
        #region IepHelper
        public static string Select_All_Department()
        {
            string sql = @"SELECT Department.* FROM Department ";
            return sql;
        }

        public static string Select_All_Students()
        {
            string sql = @"SELECT Vw_Students.* FROM Vw_Students ";
            return sql;
        }
        public static string Select_All_AcadimicYears()
        {
            string sql = @"SELECT AcadmicYears.* FROM AcadmicYears ";
            return sql;
        }
        public static string Select_AllTerms()
        {
            string sql = @"SELECT Term.* FROM Term ";
            return sql;
        }
        public static string Select_AllUsers()
        {
            string sql = @"SELECT [User] .* FROM  [User] where IsTeacher =1";
            return sql;
        }
        public static string Select_AllAssistant()
        {
            string sql = @"SELECT Assistant .* FROM  Assistant";
            return sql;
        }
        public static string Select_AllHeadOfEducation()
        {
            string sql = @"SELECT [User] .* FROM  [User] where IsHeadofEducation  =1";
            return sql;
        }
        public static string Select_AllUserAssistant()
        {
            string sql = @"SELECT User_Assistant .* FROM  User_Assistant
                           LEFT OUTER Join Assistant on Assistant.Id = User_Assistant.AssistantId";
            return sql;
        }
        public static string Select_Setting()
        {
            string sql = @"SELECT Setting .* FROM  Setting";
            return sql;
        }
        #endregion

        #region IepHelper2
        public static string Select_All_Programs()
        {
            string sql = @"SELECT Program .* FROM  Program  ";
            return sql;
        }
        public static string Select_All_Areas()
        {
            string sql = @"SELECT Area .* FROM  Area ";
            return sql;
        }
        public static string Select_All_Strands()
        {
            string sql = @"SELECT Strand .* FROM  Strand ";
            return sql;
        }
        public static string Select_All_ParamedicalServices()
        {
            string sql = @"SELECT ParamedicalService .* FROM  ParamedicalService ";
            return sql;
        }

        public static string Select_All_ExtraCurriculars()
        {
            string sql = @"SELECT ExtraCurricular .* FROM  ExtraCurricular ";
            return sql;
        }

        public static string Select_All_SkillEvaluations()
        {
            string sql = @"SELECT SkillEvaluation .* FROM  SkillEvaluation ";
            return sql;
        }

        public static string Select_AllTherapists()
        {
            string sql = @"SELECT [User] .* FROM  [User] where IsTherapist  =1";
            return sql;
        }

        public static string Select_AllTeachers()
        {
            string sql = @"SELECT [User] .* FROM  [User] where IsTeacher  =1";
            return sql;
        }
        public static string Select_AllExtraCurricularsTeacher()
        {
            string sql = @"SELECT [User] .* FROM  [User] where IsExtraCurricular  =1";
            return sql;
        }
        public static string Select_All_TherapistParamedicalServices()
        {
            string sql = @"SELECT TherapistParamedicalService .* FROM  TherapistParamedicalService ";
            return sql;
        }

        public static string Select_All_UserExtraCurriculars()
        {
            string sql = @"SELECT User_ExtraCurricular .* FROM  User_ExtraCurricular ";
            return sql;
        }
        public static string Select_All_StudentTherapists() 
        {
            string sql = @"SELECT Student_Therapist .* FROM  Student_Therapist ";
            return sql;
        }
        public static string Select_All_StudentExtraTeachers()
        {
            string sql = @"SELECT Student_ExtraTeacher .* FROM  Student_ExtraTeacher ";
            return sql;
        }
        #endregion
        #region ItpHelper
       
        #endregion

        #region ShoppingDetails
        public static string Insert_ShoppingDetails()
        {
            string sql = @"
             INSERT INTO ShoppingDetails  
             ( ShoppingDate,   
               Item_ID,   
               Description,   
               Qty,   
               Price,   
               Amount,   
               CustomerId,   
               UserName,   
               MyFatoorah_InvoiceId,   
               MyFatoorah_PaymentId,   
               MyFatoorah_URL,   
               EntryUserId,   
               EntryDate )  
      VALUES ( @ShoppingDate,   
               @Item_ID,   
               @Description,   
               @Qty,   
               @Price,   
               @Amount,   
               @CustomerId,   
               @UserName,   
               @MyFatoorah_InvoiceId,   
               @MyFatoorah_PaymentId,   
               @MyFatoorah_URL,   
               @EntryUserId,   
               @EntryDate) SELECT CAST(SCOPE_IDENTITY() as int) ;";
            return sql;
        }
        public static string Update_ShoppingDetails()
        {
            string sql = @"UPDATE ShoppingDetails  
            SET   
            ShoppingDate = @ShoppingDate,   
            Item_ID = @Item_ID,   
            Description = @Description,   
            Qty = @Qty,   
            Price = @Price,   
            Amount = @Amount,   
            CustomerId = @CustomerId,   
            UserName = @UserName,   
            MyFatoorah_InvoiceId = @MyFatoorah_InvoiceId,   
            MyFatoorah_PaymentId = @MyFatoorah_PaymentId,   
            MyFatoorah_URL = @MyFatoorah_URL,     
            EditUserId = @EditUserId,   
            EditDate = @EditDate
            WHERE Id = @Id;";
            return sql;
        }
        public static string MyfatoorahLink_ShoppingDetails()
        {
            string sql = @"UPDATE ShoppingDetails  SET MyFatoorah_InvoiceId = @MyFatoorah_InvoiceId,
                MyFatoorah_PaymentId = @MyFatoorah_PaymentId,   MyFatoorah_URL = @MyFatoorah_URL  
                WHERE (ShoppingDetails.Id =@TransId ) AND (ShoppingDetails.Item_ID =@TransDetailId);";
            return sql;
        }

        public static string Select_MyFatoorahWithoutReceipt_Cart()
        {
            string sql = @"SELECT Distinct  
            ShoppingDetails.CustomerId AS CustomerId, 
            ShoppingDetails.MyFatoorah_InvoiceId as MyFatoorah_InvoiceId,
            ShoppingDetails.MyFatoorah_PaymentId as MyFatoorah_PaymentId,
            ShoppingDetails.MyFatoorah_URL as MyFatoorah_URL
            FROM ShoppingDetails
            WHERE (ShoppingDetails.MyFatoorah_URL is not null) And 
            (ShoppingDetails.UserName=@UserName) And
             ShoppingDetails.MyFatoorah_URL Not in 
            (Select SLS_TR_SaleInvoices.MyFatoorah_URL From SLS_TR_SaleInvoices where SLS_TR_SaleInvoices.MyFatoorah_URL is not null and SLS_TR_SaleInvoices.CustomerId=ShoppingDetails.CustomerId)";
            return sql;
        }
        #endregion
    }
}