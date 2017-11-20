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
            Aimis manager = new Aimis(false);//1111
            var paras = new AIResultRequest();
            paras.StudyId = "46774cf5-3b1d-41c2-9e18-69b1205e57bc";
            var result = manager.GetAIResult(paras);
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