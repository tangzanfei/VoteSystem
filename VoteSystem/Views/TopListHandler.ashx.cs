using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteSystem.Models;

namespace VoteSystem.Views
{
    /// <summary>
    /// TopListHandler 的摘要说明
    /// </summary>
    public class TopListHandler : IHttpHandler
    {
        class TopListItem
        {
            private int _index=1;

            public int Index
            {
                get { return _index; }
                set { _index = value; }
            }

            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private double _score = 0;

            public double Score
            {
                get { return _score; }
                set { _score = value; }
            }

            private int voteNum;

            public int VoteNum
            {
                get { return voteNum; }
                set { voteNum = value; }
            }


        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string action = context.Request.Form["action"];
                string param1 = context.Request.Form["param1"];
                switch (action)
                {
                    case "GetScoreList":
                        if (AppDomain.Candidates != null)
                        {
                            AppDomain.SumResult();
                            var list= AppDomain.Candidates.ToArray().ToList();
                            list = list.OrderByDescending(c => c.Score).ToList();
                            int count = list.Count;
                            //var list1 = list.SkipWhile(c => c.IsAdmin==false).ToList();
                            //var list2 = list.SkipWhile(c => c.IsAdmin == true).ToList();
                            var list1 = list.Where(c => c.IsAdmin).ToList();
                            var list2 = list.Where(a => !a.IsAdmin).ToList();
                            List<TopListItem> admintoplist = new List<TopListItem>();
                            List<TopListItem> toplist = new List<TopListItem>();

                            for (int i = 0; i < list1.Count; i++)
                            {
                                admintoplist.Add(new TopListItem()
                                {
                                    Index = i + 1,
                                    Name = list1[i].Name,
                                    Score = list1[i].Score,
                                    VoteNum = list1[i].VoteNum
                                });
                            }


                            for (int i = 0; i < list2.Count; i++)
                            {
                                toplist.Add(new TopListItem()
                                {
                                    Index = i + 1,
                                    Name = list2[i].Name,
                                    Score = list2[i].Score,
                                    VoteNum = list2[i].VoteNum
                                });
                            }
                            Dictionary<string, List<TopListItem>> dic = new Dictionary<string, List<TopListItem>>();
                            dic.Add("admin", admintoplist);
                            dic.Add("list", toplist);

                            var json = JsonHelper.ObjectToJSON(dic);
                            context.Response.Write(json);
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                FileHelper.WriteLog(ex);
                context.Response.Write("error");
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