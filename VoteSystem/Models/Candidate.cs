using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteSystem.Models
{
    /// <summary>
    /// 候选人类
    /// </summary>
    public class Candidate
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// 票数
        /// </summary>
        public int VoteNum { get; set; }

        /// <summary>
        /// 是否是行政编制
        /// </summary>
        public bool IsAdmin { get; set; }

        private List<Voter> voters = new List<Voter>();

        /// <summary>
        /// 已投票的人
        /// </summary>
      public List<Voter> Voters
        {
            get { return voters; }
            set { voters = value; }
        }



        public double GetResult()
        {
            double score = 0;
            if (Voters != null)
            {

                foreach (var vote in Voters)
                {
                    score += vote.Score;
                }
                Score = score;
                VoteNum = voters.Count;
            }
            return score;
        }
    }
}