using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteSystem
{
    using Models;
    public static class AppDomain
    {

        public static bool IsInited { get; set; }

        private static List<Candidate> candidates=new List<Candidate>();

        public static List<Candidate> Candidates
        {
            get { return candidates; }
            set { candidates = value; }
        }

        private static List<Voter> _voters = new List<Voter>();

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
            return list;
        }

        
    }
}