using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteSystem.Models
{
    /// <summary>
    /// 投票者
    /// </summary>
    public class Voter
    {
        public enum Ticket
        {
            /// <summary>
            /// 满意票
            /// </summary>
            A=1,
            /// <summary>
            /// 基本满意票
            /// </summary>
            B=2,
            /// <summary>
            /// 不满意票
            /// </summary>
            C=3,
            /// <summary>
            /// 弃权票
            /// </summary>
            GiveUp=-1
        }

        /// <summary>
        /// 匿名识别码
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 本次投票分数
        /// </summary>
        public Ticket Score { get; set; }


        private Dictionary<string, Ticket> scoreList = new Dictionary<string, Ticket>();

        /// <summary>
        /// 投票列表
        /// </summary>
        public Dictionary<string, Ticket> ScoreList
        {
            get { return scoreList; }
            set { scoreList = value; }
        }

    }
}