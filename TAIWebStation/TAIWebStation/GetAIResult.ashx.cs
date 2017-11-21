using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TencentAImis;

namespace TAIWebStation
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class GetAIResult : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var form = context.Request.Form;
            var studyId = form.AllKeys.Contains("StudyId") ? form["StudyId"] : "";
            var dbType = form["dbType"];
            var patId = form["patId"];
            Aimis manager = new Aimis(false);//1111
            TAIDbManager dbmanager = new TAIDbManager();
            var paras = new AIResultRequest();
            paras.StudyId = dbmanager.GetStudyIdByPatId(dbType, patId);
            var result = manager.GetAIResult(paras);
            dbmanager.SaveAIResult(dbType, paras.StudyId, result.Data.Result);
            switch (result.Data.Result)
            {
                case 0: {
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 4:
                    {
                        break;
                    }
                case 5:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
                 
            }
            context.Response.Write(result.Data.Result);
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