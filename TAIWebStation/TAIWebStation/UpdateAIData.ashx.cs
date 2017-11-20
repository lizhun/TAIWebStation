using BLL;
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
                var formdata = context.Request.Form;
                var dbType = formdata["dbType"];
               
                Aimis manager = new Aimis(false);//1111
                TAIDbManager dbmanager = new TAIDbManager();
                StudyUploadRequest data = new StudyUploadRequest();
                List<ImageParams> imglist = new List<ImageParams>();
                data.Images = imglist;
                data.StudyId = Guid.NewGuid().ToString();
                data.StudyType = formdata["StudyType"];
                data.StudyName = formdata["StudyName"];
                data.PatientId = formdata["PatientId"];
                data.PatientName = formdata["PatientName"];
                data.PatientGender = formdata["PatientGender"];
                data.PatientBirthday = formdata["PatientBirthday"];
                data.StudyDate = (int)DateTime.Now.Ticks;
                var imgdatalist = formdata.AllKeys.Where(x => x.Contains("img_"));
                var imgids = imgdatalist.Select(x => x.Replace("img_", "")
                  .Replace("_content", "").Replace("_url", "")).Distinct().ToList();
                foreach (var imgdata in imgids)
                {
                    var imgitem = new ImageParams();
                    imgitem.Content = formdata["img_" + imgdata + "_content"];
                    imgitem.Url = formdata["img_" + imgdata + "Url"];
                    imgitem.ImageId = imgdata;
                    imglist.Add(imgitem);

                }
                var result = manager.StudyUpload(data);
                 
                var aa = "";

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