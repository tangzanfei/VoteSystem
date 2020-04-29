using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteSystem
{
    using Models;
    public static class AppDomain
    {
        public static int[] debug = new int[3];
        public static bool IsInited { get; set; }

        private static List<Candidate> candidates=new List<Candidate>();

        /// <summary>
        /// 候选人列表
        /// </summary>
        public static List<Candidate> Candidates
        {
            get { return candidates; }
            set { candidates = value; }
        }

        private static List<Voter> _voters = new List<Voter>();

        /// <summary>
        /// 投票人列表
        /// </summary>
        public static List<Voter> Voters
        {
            get { return _voters; }
            set { _voters = value; }
        }

        public static Candidate CurrentCandidate { get; set; }

        public static Voter FindVoterById(string id)
        {
            if (Voters == null || Voters.Count == 0)
            {
                return null;
            }
            return Voters.Find(c => { return c.ID.Equals(id); });
        }


        public static void AddVotes(List<Voter> list)
        {
            Voters.AddRange(list);
        }


        /// <summary>
        /// 根据名字查找候选人
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns></returns>
        public static Candidate FindCandidateByName(string name)
        {
            if(Candidates==null ||Candidates.Count==0)
            {
                return null;
            }
            return Candidates.Find(c => { return c.Name.Equals(name); });
        }

        public static bool AddCandidate(Candidate c)
        {
            if (Candidates.Exists(ci=> { return ci.Name.Equals(c.Name); }))
            {
                return false;
            }
            else
            {
                Candidates.Add(c);
                return true;
            }
        }

        public static void Restart()
        {
            try
            {
                if (candidates!=null)
                {
                    Candidates.Clear();
                }
                if (Voters != null)
                {
                    Voters.Clear();
                }
                if (CurrentCandidate != null)
                {
                    CurrentCandidate=null;
                }
                IsInited = false;
            }
            catch (Exception)
            {

                
            }
        }

        /// <summary>
        /// 汇总计算投票结果并排序
        /// </summary>
        public static void SumResult()
        {
            int num = Voters.Count;
            foreach (var cand in Candidates)
            {
                cand.Score = 0;
                cand.VoteNum = 0;
                
                for (int i = 0; i < num; i++)
                {
                    if (Voters[i].ScoreList.ContainsKey(cand.Name))
                    {
                        cand.Score += Voters[i].ScoreList[cand.Name];
                        cand.VoteNum++;
                    }
                }
            }
            Candidates.Sort((x, y) => y.Score.CompareTo(x.Score));
        }

        /// <summary>
        /// 用于生成授权码，有授权码的情况下不需要使用
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static List<string> CreateIdList(int num)
        {
            Random ran = new Random(934);
            List<String> list = new List<string>();
            for (int i = 0; i < num; i++)
            {
                //var uuid = Guid.NewGuid().ToString().Substring(0,7);
                var uuid = ran.Next(999999);
                list.Add(uuid.ToString("D6"));
            }
            list.Sort();
            list = list.Distinct().ToList();

            if (list.Count<num)
            {
                //有重复项被删除时，补充，因为此次num不到200，在一百万中取一百个随机数重复概率极小，因此仅重复补充一次就可
#warning 后续若num增加则可能导致生成的数量不够的bug，需要做修改优化
                var uuid = ran.Next(999999);
                list.Add(uuid.ToString("D6"));
                list.Sort();

            }
            return list;
        }

        
    }
}