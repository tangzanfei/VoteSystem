using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteSystem
{
    /// <summary>
    /// IndexHandler 的摘要说明
    /// </summary>
    public class IndexHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Redirect("Views/Login.html");
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