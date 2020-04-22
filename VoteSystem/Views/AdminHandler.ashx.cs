using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteSystem.Models;

namespace VoteSystem.Views
{
    /// <summary>
    /// AdminHandler 的摘要说明
    /// </summary>
    public class AdminHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string action = context.Request.Form["action"];
                string param1 = context.Request.Form["param1"];
                switch (action)
                {
                    case "ExportData":
                        if (AppDomain.Candidates != null)
                        {
                            var json = JsonHelper.ObjectToJSON(AppDomain.Candidates);
                            context.Response.Write(json);
                        }

                        break;
                    case "GetCurrentName":
                        if (AppDomain.CurrentCandidate != null)
                        {
                            context.Response.Write(AppDomain.CurrentCandidate.Name);
                        }
                        break;
                    case "GetCandidateList":
                        if (AppDomain.Candidates!=null)
                        {
                            var json = JsonHelper.ObjectToJSON(AppDomain.Candidates);
                            context.Response.Write(json);
                        }
                        break;
                    case "ReStart":
                        AppDomain.Restart();
                        if (InitData())
                            context.Response.Write("系统初始化完成");
                        break;
                    case "InitData":
                        if(InitData())
                            context.Response.Write("系统初始化完成");
                        break;
                    case "GetCurrentCandidate":
                        if (AppDomain.CurrentCandidate != null)
                        {
                            AppDomain.CurrentCandidate.GetResult();
                            var json = JsonHelper.ObjectToJSON(AppDomain.CurrentCandidate);

                            //var dic = JsonHelper.DataRowFromJSON(json);
                            context.Response.Write(json);
                        }
                        break;
                    //case "GetCandidatelist":
                    //    if (AppDomain.IsInited && AppDomain.Candidates != null)
                    //    {
                    //        List<string> nameList = new List<string>();
                    //        foreach (var cand in AppDomain.Candidates)
                    //        {
                    //            nameList.Add(cand.Name);
                    //        }
                    //        var json = JsonHelper.ObjectToJSON(nameList);
                    //        context.Response.Write(json);
                    //    }
                    //    break;
                    case "SetNextCandidate":
                        var c = AppDomain.FindCandidateByName(param1);
                        if(c != null)
                        {
                            AppDomain.CurrentCandidate = c;
                            context.Response.Write(string.Format("开始候选人"+AppDomain.CurrentCandidate.Name+"的投票"));
                        }
                        break;
                    default:
                        context.Response.Write("admin");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("error");
            }


        }

        public bool InitData()
        {
            if (!AppDomain.IsInited)
            {
                AppDomain.IsInited = true;
                AppDomain.Candidates.Add(new Candidate() { Name = "唐华祥" });
                AppDomain.Candidates.Add(new Candidate() { Name = "唐仑" });
                AppDomain.Candidates.Add(new Candidate() { Name = "廖洪波" });
                AppDomain.Candidates.Add(new Candidate() { Name = "肖敦胜" });
                AppDomain.Candidates.Add(new Candidate() { Name = "邹晓杰" });
                AppDomain.Candidates.Add(new Candidate() { Name = "陆衡桂" });
                AppDomain.Candidates.Add(new Candidate() { Name = "陈利华" });
                AppDomain.Candidates.Add(new Candidate() { Name = "侯军" });
                AppDomain.Candidates.Add(new Candidate() { Name = "卢丽日" });
                AppDomain.Candidates.Add(new Candidate() { Name = "何平" });
                AppDomain.Candidates.Add(new Candidate() { Name = "陈五忠" });
                AppDomain.Candidates.Add(new Candidate() { Name = "李忠林" });
                AppDomain.Candidates.Add(new Candidate() { Name = "骆成" });
                AppDomain.Candidates.Add(new Candidate() { Name = "夏清华" });
                AppDomain.Candidates.Add(new Candidate() { Name = "欧阳宁" });
                AppDomain.Candidates.Add(new Candidate() { Name = "周轶" });
                AppDomain.Candidates.Add(new Candidate() { Name = "邓春明" });
                AppDomain.Candidates.Add(new Candidate() { Name = "廖土桂" });
                AppDomain.Candidates.Add(new Candidate() { Name = "欧阳义" });
                AppDomain.Candidates.Add(new Candidate() { Name = "尹志鹏" });
                AppDomain.Candidates.Add(new Candidate() { Name = "曹惠玲" });
                AppDomain.Candidates.Add(new Candidate() { Name = "谢友国" });
                AppDomain.Candidates.Add(new Candidate() { Name = "欧阳华军" });
                AppDomain.Candidates.Add(new Candidate() { Name = "胡傲" });
                AppDomain.Candidates.Add(new Candidate() { Name = "刘午光" });
                AppDomain.Candidates.Add(new Candidate() { Name = "卢泽锋" });
                AppDomain.Candidates.Add(new Candidate() { Name = "黄志华" });
                AppDomain.Candidates.Add(new Candidate() { Name = "江阳勇" });
                AppDomain.Candidates.Add(new Candidate() { Name = "邓伟民" });
                AppDomain.Candidates.Add(new Candidate() { Name = "谢旭韬" });
                AppDomain.Candidates.Add(new Candidate() { Name = "彭向东" });
                AppDomain.Candidates.Add(new Candidate() { Name = "宋长青" });
                AppDomain.Candidates.Add(new Candidate() { Name = "罗勇" });

                AppDomain.Voters.Add(new Models.Voter() { ID = "008705", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "015410", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "020605", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "027732", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "043269", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "059160", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "063936", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "074718", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "082563", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "089154", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "094370", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "096572", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "103886", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "112052", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "112354", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "124366", IsVip = true, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "131158", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "153778", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "155329", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "178757", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "190406", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "195087", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "203436", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "209418", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "212938", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "214802", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "216720", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "224320", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "226558", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "237278", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "240670", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "241980", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "259215", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "261993", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "280533", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "285530", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "286967", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "292424", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "301129", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "302263", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "307441", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "312465", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "321676", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "324953", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "326183", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "327966", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "332883", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "338093", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "344760", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "345500", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "352520", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "358884", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "361909", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "365365", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "385379", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "385995", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "387932", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "388586", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "390982", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "392749", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "398218", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "401454", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "404505", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "407516", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "411361", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "422580", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "430809", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "435526", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "460588", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "472552", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "475298", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "476752", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "477741", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "479168", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "485655", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "488552", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "494864", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "500987", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "503973", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "515202", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "515828", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "528947", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "529753", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "533141", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "538815", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "543613", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "553594", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "562791", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "563596", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "575282", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "576840", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "593419", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "612400", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "622805", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "623409", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "624237", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "634497", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "654605", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "657925", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "661596", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "667506", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "671486", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "672950", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "675390", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "676579", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "678958", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "684089", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "687228", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "688093", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "700221", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "708645", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "711947", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "714553", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "717779", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "735969", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "751766", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "755873", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "768903", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "773649", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "773957", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "775531", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "779435", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "782292", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "796443", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "818976", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "824940", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "826547", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "842581", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "858982", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "862245", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "865339", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "870313", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "873952", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "876613", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "877217", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "877545", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "880955", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "887194", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "890656", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "893713", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "900899", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "906165", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "913857", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "917989", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "918622", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "921600", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "922919", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "924811", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "954493", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "959600", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "960921", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "960923", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "963950", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "968627", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "976174", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "980821", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "989526", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "992526", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "996772", IsVip = false, Score = 0 });
                AppDomain.Voters.Add(new Models.Voter() { ID = "999134", IsVip = false, Score = 0 });

                //var list =AppDomain.CreateIdList(160);          
                //list.Sort();                                    
                //var list2 = list.Skip(100).ToList();            
                return true;
            }
            return false;
        }
        /*
        
        
        
        
008705
015410
020605
027732
043269
059160
063936
074718
082563
089154
094370
096572
103886
112052
112354
124366
131158
153778
155329
178757
190406
195087
203436
209418
212938
214802
216720
224320
226558
237278
240670
241980
259215
261993
280533
285530
286967
292424
301129
302263
307441
312465
321676
324953
326183
327966
332883
338093
344760
345500
352520
358884
361909
365365
385379
385995
387932
388586
390982
392749
398218
401454
404505
407516
411361
422580
430809
435526
460588
472552
475298
476752
477741
479168
485655
488552
494864
500987
503973
515202
515828
528947
529753
533141
538815
543613
553594
562791
563596
575282
576840
593419
612400
622805
623409
624237
634497
654605
657925
661596
667506
671486
672950
675390
676579
678958
684089
687228
688093
700221
708645
711947
714553
717779
735969
751766
755873
768903
773649
773957
775531
779435
782292
796443
818976
824940
826547
842581
858982
862245
865339
870313
873952
876613
877217
877545
880955
887194
890656
893713
900899
906165
913857
917989
918622
921600
922919
924811
954493
959600
960921
960923
963950
968627
976174
980821
989526
992526
996772
999134
    */
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}