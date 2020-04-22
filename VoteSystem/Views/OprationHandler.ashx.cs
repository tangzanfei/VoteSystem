using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteSystem.Models;

namespace VoteSystem.Views
{
    /// <summary>
    /// OprationHandler 的摘要说明
    /// </summary>
    public class OprationHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                string action = context.Request.Form["action"];
                string param1 = context.Request.Form["param1"];
                switch (action)
                {
                    case "GetCurrentName":
                        context.Response.Write("马云");
                        break;
                    case "GetCurrentCandidate":
                        context.Response.Write(JsonHelper.ObjectToJSON(new Candidate() { Name = "马云", Score = 85.32 }));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("error");            }

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