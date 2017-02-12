using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

public static class ErrorLog {

    public static void LogErrorDb(string errorMessageForDB) {
        
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Trax"].ConnectionString)) {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into tb_Error_Log (Error_Log_TimeStamp, Error_Log_Message, Error_Log_User_Session) values (getdate(), @Error_Message, @Error_User)", con);
            cmd.Parameters.Add("@Error_Message", System.Data.SqlDbType.VarChar).Value = errorMessageForDB;
            cmd.Parameters.Add("@Error_User", System.Data.SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
            cmd.ExecuteNonQuery();
        }

        throw new Exception("An Error has been recorded and logged into the database for review.\nPlease contact a system Admin for assistance");        
    }
}