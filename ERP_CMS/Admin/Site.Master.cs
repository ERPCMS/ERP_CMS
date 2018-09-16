using ERP_CMS.DB_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP_CMS
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Session["isAdmin"] as string))
            //{
            //    string isAdmin = Session["isAdmin"].ToString();
            //    if (!isAdmin.Equals("True"))
            //    {
            //        Response.Redirect("~/Account/Login.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("~/Account/Login.aspx");
            //}

            if (!IsPostBack)
            {
                Page.Header.DataBind();

                int userID = getCurrentUserID();
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    bool isAdmin = PortalUtilities.isUserAdmin(cntx, userID);
                    if (isAdmin)
                    {
                        var objPanels = (from p in cntx.MenuPanels
                                         orderby p.DisplayOrder
                                         select new { p.PanelID, p.PanelTitle }).ToList();
                        rptrMenuPanels.DataSource = objPanels;
                        rptrMenuPanels.DataBind();
                    }
                    else
                    {
                        var objPanels = (from el in cntx.UserLinks
                                         join usr in cntx.Users on el.UserID equals usr.UserID
                                         join ml in cntx.MenuLinks on el.MenuLinkID equals ml.MenuLinkID
                                         join m in cntx.Menus on ml.MenuID equals m.MenuID
                                         join mp in cntx.MenuPanels on m.PanelID equals mp.PanelID
                                         where usr.UserID == userID
                                         select new { mp.PanelID, mp.PanelTitle, mp.DisplayOrder }).Distinct().OrderBy(f => f.DisplayOrder).ToList();
                        rptrMenuPanels.DataSource = objPanels;
                        rptrMenuPanels.DataBind();
                    }

                    bool haveChangePassword = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.ChangePassword));
                    if (haveChangePassword)
                    {
                        sideMenuLinkPassword.Visible = true;
                        sideMenuDivider.Visible = true;
                    }

                    var objUserRights = cntx.sp_GetPageRightsByUserAndMenuLinkID(userID, Convert.ToInt32(PortalUtilities.MenuLinks.Profile)).Where(f => f.UserRightID > 0).ToList();

                    IList lstRights = objUserRights.Select(f => f.Title).ToList();

                    bool haveProfile = PortalUtilities.fnHaveRights(userID, Convert.ToInt32(PortalUtilities.MenuLinks.Profile));
                    if (objUserRights.Count > 0 && haveProfile == true && (lstRights.Contains("View Self") || lstRights.Contains("Edit Self")))
                    {
                        sideMenuLinkProfile.Visible = true;
                        sideMenuDivider.Visible = true;
                    }

                    try
                    {
                        var objUser = cntx.Users.Where(f => f.UserID == userID).ToList().FirstOrDefault();
                        lblLnkSideProfile.Text = objUser.UserName;
                    }
                    catch (Exception)
                    {

                        throw;
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

        

        protected void rptrMenuPanels_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int userID = getCurrentUserID();
                int intID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "PanelID"));
                Repeater rptrMenus = (Repeater)e.Item.FindControl("rptrMenus");
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    bool isAdmin = PortalUtilities.isUserAdmin(cntx, userID);
                    if (isAdmin)
                    {
                        Label lblID = (Label)e.Item.FindControl("lblID");
                        int panelID = Convert.ToInt32(lblID.Text.ToString());
                        var objMenus = (from m in cntx.Menus
                                        where m.PanelID == panelID
                                        orderby m.DisplayOrder
                                        select new { m.MenuID, m.MenuTitle }).ToList();

                        rptrMenus.DataSource = objMenus;
                        rptrMenus.DataBind();
                    }
                    else
                    {
                        var objMenus = (from el in cntx.UserLinks
                                        join usr in cntx.Users on el.UserID equals usr.UserID
                                        join ml in cntx.MenuLinks on el.MenuLinkID equals ml.MenuLinkID
                                        join m in cntx.Menus on ml.MenuID equals m.MenuID
                                        where usr.UserID == userID & m.PanelID == intID
                                        select new { m.MenuID, m.MenuTitle, m.DisplayOrder }).Distinct().OrderBy(f => f.DisplayOrder).ToList();
                        rptrMenus.DataSource = objMenus;
                        rptrMenus.DataBind();
                    }
                }
            }
        }
        protected void rptrMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int userID = getCurrentUserID();
                int intID = (int)DataBinder.Eval(e.Item.DataItem, "MenuID");
                Repeater rptrMenuLinks = (Repeater)e.Item.FindControl("rptrMenuLinks");
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    bool isAdmin = PortalUtilities.isUserAdmin(cntx, userID);
                    if (isAdmin)
                    {
                        Label lblID = (Label)e.Item.FindControl("lblID");
                        int menuID = Convert.ToInt32(lblID.Text.ToString());
                        var objMenuLinks = (from ml in cntx.MenuLinks
                                            where ml.MenuID == menuID
                                            orderby ml.DisplayOrder
                                            select new { ml.MenuLinkText, ml.PageURL }).ToList();

                        rptrMenuLinks.DataSource = objMenuLinks;
                        rptrMenuLinks.DataBind();
                    }
                    else
                    {
                        var objMenuLinks = (from el in cntx.UserLinks
                                            join usr in cntx.Users on el.UserID equals usr.UserID
                                            join ml in cntx.MenuLinks on el.MenuLinkID equals ml.MenuLinkID
                                            join m in cntx.Menus on ml.MenuID equals m.MenuID
                                            where usr.UserID == userID & m.MenuID == intID
                                            select new { ml.MenuLinkText, ml.PageURL, ml.DisplayOrder }).Distinct().OrderBy(f => f.DisplayOrder).ToList();
                        rptrMenuLinks.DataSource = objMenuLinks;
                        rptrMenuLinks.DataBind();
                    }
                }

            }
        }
    }
}

