using ERP_CMS.Common;
using ERP_CMS.DB_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_CMS.Admin.Users
{
    public partial class ManageUsers : System.Web.UI.Page
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
                        //bool haveAddUserRights = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.AddUser));
                        var havePageRights = cntx.sp_GetPageRightsByUserAndMenuLinkID(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ManageUsers)).Where(f => f.UserRightID > 0).ToList();
                        IList listRights = havePageRights.Select(f => f.Title).ToList();
                        if (havePageRights != null && havePageRights.Count > 0)
                        {
                            if (!listRights.Contains("Add User"))
                            {
                                Response.Redirect("~/Admin/");
                            }
                        }
                    }
                    
                }
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

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                bool UserCreated = false;
                try
                {
                    using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                    {
                        string password = EncryptionHelper.Encrypt("ERP_CMS2k18", txtPassword.Text);
                        var user = new User()
                        {
                            UserName = txtLoginName.Text,
                            Password = password,
                            IsActive = true,
                            IsAdmin = false,
                            IsDeleted = false
                        };
                        cntx.Users.Add(user);
                        cntx.SaveChanges();
                        UserCreated = true;
                    }


                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    lblStatus.Text = "User Created Successfully...";

                    txtLoginName.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtConfirmPassword.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = ex.Message;
                    if (UserCreated)
                    {
                        //delete that user
                    }

                }
            }

        }

    }
}