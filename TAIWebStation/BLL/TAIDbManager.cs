using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TencentAImis;

namespace BLL
{
    public class TAIDbManager
    {
        private string GetDbCon(string type)
        {
            string result = "";
            string dbname = string.Format("DB_{0}", type);
            result = ConfigurationManager.ConnectionStrings[dbname].ConnectionString;
            return result;

        }
        public void SaveStudyUpload(string type, string studyId, string patId, List<string> imageids)
        {
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {
                try
                {
                    var sqlparams = new SqlParameter[] { new SqlParameter("@StudyId", studyId) ,
                    new SqlParameter("@PatId", patId)};
                    SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"declare  @datacount int;
set @datacount =(select COUNT(*) from TB_TAIUploadHistory where PatId=@PatId);
if @datacount >0 
begin
 update TB_TAIUploadHistory set UpdateTime=GETDATE() where PatId=@PatId 
end
else
begin
Insert into TB_TAIUploadHistory 
(StudyId,PatId,SendStatus,CreateTime,UpdateTime)   VALUES (@StudyId,@PatId,1,GETDATE(),GETDATE())
end;", sqlparams);
                    foreach (var img in imageids)
                    {
                        var imgparams = new SqlParameter[] { new SqlParameter("@StudyId", studyId) ,
                    new SqlParameter("@ImageId", img)};
                        SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"declare  @datacount int;
set @datacount =(select COUNT(*) from TB_TAIUploadImageHistory where StudyId=@StudyId and imageid=@ImageId);
if @datacount >0 
begin
 update TB_TAIUploadImageHistory set UpdateTime=GETDATE() where StudyId=@StudyId and imageid=@ImageId
end
else
begin
Insert into TB_TAIUploadImageHistory 
(StudyId,ImageId)   VALUES (@StudyId,@ImageId)
end;", imgparams);

                    }
                }
                catch (Exception e)
                {

                    var aa = e;
                }


                con.Close();

            }
        }

        public void SaveAIResult(string type, string studyId, int resultCode)
        {
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {
                var sqlparams = new SqlParameter[] { new SqlParameter("@StudyId", studyId) ,
                    new SqlParameter("@ResultCode", resultCode)};
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"Update TB_TAIUploadHistory 
                 set ResultCode=@ResultCode where StudyId=@StudyId", sqlparams);
                con.Close();
            }
        }

        public string GetStudyIdByPatId(string type, string patId)
        {
            string studyId = Guid.NewGuid().ToString();
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {
                var sqlparams = new SqlParameter[] { new SqlParameter("@PatId", patId) };
                var data = SqlHelper.ExecuteScalar(con, CommandType.Text, @"select StudyId from TB_TAIUploadHistory
                where patId = @PatId order by CreateTime desc", sqlparams);
                if (data != null)
                {
                    studyId = data.ToString();
                }
                con.Close();
            }
            return studyId;
        }
    }
}