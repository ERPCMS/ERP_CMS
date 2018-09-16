using ERP_CMS.DB_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_CMS.Admin.Rights
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
                            bool haveRights = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ManageRights));
                            if (!haveRights)
                            {
                                Response.Redirect("~/Admin/");
                            }
                        }
                        GetUsers(cntx, userID);
                    }
                }
            }
            catch (Exception ex)
            {

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
        protected void GetUsers(ERP_CMSEntities cntx,int userID)
        {
            try
            {
                var obj = (from d in cntx.Users
                           where d.IsAdmin == false && d.UserID != userID
                           orderby d.UserName
                           select new { d.UserID, d.UserName, d.IsAdmin, d.IsActive }).ToList();



                if (obj.Count > 0)
                {
                    ddlUser.DataValueField = "UserID";
                    ddlUser.DataTextField = "UserName";
                    ddlUser.DataSource = obj;
                    ddlUser.DataBind();
                    ddlUser.Items.Insert(0, new ListItem("-- Select User --", ""));

                }

            }
            catch (Exception)
            {

                throw;
            }


        }
        protected void ddlUser_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (ddlUser.SelectedIndex == 0)
                {
                    lblStatus.Text = string.Empty;
                    pnlDetails.Visible = false;
                    return;
                }
                lblStatus.Text = string.Empty;
                pnlDetails.Visible = true;
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    GetRights(cntx);
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
        protected void GetRights(ERP_CMSEntities cntx)
        {
            var assigned = cntx.sp_GetUserAssignedRights(int.Parse(ddlUser.SelectedValue)).ToList();
            grdAssignedRights.DataSource = assigned;
            grdAssignedRights.DataBind();

            var nonassigned = cntx.sp_GetUserNonAssignedRights(int.Parse(ddlUser.SelectedValue)).ToList();
            grdNonAssignedRights.DataSource = nonassigned;
            grdNonAssignedRights.DataBind();
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                lblStatus.Text = "";
                lblMsg.Text = "";

                int intUserId = int.Parse(ddlUser.SelectedValue);
                int intMenuLinkID;

                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    foreach (GridViewRow row in grdNonAssignedRights.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chkLink");
                        if (chk.Checked == true)
                        {
                            intMenuLinkID = int.Parse(grdNonAssignedRights.DataKeys[row.RowIndex].Value.ToString());


                            var objUserLinks = new UserLink()
                            {
                                UserID = intUserId,
                                MenuLinkID = intMenuLinkID
                                //UserID = intUserId,
                                //objUserLinks.MenuLinkID = intMenuLinkID;
                            };
                            cntx.UserLinks.Add(objUserLinks);
                            cntx.SaveChanges();
                            // Dim rptInnerRights As Repeater = row.FindControl("rptInnerRights")
                            // For Each rptItem As RepeaterItem In rptInnerRights.Items
                            // Dim chkInner As CheckBox = rptItem.FindControl("chkInnerLink")
                            // If chkInner.Checked Then
                            // Dim objEmpRights As New PortalModel.EmpRights
                            // Dim intPageRightID As Integer = DirectCast(rptItem.FindControl("hdnPageRightID"), HiddenField).Value
                            // With objEmpRights
                            // .EmployeeID = intUserID
                            // .PageRightID = intPageRightID
                            // End With
                            // cntx.EmpRights.AddObject(objEmpRights)
                            // End If
                            // Next

                            GridView grdInnerRights = (GridView)row.FindControl("grdNonAssignedRightsInner");
                            foreach (GridViewRow rptItem in grdInnerRights.Rows)
                            {

                                try
                                {
                                    CheckBox chkInner = (CheckBox)rptItem.FindControl("chkInnerLink");
                                    int intPageRightID = int.Parse(((HiddenField)rptItem.FindControl("hdnPageRightID")).Value);
                                    if (chkInner.Checked)
                                    {
                                        var objUserRights = new UserRight();
                                        {
                                            //var withBlock = objEmpRights;
                                            objUserRights.UserID = intUserId;
                                            objUserRights.PageRightID = intPageRightID;
                                        }
                                        cntx.UserRights.Add(objUserRights);
                                    }
                                    else
                                    {
                                        var objExisting = cntx.UserRights.Where(f => f.UserID == intUserId && f.PageRightID == intPageRightID).ToList().FirstOrDefault();
                                        if (objExisting != null)
                                        {
                                            cntx.UserRights.Remove(objExisting);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                    }


                    foreach (GridViewRow rowParent in grdAssignedRights.Rows)
                    {
                        GridView grdInner = (GridView)rowParent.FindControl("grdAssignedRightsInner");
                        int MenuLinkID = int.Parse(((HiddenField)rowParent.FindControl("hdnMenuLinkID")).Value);

                        foreach (GridViewRow row in grdInner.Rows)
                        {
                            CheckBox chkInner = (CheckBox)row.FindControl("chkInnerLink");
                            try
                            {
                                int intPageRightID = int.Parse(((HiddenField)row.FindControl("hdnPageRightID")).Value);
                                var objExistingList = cntx.UserRights.Where(f => f.UserID == intUserId && f.PageRightID == intPageRightID).ToList();
                                foreach (var objExisting in objExistingList)
                                {
                                    cntx.UserRights.Remove(objExisting);
                                    cntx.SaveChanges();
                                }

                                if (chkInner.Checked)
                                {
                                    var objUserRights = new UserRight();
                                    {
                                        objUserRights.UserID = intUserId;
                                        objUserRights.PageRightID = intPageRightID;
                                    }
                                    cntx.UserRights.Add(objUserRights);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }

                    cntx.SaveChanges();
                    GetRights(cntx);
                    lblMsg.ForeColor = System.Drawing.Color.DarkGreen;
                    lblMsg.Text = "Rights updated successfully...";
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void grdAssignedRights_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                int intUserID = int.Parse(ddlUser.SelectedValue);
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Dim rptInnerRights As Repeater = e.Row.FindControl("rptInnerRights")
                        GridView grdInnerRights = (GridView)e.Row.FindControl("grdAssignedRightsInner");
                        int intMenuLinkID = int.Parse(((HiddenField)e.Row.FindControl("hdnMenuLinkID")).Value);
                        var obj = cntx.sp_GetPageRightsByUserAndMenuLinkID(intUserID, intMenuLinkID).ToList();
                        if (obj.Count > 0)
                        {
                            // rptInnerRights.DataSource = obj
                            // rptInnerRights.DataBind()
                            grdInnerRights.DataSource = obj;
                            grdInnerRights.DataBind();

                            foreach (GridViewRow row in grdInnerRights.Rows)
                            {
                                CheckBox chkInner = (CheckBox)row.FindControl("chkInnerLink");
                                Label lblMenuLinkTextInner = (Label)row.FindControl("lblMenuLinkTextInner");
                                int hdnEmpRightID = int.Parse(((HiddenField)row.FindControl("hdnEmpRightID")).Value);
                                if (hdnEmpRightID > 0)
                                    chkInner.Checked = true;
                                else
                                    chkInner.Checked = false;
                            }
                        }
                        else
                        {
                            // rptInnerRights.DataSource = Nothing
                            // rptInnerRights.DataBind()
                            grdInnerRights.DataSource = null;
                            grdInnerRights.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void grdAssignedRights_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            using (ERP_CMSEntities cntx = new ERP_CMSEntities())
            {
                int id = int.Parse(grdAssignedRights.DataKeys[e.RowIndex].Value.ToString());
                int intUserID = int.Parse(ddlUser.SelectedValue);

                lblStatus.Text = string.Empty;
                lblMsg.Text = string.Empty;

                try
                {
                    // Dim obj = New PortalModel.EmpLinks With {.EmpLinkID = id}
                    var obj = cntx.UserLinks.Where(f => f.UserLinkID == id).ToList().FirstOrDefault();
                    int intMenuLinkID = (int)obj.MenuLinkID;


                    cntx.UserLinks.Attach(obj);
                    cntx.UserLinks.Remove(obj);


                    var objInner = cntx.sp_GetPageRightsByUserAndMenuLinkID(intUserID, intMenuLinkID).Where(f => f.UserRightID > 0).ToList();
                    foreach (var o in objInner)
                    {
                        var objEmpRights = cntx.UserRights.Where(f => f.PageRightID == o.PageRightID).ToList().FirstOrDefault();
                        {
                            
                            cntx.UserRights.Attach(objEmpRights);
                            cntx.UserRights.Remove(objEmpRights);
                        }
                    }


                    cntx.SaveChanges();
                    GetRights(cntx);
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    lblStatus.Text = "Right Deleted Successfully.";
                    lblMsg.Text = "Right Deleted Successfully.";
                }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = ex.Message;
                }
            }
        }

        protected void grdNonAssignedRights_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Dim rptInnerRights As Repeater = e.Row.FindControl("rptInnerRights")
                        GridView grdInnerRights = (GridView)e.Row.FindControl("grdNonAssignedRightsInner");
                        int intMenuLinkID = int.Parse(grdNonAssignedRights.DataKeys[e.Row.RowIndex].Value.ToString());
                        var obj = cntx.PageRights.Where(f => f.MenuLinkID == intMenuLinkID).ToList();
                        if (obj.Count > 0)
                        {
                            // rptInnerRights.DataSource = obj
                            // rptInnerRights.DataBind()
                            grdInnerRights.DataSource = obj;
                            grdInnerRights.DataBind();


                            foreach (GridViewRow row in grdInnerRights.Rows)
                            {
                                Label lblMenuLinkTextInner = (Label) row.FindControl("lblMenuLinkTextInner");

                            }
                        }
                        else
                        {
                            // rptInnerRights.DataSource = Nothing
                            // rptInnerRights.DataBind()
                            grdInnerRights.DataSource = null;
                            grdInnerRights.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}