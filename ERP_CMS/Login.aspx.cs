using ERP_CMS.DB_Code;
using ERP_CMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_CMS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                string key = "ERP_CMS2k18";
                //string encryptedUserName = EncryptionHelper.Encrypt(key, userName);
                string encryptedPassword = EncryptionHelper.Encrypt(key, password);
                ERP_CMSEntities db = new ERP_CMSEntities();
                var obj = (from c in db.Users
                           where c.UserName == userName && c.Password == encryptedPassword && c.IsDeleted == false
                           select new { c.UserID, c.IsActive }).FirstOrDefault();
                if (obj != null)
                {
                    if (obj.UserID != 0)
                    {
                        if (obj.IsActive == false)
                        {
                            ErrorMessage.Visible = true;
                            FailureText.Text = "This User is Blocked";
                            return;
                        }
                        Session["userID"] = obj.UserID;
                        ErrorMessage.Visible = false;
                        Response.Redirect("~/Admin/Default.aspx");
                    }

                   
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Invalid User Name or Password";
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}