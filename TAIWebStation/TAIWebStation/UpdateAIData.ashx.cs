using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TencentAImis;

namespace TAIWebStation
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
                Aimis manager = new Aimis(false);//1111
                StudyUploadRequest data = new StudyUploadRequest();
                var result = manager.StudyUpload(data);
                

            }
            else
            {
                context.Response.Write("Hello World");
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