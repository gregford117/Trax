using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

public class TraxUser {

    public int Id { get; set; }
    public string DisplayName { get; set; }
    public string LoginId { get; set; }
    public string EmailAddress { get; set; }
    public bool IsAdmin { get; set; }

    public string ErrorMessage { get; set; }

    public TraxUser() {
        this.SetUserDetails(HttpContext.Current.User.Identity.Name);

        if (!string.IsNullOrEmpty(this.ErrorMessage)) {
            ErrorLog.LogErrorDb(this.ErrorMessage);
        }
    }

    public TraxUser(string loginIdPassed) {
        this.SetUserDetails(loginIdPassed);

        if (!string.IsNullOrEmpty(this.ErrorMessage)) {
            ErrorLog.LogErrorDb(this.ErrorMessage);
        }
    }

    bool SetUserDetails(string loginIdToSearch) {

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Trax"].ConnectionString)) {
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from tb_Users where User_Login = @User_Login;", con);
            cmd.Parameters.Add("@User_Login", System.Data.SqlDbType.VarChar).Value = loginIdToSearch;
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows) {
                while (rdr.Read()) {
                    try {
                        this.Id = rdr.GetInt32(0);
                        this.DisplayName = rdr.GetString(1);
                        this.LoginId = rdr.GetString(2);
                        this.EmailAddress = rdr.GetString(3);
                        this.IsAdmin = rdr.GetBoolean(4);
                        this.ErrorMessage = string.Empty;

                        return true;
                    } catch (ArgumentNullException e) {
                        //returns false to indecate there was an issue
                        this.ErrorMessage = "NullArgumentException. ErrorMsg: " + e.Message;
                        return false;
                    } catch (Exception e) {
                        this.ErrorMessage = "Exception. ErrorMsg: " + e.Message;
                        return false;
                    }
                }
            } else {
                this.ErrorMessage = string.Format("User: {0} was not found in the user databse table", loginIdToSearch);
                return false;
            }                
        }
        this.ErrorMessage = "IF YOU ARE READING THIS FROM THE METHOD SOMETHING IS SERIOSULY WRONG WITH THE LOGIC";
        return false;       
    }
}