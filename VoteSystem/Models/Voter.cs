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
        /// <summary>
        /// 匿名识别码
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 本次投票分数
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 是否是VIP
        /// </summary>
        public bool IsVip { get; set; }

        private Dictionary<string, int> scoreList = new Dictionary<string, int>();

        /// <summary>
        /// 投票列表
        /// </summary>
        public Dictionary<string,int> ScoreList
        {
            get { return scoreList; }
            set { scoreList = value; }
        }

    }
}