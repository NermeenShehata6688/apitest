using IesSchool.Core.Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Olsys.Business.Data
{
    public static class ConnectionManager
    {

        //Local
        public static IDbConnection GetConnection()
        {
            return SqlHelper.GetConnection();
    }
        public static IDbConnection OpenConnection()
        {
            var dbConnection = GetConnection();
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }
        public static IDbConnection OpenConnection(IDbConnection dbConnection)
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }
        public static void CloseConnection(IDbConnection dbConnection)
        {
            if (dbConnection.State == ConnectionState.Open || dbConnection.State == ConnectionState.Broken)
            {
                dbConnection.Close();
            }
        }
    }
}
