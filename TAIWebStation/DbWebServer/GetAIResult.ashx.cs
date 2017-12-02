using BLL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace DbWebServer
{
    /// <summary>
    /// GetAIResult 的摘要说明
    /// </summary>
    public class GetAIResult : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var form = context.Request.Form;
            var dbType = form["dbType"]?.Trim();
            var patId = form["patId"]?.Trim();
            TAIDbManager dbmanager = new TAIDbManager();
            var StudyId = dbmanager.GetStudyIdByPatId(dbType, patId);
            WebClient cc = new WebClient();
            var paras = new NameValueCollection();
            paras.Add("StudyId", StudyId);
            byte[] responseData = cc.UploadValues(AppSettings.FrontServerBaseUrl + "/GetAIResult.ashx", paras);
            var strdata = Encoding.UTF8.GetString(responseData);
            context.Response.Write(strdata);
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