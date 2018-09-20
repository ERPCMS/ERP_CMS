using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_CMS.Admin.Setup.Shipper
{
    /// <summary>
    /// Summary description for Shipper_Handler
    /// </summary>
    public class Shipper_Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}