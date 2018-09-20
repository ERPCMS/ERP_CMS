using ERP_CMS.DB_Code;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_CMS.Admin.Setup.Shipper
{
    /// <summary>
    /// Summary description for Shipper_Handler
    /// </summary>
    public class Shipper_Handler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        int userID;
        public void ProcessRequest(HttpContext context)
        {
            userID = Convert.ToInt32(context.Session["userID"]);
            if (context.Request.QueryString["action"] != null)
            {
                int intOption = Convert.ToInt32(context.Request.QueryString["action"].ToString());
                switch (intOption)
                {
                    case 1:
                        {
                            getShipperList(context);
                            break;
                        }

                    case 2:
                        {
                            int intID = Convert.ToInt32(context.Request.QueryString["id"]);
                            editShipper(context, intID);
                            break;
                        }

                    case 3:
                        {
                            int intID = Convert.ToInt32(context.Request.QueryString["id"]);
                            addShipper(context, intID);
                            break;
                        }

                    case 4:
                        {
                            int intID = Convert.ToInt32(context.Request.QueryString["id"]);
                            deleteShipper(context, intID);
                            break;
                        }
                }
            }
        }
        private void getShipperList(HttpContext context)
        {
            try
            {
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    var obj = (from b in cntx.Shippers
                               where b.IsDeleted == false
                               orderby b.Shipper_Name
                               select new { b.ShipperID, b.Shipper_Name,b.Shipper_Address,b.Shipper_Email,b.Contact1,b.Contact2 }).ToList();

                    string JsonStr = JsonConvert.SerializeObject(new { shipperList = obj });
                    context.Response.ContentType = "application/json; charset=utf-8";
                    context.Response.Write(JsonStr);
                }
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                Newtonsoft.Json.Linq.JObject objErr = new Newtonsoft.Json.Linq.JObject();
                objErr.Add("Errors", ex.Message);
                context.Response.Write(objErr.ToString());
            }
        }

        private void editShipper(HttpContext context, int intID)
        {
            string output = "";
            try
            {
                var JsonStr = context.Request["models"];
                List<clsShipper> Shippers = JsonConvert.DeserializeObject<List<clsShipper>>(JsonStr);

                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    string shipperContact1 = Shippers[0].Contact1;
                    int shipperID = Convert.ToInt32(Shippers[0].ShipperID);
                    var obj = (from tbl in cntx.Shippers
                               where tbl.IsDeleted == false && tbl.ShipperID != shipperID && tbl.Contact1 == shipperContact1
                               select tbl).FirstOrDefault();

                    if (obj == null)
                    {
                        var objShipper = cntx.Shippers.Where(f => f.ShipperID == shipperID).ToList().FirstOrDefault();
                        // Dim objBrands = New PortalModel.Brands With {.BrandID = Brands(0).BrandID}
                        var objShipperOld = new ERP_CMSEntities().Shippers.Where(f => f.ShipperID == shipperID).ToList().FirstOrDefault();
                        cntx.Shippers.Attach(objShipper);
                        {
                            //var withBlock = objBrands;
                            objShipper.Shipper_Name = Shippers[0].Shipper_Name;
                            objShipper.Shipper_Address = Shippers[0].Shipper_Address;
                            objShipper.Shipper_Email = Shippers[0].Shipper_Email;
                            objShipper.Contact1 = Shippers[0].Contact1;
                            objShipper.Contact2 = Shippers[0].Contact2;
                        }
                        cntx.SaveChanges();

                        try
                        {
                            // PortalUtilities.fnCompare((object)objBrandsOld, (object)objBrands, objBrands.BrandID, lstEmployeeAndCompanyID(0));
                        }
                        catch (Exception ex)
                        {
                        }

                        context.Response.ContentType = "application/json; charset=utf-8";
                        context.Response.Write(JsonStr);
                    }
                    else
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";
                        Newtonsoft.Json.Linq.JObject objErr = new Newtonsoft.Json.Linq.JObject();
                        objErr.Add("Errors", "Shipper Already Exist!");
                        context.Response.Write(objErr.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                Newtonsoft.Json.Linq.JObject objErr = new Newtonsoft.Json.Linq.JObject();
                objErr.Add("Errors", ex.Message);
                context.Response.Write(objErr.ToString());
            }
        }
        private void addShipper(HttpContext context, int intID)
        {
            string output = "";
            try
            {
                var JsonStr = context.Request["models"];
                List<clsShipper> Shippers = JsonConvert.DeserializeObject<List<clsShipper>>(JsonStr);
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    string shipperContact1 = Shippers[0].Contact1;
                    var obj = (from tbl in cntx.Shippers
                               where tbl.IsDeleted == false && tbl.Contact1 == shipperContact1
                               select tbl).FirstOrDefault();

                    if (obj == null)
                    {
                        var objShipper = new ERP_CMS.DB_Code.Shipper()
                        {
                            Shipper_Name = Shippers[0].Shipper_Name,
                            Shipper_Address = Shippers[0].Shipper_Address,
                            Shipper_Email = Shippers[0].Shipper_Email,
                            Contact1 = Shippers[0].Contact1,
                            Contact2 = Shippers[0].Contact2,
                            IsDeleted = false,
                            InsertedDate = DateTime.Now,
                            InsertedBy = userID
                        };
                        cntx.Shippers.Add(objShipper);
                        cntx.SaveChanges();

                        context.Response.ContentType = "application/json; charset=utf-8";
                        context.Response.Write(JsonStr);
                    }
                    else
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";
                        Newtonsoft.Json.Linq.JObject objErr = new Newtonsoft.Json.Linq.JObject();
                        objErr.Add("Errors", "Shipper Already Exists!");
                        context.Response.Write(objErr.ToString());
                    }
                }
            }


            catch (Exception ex)
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                Newtonsoft.Json.Linq.JObject objErr = new Newtonsoft.Json.Linq.JObject();
                objErr.Add("Errors", ex.Message);
                context.Response.Write(objErr.ToString());
            }
        }
        private void deleteShipper(HttpContext context, int intID)
        {
            string output = "";
            try
            {
                var JsonStr = context.Request["models"];
                List<clsShipper> Shippers = JsonConvert.DeserializeObject<List<clsShipper>>(JsonStr);

                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    var objShipper = new ERP_CMS.DB_Code.Shipper()
                    {
                        ShipperID = Convert.ToInt32(Shippers[0].ShipperID)
                    };
                    //objAgentTypes.DeletedBy = userID;
                    // Dim objBrands = New PortalModel.Brands With {.BrandID = intID}
                    cntx.Shippers.Attach(objShipper);
                    objShipper.IsDeleted = true;
                    objShipper.DeletedBy = userID;
                    objShipper.DeletedDate = DateTime.Now;
                    // cntx.Brands.DeleteObject(objBrands)
                    cntx.SaveChanges();
                }


                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.Write(JsonStr);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                Newtonsoft.Json.Linq.JObject objErr = new Newtonsoft.Json.Linq.JObject();
                objErr.Add("Errors", ex.Message);
                context.Response.Write(objErr.ToString());
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class clsShipper
    {
        public int? ShipperID { get; set; }
        public string Shipper_Name { get; set; }
        public string Shipper_Address { get; set; }
        public string Shipper_Email { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }

    }
}
