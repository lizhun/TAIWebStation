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
    /// UpdateAIData 的摘要说明
    /// </summary>
    public class UpdateAIData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.HttpMethod == "POST")
            {
                var dbType = context.Request.Form["DbType"].Trim();
                var patId = context.Request.Form["PatId"].Trim();
                WebClient cc = new WebClient();
                var paras = new NameValueCollection();
                foreach (var key in context.Request.Form.AllKeys)
                {
                    paras.Add(key, context.Request.Form[key]);
                }
                string studyId = Guid.NewGuid().ToString();
                paras.Add("StudyId", studyId);
                byte[] responseData = cc.UploadValues(AppSettings.FrontServerBaseUrl + "/UpdateAIData.ashx", paras);
                var strdata = Encoding.UTF8.GetString(responseData);
                var imgdatalist = context.Request.Form.AllKeys.Where(x => x.Contains("img_"));
                var imgids = imgdatalist.Select(x => x.Replace("img_", "")
           .Replace("_content", "").Replace("_url", "")).Distinct().ToList();
                TAIDbManager dbmanager = new TAIDbManager();
                dbmanager.SaveStudyUpload(dbType, studyId, patId, imgids);
                context.Response.Write(strdata);
            }
            else
            {
                context.Response.Write("Only support post metod!");
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