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
                    case "ExportData":
                        AppDomain.SumResult();
                        FileHelper.SaveReuslt();

                        //if (AppDomain.Candidates != null)
                        //{
                        //    var json = JsonHelper.ObjectToJSON(AppDomain.Candidates);
                        //    context.Response.Write(json);
                        //}
                        //要考虑异常时的情况，增加导出失败的提示。
                        context.Response.Write("结果导出成功");

                        break;
                    case "GetCurrentName":
                        if (AppDomain.CurrentCandidate != null)
                        {
                            context.Response.Write(AppDomain.CurrentCandidate.Name);
                        }
                        break;
                    case "GetCandidateList":
                        if (AppDomain.Candidates != null)
                        {
                            var json = JsonHelper.ObjectToJSON(AppDomain.Candidates);
                            context.Response.Write(json);
                        }
                        break;
                    case "ReStart":
                        AppDomain.Restart();
                        if (InitData())
                            context.Response.Write("系统初始化完成");
                        else
                            context.Response.Write("系统初始化出错");
                        break;
                    case "InitData":
                        if(AppDomain.IsInited)
                        {
                            //如果已初始过，不执行
                            context.Response.Write("系统初始化已完成");
                        }
                        else
                        {
                            //已判断过初始化完成标记，所以如果返回false一定是因为报错而不会是因为初始化完成标记而跳过。
                            if (InitData())
                                context.Response.Write("系统初始化完成");
                            else
                                context.Response.Write("系统初始化出错");

                        }
                        break;
                    case "GetCurrentCandidate":
                        //刷新得票情况
                         if (AppDomain.CurrentCandidate != null)
                        {
                            AppDomain.CurrentCandidate.SumResult();
                            var json = JsonHelper.ObjectToJSON(AppDomain.CurrentCandidate);

                            //var dic = JsonHelper.DataRowFromJSON(json);
                            context.Response.Write(json);
                        }
                        else
                           {//说明还没开始选举,返回空
                            context.Response.Write("");
                        }
                        break;
                    case "SetNextCandidate":
                        var c = AppDomain.FindCandidateByName(param1);
                        if (c != null)
                        {
                            AppDomain.CurrentCandidate = c;
                            context.Response.Write(string.Format("开始对" + AppDomain.CurrentCandidate.Name + "的投票"));
                        }
                        break;
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
                    //var list = AppDomain.CreateIdList(60);
                    //FileHelper.SaveIDList(list);
                    //FileHelper.LoadIDList();
                }
                return FileHelper.LoadCandidates();

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