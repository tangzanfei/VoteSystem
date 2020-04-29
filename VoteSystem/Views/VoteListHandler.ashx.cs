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
                //string astr= Models.JsonHelper.ObjectToJSON(b);
                score = score.Insert(0, "[");
                score = score + "]";
                var nameList = name.Split(',');
                var scoreList = Models.JsonHelper.JSONToObject<List<int>>(score);

                string id = context.Session["ID"].ToString();//投票人ID

                if (AppDomain.Candidates != null && AppDomain.Candidates[0] != null && AppDomain.Candidates[0].Voters != null)
                {
                    if (AppDomain.Candidates[0].Voters.Exists(v => { return v.ID.Equals(id); }))
                    {
                        response.Write("投票失败，您已投过票，请勿重复投票");
                        return;
                    }
                }
                Vote(id, nameList, scoreList);

                response.Write("ok");
            }
            catch(Exception e)
            {
                Models.FileHelper.WriteLog(e);
            }
        }



        public void Vote(string id, string[] name, List<int> score)
        {
//1.根据name在appdomain找到候选人
//2.判断候选人的选票列表是否为空，为空则new
//3.找到选票列表有没有id，有则，给它赋值score，没有则new一个新的并添加到候选人的选票列表
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