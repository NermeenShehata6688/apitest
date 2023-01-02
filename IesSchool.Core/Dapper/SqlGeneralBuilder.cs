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