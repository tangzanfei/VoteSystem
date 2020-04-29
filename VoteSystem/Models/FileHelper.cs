using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace VoteSystem.Models
{
    public static class FileHelper
    {
     
        /// <summary>
        /// 保存投票账号列表到文件
        /// </summary>
        public static void SaveIDList(List<string> list )
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "\\Lib\\" + "DataC.txt"))
                {
                    foreach (string s in list)
                    {
                        sw.WriteLine(s);

                    }
                }
            }
            catch (Exception e)
            {

                WriteLog(e);
            }
        }

        /// <summary>
        /// 从文件读取投票账号
        /// </summary>
        public static void LoadIDList()
        {
            try
            {
                string line = "";
                List<Voter> list = new List<Voter>();
                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Lib\\" + "DataC.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != "")
                        {
                            Voter c = new Voter();
                            c.ID = line;
                            c.IsVip = false;
                            c.Score = 0;
                            list.Add(c);
                        }
                    }
                }
                if (list.Count>0)
                {
                    AppDomain.Voters.Clear();
                    AppDomain.Voters.AddRange(list);
                }
            }
            catch (Exception e)
            {
                WriteLog(e);
            }

        }



        public static void LoadCandidates()
        {
            try
            {
                //AppDomain.Candidates.Clear();
                // 从文件中读取行政编制候选人
                string line = "";
                //Environment.CurrentDirectory
                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath+"\\Lib\\"+"DataA.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line!="")
                        {
                            Candidate c = new Candidate();
                            c.Name = line;
                            c.IsAdmin = true;
                            AppDomain.Candidates.Add(c);

                        }
                    }
                }
                //从文件中读取事业编制候选人
                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Lib\\" + "DataB.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != "")
                        {
                            Candidate c = new Candidate();
                            c.Name = line;
                            c.IsAdmin = false;
                            AppDomain.Candidates.Add(c);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                WriteLog(e);
            }

        }
        public static void WriteLog(Exception e)
        {
            using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "Log.txt", true))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(e.Message);
            }
        }
    }
}