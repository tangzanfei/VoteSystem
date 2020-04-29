using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteSystem.Models;

namespace VoteSystem.Views
{
    /// <summary>
    /// 登录页面处理程序
    /// </summary>
    public class LoginHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        //public void ProcessRequest(HttpContext context)
        //{
        //    var QQcontext = new QConnectSDK.Context.QzoneContext();
        //    string state = Guid.NewGuid().ToString().Replace("-", "");
        //    string scope = "";
        //    var authenticationUrl = QQcontext.GetAuthorizationUrl(state, scope);
        //    //request token, request token secret 需要保存起来 
        //    //在demo演示中，直接保存在全局变量中.真实情况需要网站自己处理 
        //    context.Session["requeststate"] = state;
        //    context.Response.Redirect(authenticationUrl);
        //}

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var request = context.Request;
            var response = context.Response;
            try
            {
                if (request.Cookies["sessionID"]==null)
                {
                    string sessionID = context.Session.SessionID;
                    request.Cookies.Add(new HttpCookie("sessionID", sessionID) { Expires = DateTime.Now.AddMinutes(30) });
                }
                if (request.Form["ID"] != null)
                {
                    var id = request.Form["ID"];
                    if (id.Equals("Admin"))
                    {
                        //如果是超级管理员，进入后台
                        context.Session["ID"] = id;
                        response.Write("admin");
                        return;
                    }

                    if (CheckID(id))
                    {
                        if (AppDomain.IsInited)
                        {
                            context.Session["ID"] = id;
                            response.Write("ok");
                        }
                        else
                        {
                            response.Write("投票尚未开始");
                        }
                    }
                    else
                    {
                        response.Write("无效的ID");
                    }
                }
                else
                {
                    response.Redirect("Login.html");
                }

            }
            catch (Exception e)
            {
                FileHelper.WriteLog(e);
                response.Write(e);
            }

            //context.Response.Write("Hello World");
        }

        /// <summary>
        /// 到数据库核实id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckID(string id)
        {
            var vote = AppDomain.FindVoterById(id);
            return vote != null;
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