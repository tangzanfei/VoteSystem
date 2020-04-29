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
                            var list= AppDomain.Candidates.ToArray().ToList();
                            list = list.OrderByDescending(c => c.Score).ToList();
                            int count = list.Count;
                            List<TopListItem> toplist = new List<TopListItem>();
                            for (int i = 0; i < count; i++)
                            {
                                toplist.Add(new TopListItem()
                                {
                                    Index = i+1,
                                    Name = list[i].Name,
                                    Score = list[i].Score
                                });
                            }
                            var json = JsonHelper.ObjectToJSON(toplist);
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