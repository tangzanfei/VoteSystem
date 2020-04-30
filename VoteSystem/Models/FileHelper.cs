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


        /// <summary>
        /// 从文件加载候选人数据
        /// </summary>
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

        /// <summary>
        /// 保存投票结果
        /// </summary>
        public static void SaveReuslt()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "ResultA.txt"))
                {
                    int index = 1;
                    foreach (var cand in AppDomain.Candidates)
                    {
                        if (cand.IsAdmin)
                        {
                            sw.WriteLine("名次：{0},{1},分数：{2},票数：{3}", index, cand.Name, cand.Score, cand.VoteNum);
                            index++;
                        }
                    }
                }
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "ResultB.txt"))
                {
                    int index = 1;
                    foreach (var cand in AppDomain.Candidates)
                    {
                        if (!cand.IsAdmin)
                        {
                            sw.WriteLine("名次：{0},{1},分数：{2},票数：{3}", index, cand.Name, cand.Score, cand.VoteNum);
                            index++;
                        }
                    }
                }
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "Detail.txt"))
                {
                    int index = 1;
                    sw.WriteLine("投票明细数据：");
                    foreach (var voter in AppDomain.Voters)
                    {
                        sw.WriteLine("授权码：{0}", voter.ID);
                        foreach (var dic in voter.ScoreList)
                        {
                            sw.WriteLine("{0},{1}分", dic.Key, dic.Value);
                        }
                        sw.WriteLine();
                        index++;
                    }
                }
            }
            catch (Exception e)
            {

                WriteLog(e);
            }

        }

        public static void SaveHtml()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "Html.txt"))
                {
                    int index = 1;
                    foreach (var cand in AppDomain.Candidates)
                    {
                        sw.WriteLine("<fieldset data-role = \"controlgroup\" data-type = \"horizontal\" id = \"{0}\" >", cand.Name);
                        sw.WriteLine("<legend > {0} </legend >", cand.Name);
                        sw.WriteLine("<label for= \"{0}A\" > 优秀 </label >", cand.Name);
                        sw.WriteLine("<input type = \"radio\" name = \"{0}\" id = \"{0}A\" value = \"10\" >", cand.Name);
                        sw.WriteLine("<label for= \"{0}B\" > 称职 </label >", cand.Name);
                        sw.WriteLine("<input type = \"radio\" name = \"{0}\" id = \"{0}B\" value = \"8\" checked>", cand.Name);
                        sw.WriteLine("<label for= \"{0}C\" > 基本称职 </label >     ", cand.Name);
                        sw.WriteLine("<input type = \"radio\" name = \"{0}\" id = \"{0}C\" value = \"7\" >", cand.Name);
                        sw.WriteLine("<label for= \"{0}D\" > 不称职 </label >", cand.Name);
                        sw.WriteLine("<input type = \"radio\" name = \"{0}\" id = \"{0}D\" value = \"5\" >", cand.Name);
                        sw.WriteLine("</fieldset >", cand.Name);

                        index++;
                    }
                }
            }
            catch (Exception e)
            {

                WriteLog(e);
            }

        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="e"></param>
        public static void WriteLog(Exception e)
        {
            using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "Log.txt", true))
            {
                sw.WriteLine(DateTime.Now);
                if (e.InnerException != null)
                {
                    sw.WriteLine(e.InnerException.Message);
                }
                else
                {
                    sw.WriteLine(e.Message);
                }
            }
        }
    }
}