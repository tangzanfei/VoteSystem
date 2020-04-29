using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteSystem.Models;

namespace VoteSystem.Views
{
    /// <summary>
    /// AdminPageHandler 的摘要说明
    /// </summary>
    public class AdminPageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                        //FileHelper.LoadIDList();

                        //if (AppDomain.Candidates != null)
                        //{
                        //    var json = JsonHelper.ObjectToJSON(AppDomain.Candidates);
                        //    context.Response.Write(json);
                        //}
                        context.Response.Write("结果导出成功");
                        break;
                    case "InitData":
                        if (InitData())
                        context.Response.Write("系统初始化完成");
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

                if (AppDomain.Voters.Count <= 0)
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