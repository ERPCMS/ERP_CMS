using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_CMS.Admin.Setup.Agent
{
    /// <summary>
    /// Summary description for Agent_Handler
    /// </summary>
    public class Agent_Handler : IHttpHandler
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