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
                var score = request.Form["Score"];

                int score_int;
                int.TryParse(score, out score_int);
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
                            if (score_int <= 95 && score_int >= 70)
                            {
                                //response.Redirect("VotePageHandler.ashx");

                                Vote(id, score_int);

                                response.Write("ok");
                            }
                            else
                            {
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



        public void Vote(string id,int score)
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
                    AppDomain.CurrentCandidate.Voters.Add(voter);
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
                    AppDomain.CurrentCandidate.Voters.Add(voter);
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