using ERP_CMS.DB_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_CMS.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userID = getCurrentUserID();
            try
            {
                if (!IsPostBack)
                {
                    using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                    {

                        bool isAdmin = (bool)cntx.Users.Where(f => f.UserID == userID).Select(f => f.IsAdmin).FirstOrDefault();
                        if (!isAdmin)
                        {
                            bool haveManageUserRights = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ManageUsers));
                            bool haveManageRights = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ManageRights));
                            bool haveChangePasswordRights = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ChangePassword));
                            if (!haveManageUserRights)
                            {
                                divManageUsersDashboard.Visible = false;
                            }
                            if (!haveManageRights)
                            {
                                divManageRightsDashboard.Visible = false;
                            }
                            if (!haveChangePasswordRights)
                            {
                                divChangePasswordDashboard.Visible = false;
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {

                //lblStatus.Text = ex.Message;
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