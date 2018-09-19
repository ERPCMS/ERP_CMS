using ERP_CMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;

namespace ERP_CMS.DB_Code
{
    public class PortalUtilities
    {
        public static bool fnHaveRights(int intUserID, int intMenuLink)
        {
            bool haveRights = false;
            using (ERP_CMSEntities cntx = new ERP_CMSEntities())
            {
                var objUserLinks = cntx.UserLinks.Where(f => f.UserID == intUserID && f.MenuLinkID == intMenuLink).ToList().FirstOrDefault();
                if (objUserLinks != null)
                {
                    haveRights = true;
                }
            }
            return haveRights;
        }



        public static string resetPassword(ERP_CMSEntities cntx, int UserID)
        {
            var usr = cntx.Users.Find(UserID);
            string UserName = usr.UserName;
            string newPassword = EncryptionHelper.Encrypt("ERP_CMS2k18", UserName + "@12345");
            usr.Password = newPassword;
            cntx.SaveChanges();
            return newPassword;
        }
        public static bool isUserAdmin(ERP_CMSEntities cntx, int userID)
        {
           // bool isAdmin = false;
            bool isAdmin = (bool)(from d in cntx.Users
                            where d.UserID == userID
                            select d.IsAdmin).FirstOrDefault();
            //isAdmin = Convert.ToBoolean(chkAdmin);
            return isAdmin;
        }
        public enum MenuLinks
        {
            ManageAgentType = 1,
            ChangePassword = 4,
            Profile = 39,
            AddUser = 10,
            ListUsers = 11,
            ManageRights = 3,
            ManageUsers = 2,


        }

    }
}