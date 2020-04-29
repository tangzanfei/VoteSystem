using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteSystem.Models;

namespace VoteSystem.Views
{
    /// <summary>
    /// AdminHandler 的摘要说明
    /// </summary>
    public class AdminHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                    //case "ExportData":
                    //    if (AppDomain.Candidates != null)
                    //    {
                    //        var json = JsonHelper.ObjectToJSON(AppDomain.Candidates);
                    //        context.Response.Write(json);
                    //    }

                    //    break;
                    //case "GetCurrentName":
                    //    if (AppDomain.CurrentCandidate != null)
                    //    {
                    //        context.Response.Write(AppDomain.CurrentCandidate.Name);
                    //    }
                    //    break;
                    //case "GetCandidateList":
                    //    if (AppDomain.Candidates!=null)
                    //    {
                    //        var json = JsonHelper.ObjectToJSON(AppDomain.Candidates);
                    //        context.Response.Write(json);
                    //    }
                    //    break;
                    //case "ReStart":
                    //    AppDomain.Restart();
                    //    if (InitData())
                    //        context.Response.Write("系统初始化完成");
                    //    break;
                    case "InitData":
                        if(InitData())
                            context.Response.Write("系统初始化完成");
                        break;
                    case "GetCurrentCandidate":
                        if (AppDomain.CurrentCandidate != null)
                        {
                            AppDomain.CurrentCandidate.GetResult();
                            var json = JsonHelper.ObjectToJSON(AppDomain.CurrentCandidate);

                            //var dic = JsonHelper.DataRowFromJSON(json);
                            context.Response.Write(json);
                        }
                        break;
                    //case "SetNextCandidate":
                    //    var c = AppDomain.FindCandidateByName(param1);
                    //    if(c != null)
                    //    {
                    //        AppDomain.CurrentCandidate = c;
                    //        context.Response.Write(string.Format("开始候选人"+AppDomain.CurrentCandidate.Name+"的投票"));
                    //    }
                    //    break;
                    default:
                        context.Response.Write("admin");
                        break;
                }
            }
            catch (Exception ex)
            {
                FileHelper.WriteLog(ex);
                context.Response.Write("error");
            }


        }

        /// <summary>
        /// 初始化投票系统数据（不重置账号）
        /// </summary>
        /// <returns></returns>
        public bool InitData()
        {
            if (!AppDomain.IsInited)
            {

                AppDomain.IsInited = true;
                if (AppDomain.Voters == null)
                    AppDomain.Voters = new List<Voter>();

                if(AppDomain.Voters.Count <= 0)
                {
                    FileHelper.LoadIDList();
                    ////加载账号失败，则重新生成账号
                    //var list = AppDomain.CreateIdList(120);
                    //FileHelper.SaveIDList(list);
                    //FileHelper.LoadIDList();
                }
                FileHelper.LoadCandidates();


                return true;
            }
            return false;
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