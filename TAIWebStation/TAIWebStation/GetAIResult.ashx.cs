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
            string tencentUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
            string parentId = System.Configuration.ConfigurationManager.AppSettings["PartnerId"];
            context.Response.ContentType = "text/plain";
            string resultStr = "";// "1$中就事论事$http://www.baidu.com";
            string code = "", codename = "处理失败";
            var form = context.Request.Form;
            var studyId = form.AllKeys.Contains("StudyId") ? form["StudyId"] : "";
            string url = string.Format("{0}/studydetail?partnerId={1}&studyId={2}",
                tencentUrl, parentId, studyId);
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
                code = result.Data.Result.ToString();
                codename = getCodeName(code);
            }
            resultStr = string.Format("{0}${1}${2}", code, codename, url);
            context.Response.Write(resultStr);
        }

        private static string getCodeName(string code)
        {
            string codename = "";
            switch (code)
            {
                case "-5":
                    {
                        codename = "未收到图像";
                        break;
                    }
                case "-4":
                    {
                        codename = "图像不合格";
                        break;
                    }
                case "-3":
                    {
                        codename = "非食管部位";
                        break;
                    }
                case "-2":
                    {
                        codename = "待处理";
                        break;
                    }
                case "0":
                    {
                        codename = "疑似食管癌";
                        break;
                    }
                case "1":
                    {
                        codename = "不是食管癌";
                        break;
                    }
                case "2":
                    {
                        codename = "处理中";
                        break;
                    }
                case "3":
                    {
                        codename = "处理失败";
                        break;
                    }
                default:
                    break;
            }

            return codename;
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