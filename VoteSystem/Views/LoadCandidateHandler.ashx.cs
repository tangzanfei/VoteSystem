using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteSystem.Views
{
    /// <summary>
    /// LoadCandidateHandler 的摘要说明
    /// </summary>
    public class LoadCandidateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                if (AppDomain.Candidates != null)
                {
                    //List<string> namelist = AppDomain.Candidates.Select(c => c.Name).ToList();

                    //string modelstr = "<fieldset data - role = \"controlgroup\" data - type = \"horizontal\" id = \"MYNAME\" ><legend > MYNAME </legend ><label for= \"MYNAMEA\" > 优秀 </label ><input type = \"radio\" name = \"MYNAME\" id = \"MYNAMEA\" value = \"10\" ><label for= \"MYNAMEB\" > 称职 </label ><input type = \"radio\" name = \"MYNAME\" id = \"MYNAMEB\" value = \"8\" checked><label for= \"MYNAMEC\" > 基本称职 </label ><input type = \"radio\" name = \"MYNAME\" id = \"MYNAMEC\" value = \"7\" ><label for= \"MYNAMED\" > 不称职 </label ><input type = \"radio\" name = \"MYNAME\" id = \"MYNAMED\" value = \"5\" ></fieldset >";
                    //string htmlstr = "";
                    //List<string> htmlStrList = new List<string>();
                    //for (int i = 0; i < namelist.Count; i++)
                    //{
                    //    string name = modelstr.Replace("MYNAME", namelist[i]);
                    //    htmlStrList.Add(name);
                    //    htmlstr += name;
                    //}
                    //string json = Models.JsonHelper.ObjectToJSON(htmlStrList);
                    context.Response.Write("");
                    //Models.FileHelper.SaveHtml();
                }

            }
            catch (Exception e)
            {
                Models.FileHelper.WriteLog(e);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}