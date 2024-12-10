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
        /// 候选人姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 候选人部门
        /// </summary>
        public string Department { get; set; }


        private int giveUpNum = 0;

        /// <summary>
        /// 弃权票
        /// </summary>
        public int GiveUpNum
        {
            get { return giveUpNum; }
            set { giveUpNum = value; }
        }

        /// <summary>
        /// 满意票数
        /// </summary>
        public int A_Num{ get; set; }

        /// <summary>
        /// 基本满意票数
        /// </summary>
        public int B_Num { get; set; }

        /// <summary>
        /// 不满意票数
        /// </summary>
        public int C_Num { get; set; }

        /// <summary>
        /// 总票数（不含弃权票）
        /// </summary>
        public int Total_Num
        {
            get
            {
                return this.A_Num + this.B_Num + this.C_Num;
            }
        }

        /// <summary>
        /// 满意票占比
        /// </summary>
        public double A_Ratio { get; set; }

        /// <summary>
        /// 基本满意票占比
        /// </summary>
        public double B_Ratio { get; set; }
        
        /// <summary>
        /// 不满意票占比
        /// </summary>
        public double C_Ratio { get; set; }

        private List<Voter> voters = new List<Voter>();

        /// <summary>
        /// 已投票的人
        /// </summary>
      public List<Voter> Voters
        {
            get { return voters; }
            set { voters = value; }
        }



        //public double GetResult()
        //{
        //    Score = 0;
        //    NomScore = VipScore = 0;
        //    GiveUpNum = NomNum = VipNum = 0;
        //    if (Voters != null)
        //    {

        //        foreach (var vote in Voters)
        //        {
        //            if (vote.Score == 0)
        //            {
        //                giveUpNum++;
        //            }
        //            else
        //            {

        //                if (vote.IsVip)
        //                {
        //                    vipNum++;
        //                    vipScore += vote.Score;
        //                }
        //                else
        //                {
        //                    nomNum++;
        //                    nomScore += vote.Score;
        //                }
        //            }
        //        }
        //        if (vipNum!=0)
        //        {
        //            Score += vipScore / vipNum * 0.5;
        //        }
        //        if (nomNum!=0)
        //        {
        //            Score+= nomScore / NomNum * 0.5;
        //        }
        //        VoteNum = voters.Count-GiveUpNum;//总票数不含弃权票
        //    }
        //    return Score;
        //}

        /// <summary>
        /// 计算投票结果
        /// </summary>
        /// <returns>如果没有投票返回false，有投票记录返回ture</returns>
        public bool SumResult()
        {
            A_Num = B_Num = C_Num = 0;
            A_Ratio = B_Ratio = C_Ratio = 0;
            
            //如果还没有投票返回false
            if (Voters != null)
            {
                foreach (var vote in Voters)
                {
                    switch (vote.Score)
                    {
                        case Voter.Ticket.A:
                            this.A_Num++;
                            break;
                        case Voter.Ticket.B:
                            this.B_Num++;
                            break;
                        case Voter.Ticket.C:
                            this.C_Num++;
                            break;
                        case Voter.Ticket.GiveUp:
                        default:
                            this.GiveUpNum++;
                            break;
                    }
                }
                if (this.Total_Num == 0)
                {
                    return false;
                }
                A_Ratio = (double)A_Num / Total_Num;
                B_Ratio = (double)B_Num / Total_Num;
                C_Ratio = (double)C_Num / Total_Num;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}