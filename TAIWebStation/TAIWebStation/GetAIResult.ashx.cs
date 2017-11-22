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
            string resultStr = "";
            var form = context.Request.Form;
            var studyId = form.AllKeys.Contains("StudyId") ? form["StudyId"] : "";
            var dbType = form["dbType"].Trim();
            var patId = form["patId"].Trim();
            Aimis manager = new Aimis(false);//1111
            TAIDbManager dbmanager = new TAIDbManager();
            var paras = new AIResultRequest();           
            paras.StudyId = dbmanager.GetStudyIdByPatId(dbType, patId);
            var result = manager.GetAIResult(paras);
            if (result.Code == 0)
            {
                dbmanager.SaveAIResult(dbType, paras.StudyId, result.Data.Result);
                resultStr = result.Data.Result.ToString();                 
            }
            context.Response.Write(result);
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