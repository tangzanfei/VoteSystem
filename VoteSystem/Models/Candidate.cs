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


        public int VipNum { get; set; }

        public int NomNum { get; set; }

        public int VipScore { get; set; }

        public int NomScore { get; set; }

        public int GiveUpNum { get; set; }

        public double GetResult()
        {
            double score = 0;
            int vipNum = 0, vipScore = 0, nomNum = 0, nomScore = 0,giveUpNum=0;
            if (Voters != null)
            {

                foreach (var vote in Voters)
                {
                    if (vote.IsVip)
                    {
                        //弃权票不计入，超出分数范围视为弃权票
                        if (vote.Score >= 70 && vote.Score <= 95)
                        {
                            vipNum++;
                            vipScore += vote.Score;

                        }
                        else
                        {
                            giveUpNum++;
                        }

                    }
                    else
                    {
                        if (vote.Score >= 70 && vote.Score <= 95)
                        {
                            nomNum++;
                            nomScore += vote.Score;
                        }
                        else
                        {
                            giveUpNum++;
                        }
                        
                    }
                }
                if (vipNum!=0)
                {
                    score += vipScore * 0.6 / vipNum;
                }
                if (nomNum!=0)
                {
                    score += nomScore * 0.4 / nomNum;

                }
                Score = score;
                VipNum = vipNum;
                VipScore = vipScore;
                NomNum = nomNum;
                NomScore = nomScore;
                VoteNum = nomNum + vipNum;
                GiveUpNum = giveUpNum;
            }
            return score;
        }
    }
}