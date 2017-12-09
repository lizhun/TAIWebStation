using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TencentAImis;

namespace TAIFrontWebStation
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class GetAIResult : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string tencentUrl = AppSettings.TAIDetailUrl;
            string parentId = AppSettings.PartnerId;
            context.Response.ContentType = "text/plain";
            string resultStr = "";// "1$中就事论事$http://www.baidu.com";
            string code = "", codename = "处理失败";
            var form = context.Request.Form;
            var StudyId = form["StudyId"]?.Trim();
             
            Aimis manager = new Aimis(false);//1111
            var paras = new AIResultRequest();
            paras.StudyId = StudyId;
            string url = string.Format("{0}/studydetail?partnerId={1}&studyId={2}",
               tencentUrl, parentId, paras.StudyId);
            var result = manager.GetAIResult(paras);
            if (result.Code == 0)            {
                
                code = result.Data.Result.ToString();
                codename = getCodeName(code);
            }
            else
            {
                code = result.Code.ToString();
                codename = result.Message;
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