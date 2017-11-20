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
        public void SaveStudyUpload(string type, string studyId,string patId)
        {
            using (var con = new SqlConnection(this.GetDbCon(type.ToUpper())))
            {

                var sqlparams = new SqlParameter[] { new SqlParameter("StudyId", studyId) ,
                    new SqlParameter("@PatId", patId)};
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, @"Insert into TB_AckAntCVResult (AntCVResultID,ReportType,ExecDocCode,ExecDocName,ExecDate,ExecTime,StudyNo,LastUpateTime,Status) 
                                         VALUES (@AntCVResultID,@ReportType,@ExecDocCode,@ExecDocName,@ExecDate,@ExecTime,@StudyNo,@LastUpateTime,@Status)", sqlparams);


                con.Close();
            }

        }
    }
}