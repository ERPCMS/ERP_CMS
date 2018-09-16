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
    public partial class ListUsers : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            int userID = getCurrentUserID();
            if (!IsPostBack)
            {
                //int userID = getCurrentUserID();
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    
                    bool isAdmin = (bool)cntx.Users.Where(f => f.UserID == userID).Select(f => f.IsAdmin).FirstOrDefault();
                    if (!isAdmin)
                    {
                        var havePageRights = cntx.sp_GetPageRightsByUserAndMenuLinkID(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ManageUsers)).Where(f => f.UserRightID > 0).ToList();
                        IList listRights = havePageRights.Select(f => f.Title).ToList();
                        if (havePageRights != null && havePageRights.Count > 0)
                        {
                            if (!listRights.Contains("List User"))
                            {
                                Response.Redirect("~/Admin/");
                            }
                            if (!listRights.Contains("Add User"))
                            {
                                pnlAdd.Visible = false;
                            }

                        }
                    }

                    try
                    {

                        var obj = (from d in cntx.Users
                                   orderby d.UserName
                                   select new { d.UserID, d.UserName, d.IsAdmin, d.IsActive }).ToList();
                        ddlUser.DataValueField = "UserID";
                        ddlUser.DataTextField = "UserName";
                        ddlUser.DataSource = obj;
                        ddlUser.DataBind();
                        ddlUser.Items.Insert(0, new ListItem("-- Select User --", ""));


                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = ex.Message;

                    }
                    GetUsers(cntx, 0);
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

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                lblStatus.Text = string.Empty;
                pnlDetails.Visible = true;
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    if (ddlUser.SelectedIndex == 0)
                    {
                        GetUsers(cntx, 0);
                        return;
                    }
                    int UserID = int.Parse(ddlUser.SelectedValue.ToString());
                        GetUsers(cntx, UserID);
                }

            }
            catch (Exception ex)
            {
                pnlDetails.Visible = false;
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = ex.Message;
                if (ex.InnerException != null)
                {
                    lblStatus.Text = lblStatus.Text + "<br />" + ex.InnerException.Message;
                }

            }

        }

        protected void GetUsers(ERP_CMSEntities cntx, int UserID)
        {
            var obj = (from d in cntx.Users
                       orderby d.UserName
                       select new { d.UserID, d.UserName, d.IsAdmin, d.IsActive }).ToList();

            if (UserID > 0)
            {
                obj = obj.Where(f => f.UserID == UserID).ToList();
            }

            grd.DataSource = obj;
            grd.DataBind();
            if (obj.Count > 0)
            {
                grd.DataSource = obj;
                grd.DataBind();
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "0 records found in selected user!";
            }

        }
        protected void grd_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "chngStatus")
            {
                try
                {
                    int RowIndex = int.Parse(e.CommandArgument.ToString());
                    int UserID = int.Parse(grd.DataKeys[RowIndex]["UserID"].ToString());
                    string UserName = grd.Rows[RowIndex].Cells[0].Text;
                    using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                    {
                        //var usr = cntx.Users.Where(f => f.UserName == UserName).Select(f => f.UserName).FirstOrDefault();
                        var usr = cntx.Users.Find(UserID);

                        if (UserName == "admin")
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            lblStatus.Text = "admin user can not be blocked.";
                        }
                        else
                        {
                            usr.IsActive = !usr.IsActive;
                            cntx.SaveChanges();
                            lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                            lblStatus.Text = "User status has been updated.";

                            GetUsers(cntx, UserID);

                        }

                    }
                    //MembershipUser usr = Membership.GetUser(UserName);

                }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = ex.Message;
                }
            }
            else if (e.CommandName == "chngPwd")
            {
                try
                {
                    int RowIndex = int.Parse(e.CommandArgument.ToString());
                    int UserID = int.Parse(grd.DataKeys[RowIndex]["UserID"].ToString());
                    string UserName = grd.Rows[RowIndex].Cells[0].Text;
                    //MembershipUser usr = Membership.GetUser(UserName);
                    using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                    {
                        //var usr = cntx.Users.Where(f => f.UserName == UserName).Select(f => f.UserName).FirstOrDefault();
                        string newPassword = PortalUtilities.resetPassword(cntx, UserID);
                    }


                    lblStatus.Text = "Password updated with: " + UserName + "@12345";
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = ex.Message;
                }
            }
        }

        protected void grd_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string UserName = e.Row.Cells[0].Text;
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    bool usr = (bool)(from c in cntx.Users
                                      where c.UserName == UserName
                                      select c.IsActive).FirstOrDefault();

                    //var usr = cntx.Users.Where(f => f.UserName == UserName).Select(f => f.IsActive).FirstOrDefault();
                    //var usr = cntx.Users.Find(UserName);
                    Image imgStatus = (Image)e.Row.FindControl("imgStatus");

                    if (usr)
                        imgStatus.ImageUrl = "~/Admin/images/icoactive.png";
                    else
                        imgStatus.ImageUrl = "~/Admin/images/icoblocked.png";
                }

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Users/Default.aspx");
        }
    }
}