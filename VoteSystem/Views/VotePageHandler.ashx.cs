using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteSystem.Views
{
    /// <summary>
    /// VotePageHandler 的摘要说明
    /// </summary>
    public class VotePageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html;charset=UTF-8";
            var request = context.Request;
            var response = context.Response;
            try
            {

                //context.Response.Write("Hello World");
                var name = request.Form["Vote"];
                string score = request.Form["Score"];

                if (String.IsNullOrEmpty(score))
                {
                    response.Write("投票失败，测评结果为空");

                }



                //var ticket = (Models.Voter.Ticket)score;
                Models.Voter.Ticket ticket = Models.Voter.Ticket.GiveUp;
                switch (score)
                {
                    case "A":
                        ticket = Models.Voter.Ticket.A;
                        break;
                    case "B":
                        ticket = Models.Voter.Ticket.B;
                        break;
                    case "C":
                        ticket = Models.Voter.Ticket.C;
                        break;
                    default:
                        break;
                }

                string id = context.Session["ID"].ToString();
                if (name != null)
                {

                    if (AppDomain.CurrentCandidate != null)
                    {
                        if (name.Equals(AppDomain.CurrentCandidate.Name))
                        {

                            if (AppDomain.CurrentCandidate != null && AppDomain.CurrentCandidate.Voters != null)
                            {
                                if (AppDomain.CurrentCandidate.Voters.Exists(v => { return v.ID.Equals(id); }))
                                {
                                    response.Write("投票失败，您已投过票，请勿重复投票");
                                    return;
                                }

                            }
                            //if (score_int <= 95 && score_int >= 70)
                            if (ticket != Models.Voter.Ticket.GiveUp)
                                {
                                //response.Redirect("VotePageHandler.ashx");

                                Vote(id, ticket);

                                response.Write("ok");
                            }
                            else
                            {//页面脚本上点弃权给了非法值
                                GiveUp(id);
                                response.Write("弃权");
                            }
                        }
                        else
                        {
                            response.Write(string.Format("候选人" + name + "的投票已经结束"));
                        }
                    }
                    else
                    {
                        response.Write(string.Format("投票尚未开始"));
                    }

                }
                else
                {
                    response.Redirect("VotePage.html");
                }
            }
            catch (Exception e)
            {
                response.Write(e);

            }
       }



        public void Vote(string id,Models.Voter.Ticket score)
        {
            if (AppDomain.CurrentCandidate!=null)
            {
                if (AppDomain.CurrentCandidate.Voters==null)
                {
                    AppDomain.CurrentCandidate.Voters = new List<Models.Voter>();
                }
                var voter = AppDomain.FindVoterById(id);
                if(voter!=null)
                {
                    voter.Score = score;
                    AppDomain.CurrentCandidate.Voters.Add(new Models.Voter() { ID=voter.ID,Score=voter.Score});


                    //为了导出投票明细，给每个投票人的列表也加上投票情况
                    if (voter.ScoreList == null)
                    {
                        voter.ScoreList = new Dictionary<string, Models.Voter.Ticket>();
                    }
                    voter.ScoreList.Add(AppDomain.CurrentCandidate.Name, voter.Score);
                }
                
            }
        }


        public void GiveUp(string id)
        {
            if (AppDomain.CurrentCandidate != null)
            {
                if (AppDomain.CurrentCandidate.Voters == null)
                {
                    AppDomain.CurrentCandidate.Voters = new List<Models.Voter>();
                }
                var voter = AppDomain.FindVoterById(id);
                if (voter != null)
                {
                    voter.Score = 0;
                    AppDomain.CurrentCandidate.Voters.Add(new Models.Voter() { ID = voter.ID, Score = voter.Score });

                    //为了导出投票明细，给每个投票人的列表也加上投票情况
                    if (voter.ScoreList == null)
                    {
                        voter.ScoreList = new Dictionary<string, Models.Voter.Ticket>();
                    }
                    voter.ScoreList.Add(AppDomain.CurrentCandidate.Name, voter.Score);

                }

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