using ERP_CMS.DB_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_CMS.Admin.Setup.AgentType
{
    public partial class AgentType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userID = getCurrentUserID();
            if (!IsPostBack)
            {
                try
                {
                    using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                    {

                        bool isAdmin = (bool)cntx.Users.Where(f => f.UserID == userID).Select(f => f.IsAdmin).FirstOrDefault();
                        if (!isAdmin)
                        {
                            bool haveRights = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ManageAgentType));
                            if (!haveRights)
                            {
                                Response.Redirect("~/Admin/");
                            }
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }
                //HttpContext.Current("") = userID;
                //Server.Transfer("TreeLoader.ashx");
                // Server.Transfer("AgentType_Handler.ashx?currentUser=" + userID);
                //Loader = "AgentType_Handler.ashx?currentUser=" + userID;
                //Response.Redirect("AgentType_Handler.ashx?currentUser=" + userID);
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