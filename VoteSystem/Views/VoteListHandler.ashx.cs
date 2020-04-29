using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteSystem.Models;

namespace VoteSystem.Views
{
    /// <summary>
    /// VoteListHandler 的摘要说明
    /// </summary>
    public class VoteListHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html;charset=UTF-8";
            var request = context.Request;
            var response = context.Response;
            try
            {
                var name = request.Form["Name[]"];
                var score = request.Form["Score[]"];
                int i = 0;
                //List<int> a = new List<int> { 1, 2, 3 };
                //int[] b = { 1, 2, 3 };
                //string astr= JsonHelper.ObjectToJSON(b);
                score = score.Insert(0, "[");
                score = score + "]";
                var nameList = name.Split(',');
                var scoreList = JsonHelper.JSONToObject<List<int>>(score);

                string id = context.Session["ID"].ToString();//投票人ID

                if (AppDomain.Voters != null && AppDomain.Voters.Count > 0 )
                {
                    if (!AppDomain.Voters.Exists(v => { return v.ID.Equals(id); }))
                    {
                        response.Write("投票失败,无效的授权码");
                        return;
                    }
                    else
                    {
                        Vote(id, nameList, scoreList);
                        response.Write("ok");
                        return;
                    }
                }
                response.Write("投票已结束或尚未开始");

            }
            catch(Exception e)
            {
                FileHelper.WriteLog(e);
            }
        }


        /// <summary>
        /// 收集一个人的投票结果，尚未汇总分数。如已投过票，则覆盖
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public void Vote(string id, string[] name, List<int> score)
        {

            //1.在app中找到投票人列表，根据id找到投票者，如果没有就new一个，如果有就赋值。
            if (AppDomain.Voters!=null && AppDomain.Voters.Count>0)
            {
                var vote = AppDomain.Voters.Find((v) => { return v.ID == id; });
                if (vote==null)
                {
                    vote = new Voter();
                    AppDomain.Voters.Add(vote);
                }
                vote.ID = id;
                vote.ScoreList.Clear();
                for (int i = 0; i < name.Length; i++)
                {
                    vote.ScoreList.Add(name[i], score[i]);
                }
            }


            //foreach (var cand in AppDomain.Candidates)
            //{
            //    if (cand != null)
            //    {

            //        if (cand.Voters == null)
            //        {
            //            cand.Voters = new List<Models.Voter>();
            //        }

            //        Voter tempVoter;
            //        if (cand.Voters.Count <= 0)
            //        {
            //            tempVoter = new Voter();
            //        }
            //           tempVoter= cand.Voters.Find(c => { return c.ID.Equals(id); });


            //        var voter = AppDomain.FindVoterById(id);
            //        if (voter != null)
            //        {
            //            int index = AppDomain.Voters.IndexOf(voter);
            //            voter.Score = score[index];
            //            cand.Voters.Add(voter);
            //        }

            //    }

            //}
        }


        //public void GiveUp(string id)
        //{
        //    if (AppDomain.CurrentCandidate != null)
        //    {
        //        if (AppDomain.CurrentCandidate.Voters == null)
        //        {
        //            AppDomain.CurrentCandidate.Voters = new List<Models.Voter>();
        //        }
        //        var voter = AppDomain.FindVoterById(id);
        //        if (voter != null)
        //        {
        //            voter.Score = 0;
        //            AppDomain.CurrentCandidate.Voters.Add(voter);
        //        }

        //    }

        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}