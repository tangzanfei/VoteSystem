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
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != "")
                        {
                            Voter c = new Voter();
                            c.ID = line;
                            //if (i<20)//给定20个领导账号
                            //{
                            //    c.IsVip = true;
                            //}
                            //else
                            //{
                            //    c.IsVip = false;
                            //}
                            c.Score = Voter.Ticket.GiveUp;
                            list.Add(c);
                            i++;
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
        public static bool LoadCandidates()
        {
            try
            {
                AppDomain.Candidates.Clear();
                // 从文件中读取候选人
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
                            AppDomain.Candidates.Add(c);

                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                WriteLog(e);
                return false;
            }

        }

        /// <summary>
        /// 保存投票结果
        /// </summary>
        public static void SaveReuslt()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "ResultA.txt",false,System.Text.Encoding.GetEncoding("gb2312")))
                {
                    int index = 1;
                    foreach (var cand in AppDomain.Candidates)
                    {
                        //sw.WriteLine("名次：{0},{1},总分数：{2},有效票数：{3},弃权票数：{8},领导总分：{4},领导票数：{5},一般干部总分：{6},一般干部票数{7}",
                        //    index, cand.Name, cand.Score, cand.VoteNum,cand.VipScore,cand.VipNum,cand.NomScore,cand.NomNum,cand.GiveUpNum);
                        sw.WriteLine("序号：{0},单位：{1},得票总数：{2},满意票数：{3},满意度：{4},基本满意票数：{5},基本满意度：{6},不满意票数：{7},不满意度：{8},",
                                          index, cand.Name, cand.Total_Num,  cand.A_Num,    cand.A_Ratio, cand.B_Num,     cand.B_Ratio,    cand.C_Num, cand.C_Ratio);
                        index++;
                    }
                }
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "Detail.txt",false,System.Text.Encoding.GetEncoding("gb2312")))
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
                        sw.WriteLine("<input type = \"radio\" name = \"{0}\" id = \"{0}C\" value = \"6\" >", cand.Name);
                        sw.WriteLine("<label for= \"{0}D\" > 不称职 </label >", cand.Name);
                        sw.WriteLine("<input type = \"radio\" name = \"{0}\" id = \"{0}D\" value = \"3\" >", cand.Name);
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