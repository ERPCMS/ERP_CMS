using ERP_CMS.DB_Code;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_CMS.Admin.Setup.AgentType
{
    /// <summary>
    /// Summary description for AgentType_Handler
    /// </summary>
    public class AgentType_Handler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        int userID;
        public void ProcessRequest(HttpContext context)
        {

            userID = getCurrentUserID(context);
            //if (context.Request.QueryString["currentUser"] != null)
            //{
            //    userID = Convert.ToInt32(context.Request.QueryString["currentUser"].ToString());
            //}
            if (context.Request.QueryString["action"] != null)
            {
                int intOption = Convert.ToInt32(context.Request.QueryString["action"].ToString());
                switch (intOption)
                {
                    case 1:
                        {
                            getAgentTypeList(context);
                            break;
                        }

                    case 2:
                        {
                            int intID = Convert.ToInt32(context.Request.QueryString["id"]);
                            editAgentType(context, intID);
                            break;
                        }

                    case 3:
                        {
                            int intID = Convert.ToInt32(context.Request.QueryString["id"]);
                            addAgentType(context, intID);
                            break;
                        }

                    case 4:
                        {
                            int intID = Convert.ToInt32(context.Request.QueryString["id"]);
                            deleteAgentType(context, intID);
                            break;
                        }
                }
            }
        }
        public int getCurrentUserID(HttpContext context)
        {
            int userID = 0;
            if (context.Session["userID"] != null)
            {
                userID = Convert.ToInt32(context.Session["userID"]);
            }
            else
            {
                context.Response.Redirect("~/Login.aspx");
            }
            return userID;
        }
        private void getAgentTypeList(HttpContext context)
        {
            try
            {
                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    var obj = (from b in cntx.AgentTypes
                               where b.IsDeleted == false
                               orderby b.AgentType1
                               select new { b.AgentTypeID, b.AgentType1 }).ToList();

                    string JsonStr = JsonConvert.SerializeObject(new { agentTypeList = obj });
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

        private void editAgentType(HttpContext context, int intID)
        {
            string output = "";
            try
            {
                var JsonStr = context.Request["models"];
                List<clsAgentType> AgentTypes = JsonConvert.DeserializeObject<List<clsAgentType>>(JsonStr);

                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    string strName = AgentTypes[0].AgentType1.Trim().ToLower();
                    int agentTypeID = Convert.ToInt32(AgentTypes[0].AgentTypeID);
                    var obj = (from tbl in cntx.AgentTypes
                               where tbl.IsDeleted == false && tbl.AgentTypeID != agentTypeID && tbl.AgentType1.Trim().ToLower() == strName
                               select tbl).FirstOrDefault();

                    if (obj == null)
                    {
                        var objAgentTypes = cntx.AgentTypes.Where(f => f.AgentTypeID == agentTypeID).ToList().FirstOrDefault();
                        // Dim objBrands = New PortalModel.Brands With {.BrandID = Brands(0).BrandID}
                        var objAgentTypesOld = new ERP_CMSEntities().AgentTypes.Where(f => f.AgentTypeID == agentTypeID).ToList().FirstOrDefault();
                        cntx.AgentTypes.Attach(objAgentTypes);
                        {
                            //var withBlock = objBrands;
                            objAgentTypes.AgentType1 = AgentTypes[0].AgentType1;
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
                        objErr.Add("Errors", "Agent Type Already Exist!");
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

        private void addAgentType(HttpContext context, int intID)
        {
            string output = "";
            try
            {
                var JsonStr = context.Request["models"];
                List<clsAgentType> AgentTypes = JsonConvert.DeserializeObject<List<clsAgentType>>(JsonStr);

                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    string strName = AgentTypes[0].AgentType1.Trim().ToLower();
                    var obj = (from tbl in cntx.AgentTypes
                               where tbl.IsDeleted == false && tbl.AgentType1.Trim().ToLower() == strName
                               select tbl).FirstOrDefault();

                    if (obj == null)
                    {
                        var objAgentType = new ERP_CMS.DB_Code.AgentType()
                        {
                            AgentType1 = AgentTypes[0].AgentType1,
                            IsDeleted = false,
                            InsertedDate = DateTime.Now,
                            InsertedBy = userID
                        };
                        cntx.AgentTypes.Add(objAgentType);
                        cntx.SaveChanges();

                        context.Response.ContentType = "application/json; charset=utf-8";
                        context.Response.Write(JsonStr);
                    }
                    else
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";
                        Newtonsoft.Json.Linq.JObject objErr = new Newtonsoft.Json.Linq.JObject();
                        objErr.Add("Errors", "Agent Type Already Exists!");
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

        private void deleteAgentType(HttpContext context, int intID)
        {
            string output = "";
            try
            {
                var JsonStr = context.Request["models"];
                List<clsAgentType> AgentTypes = JsonConvert.DeserializeObject<List<clsAgentType>>(JsonStr);

                using (ERP_CMSEntities cntx = new ERP_CMSEntities())
                {
                    var objAgentTypes = new ERP_CMS.DB_Code.AgentType()
                    {
                        AgentTypeID = Convert.ToInt32(AgentTypes[0].AgentTypeID)
                    };
                    //objAgentTypes.DeletedBy = userID;
                    // Dim objBrands = New PortalModel.Brands With {.BrandID = intID}
                    cntx.AgentTypes.Attach(objAgentTypes);
                    objAgentTypes.IsDeleted = true;
                    objAgentTypes.DeletedBy = userID;
                    objAgentTypes.DeletedDate = DateTime.Now;
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

    public class clsAgentType
    {
        public int? AgentTypeID { get; set; }
        public string AgentType1 { get; set; }

    }
}