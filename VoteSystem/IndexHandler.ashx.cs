using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteSystem.Models;

namespace VoteSystem
{
    /// <summary>
    /// IndexHandler 的摘要说明
    /// </summary>
    public class IndexHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var request = context.Request;
            var response = context.Response;
            try
            {
                if (request.Cookies["sessionID"] == null)
                {
                    string sessionID = context.Session.SessionID;
                    request.Cookies.Add(new HttpCookie("sessionID", sessionID) { Expires = DateTime.Now.AddMinutes(30) });
                }

                string id = request["id"];
                context.Session["ID"] = id;

                context.Response.Redirect("Views/Login.html?id=" + id);


            }
            catch (Exception e)
            {
                FileHelper.WriteLog(e);
                response.Write(e);
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
}