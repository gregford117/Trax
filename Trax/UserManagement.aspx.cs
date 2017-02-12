using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserManagement : System.Web.UI.Page {

    protected void Page_Init(object sender, EventArgs e) {
        DS_Bound_Companies.SelectParameters["UserId"].DefaultValue = (Session["TraxUser"] as TraxUser) != null ? (Session["TraxUser"] as TraxUser).Id.ToString() : "0";
        DS_UnBound_Companies.SelectParameters["UserId"].DefaultValue = (Session["TraxUser"] as TraxUser) != null ? (Session["TraxUser"] as TraxUser).Id.ToString() : "0";
    }

    protected void Page_Load(object sender, EventArgs e) {
                
    }
}