using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteSystem.Views
{
    /// <summary>
    /// GetNameHandler 的摘要说明
    /// </summary>
    public class GetNameHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                if (AppDomain.CurrentCandidate!=null)
                {
                    context.Response.Write(AppDomain.CurrentCandidate.Name);
                }

            }
            catch (Exception)
            {
                context.Response.Write("error");
            }
            //context.Response.Write("马云");
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