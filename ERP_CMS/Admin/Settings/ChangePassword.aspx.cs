using ERP_CMS.Common;
using ERP_CMS.DB_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_CMS.Admin.Settings
{
    public partial class ChangePassword : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int userID = getCurrentUserID();
            if (!IsPostBack)
            {
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    bool isAdmin = (bool)cntx.Users.Where(f => f.UserID == userID).Select(f => f.IsAdmin).FirstOrDefault();
                    if (!isAdmin)
                    {
                        bool haveRights = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ChangePassword));
                        if (!haveRights)
                        {
                            Response.Redirect("~/Admin/");
                        }
                    }
                    var user = cntx.Users.Where(f => f.UserID == userID).Select(f => f.UserName).FirstOrDefault();
                    if (user == null && user.Equals(null))
                    {
                        lblUserName.Text = "-- Invalid User --";
                        btnChangePassword.Enabled = false;
                        txtPassword.Enabled = false;
                        txtConfirmPassword.Enabled = false;
                    }
                    else
                    {
                        lblUserName.Text = user;
                    //txtEmailAddress.Text = user.Email
                    //lblUserRole.Text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Roles.GetRolesForUser(strUserName)(0))
                    //strRole = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Roles.GetRolesForUser(strUserName)(0))

                    //Dim objEmployee = cntx.Employees.Where(Function(f) f.EmployeeID = intEmpID).ToList().FirstOrDefault()
                    //txtEmailAddress.Text = objEmployee.Email
                    }
                }
            }
        }
        protected void btnChangePassword_Click(object sender, System.EventArgs e)
        {
            try
            {
                using(ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    int userID = getCurrentUserID();
                    var user = cntx.Users.Find(userID);
                    string resetPassword = EncryptionHelper.Encrypt("ERP_CMS2k18", txtPassword.Text);
                    user.Password = resetPassword;
                    cntx.SaveChanges();

                    lblStatus.Text = "Password changed successfully";
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                }
                
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = ex.Message;
            }
        }

        public int getCurrentUserID()
        {
            int userID = 0;
            if (Session["userID"] != null || !string.IsNullOrEmpty(Session["userID"] as string))
            {
                userID = Convert.ToInt32(Session["userID"]);
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            return userID;
        }
    }
}