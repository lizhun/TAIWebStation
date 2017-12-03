using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TencentAImis;

namespace TAIFrontWebStation
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
                var formdata = context.Request.Form;
            
                Aimis manager = new Aimis(false);//1111
                var data = ContextToAIRequst(context);
                var result = manager.StudyUpload(data);
                context.Response.Write(result.ToString());         
            }
            else
            {
                context.Response.Write("Only support post metod!");
            }

        }

        private StudyUploadRequest ContextToAIRequst(HttpContext context)
        {
            var formdata = context.Request.Form;
            StudyUploadRequest data = new StudyUploadRequest();
            List<ImageParams> imglist = new List<ImageParams>();
            data.Images = imglist;
            data.StudyId = Guid.NewGuid().ToString();
            data.StudyType = formdata["StudyType"]?.Trim();
            data.StudyName = formdata["StudyName"]?.Trim();
            data.PatientId = formdata["PatientId"]?.Trim();
            data.PatientName = formdata["PatientName"]?.Trim();
            data.PatientGender = formdata["PatientGender"]?.Trim();
            data.PatientBirthday = formdata["PatientBirthday"]?.Trim();
            var sdata = formdata["StudyDate"].Trim();
            data.StudyDate = (int)(DateTime.Parse(sdata).Ticks - DateTime.Parse("1970-1-1").Ticks);
            var imgdatalist = formdata.AllKeys.Where(x => x.Contains("img_"));
            var imgids = imgdatalist.Select(x => x.Replace("img_", "")
              .Replace("_content", "").Replace("_url", "")).Distinct().ToList();
            foreach (var imgdata in imgids)
            {
                var imgitem = new ImageParams();
                imgitem.Content = formdata["img_" + imgdata + "_content"]?.Trim();
                imgitem.Url = formdata["img_" + imgdata + "url"]?.Trim();
                imgitem.ImageId = imgdata;
                imglist.Add(imgitem);

            }
            return data;
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