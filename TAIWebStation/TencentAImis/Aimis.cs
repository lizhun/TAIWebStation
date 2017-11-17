
namespace TencentAImis
{
    public class Aimis
    {
        private HttpHelper HttpHelper;
        private Tools tool;

        /**
	     * 初始化
	     * @param partnerId 分配的合作方id
	     * @param token 分配的合作方密钥
	     * @param baseUrl 腾讯服务器url地址
	     * @param debug debug模式为调试模式,输出打印日志
	     */
        public Aimis(string partnerId, string token, string url, bool isDebug=false)
        {
            HttpHelper = new HttpHelper(partnerId, token, url, isDebug);
            tool = new Tools(isDebug);
        }

        /**
         * 检查信息上传接口 非CT
         * @param param 请求参数
         * @return 
         * @throws Exception
         */
        public StudyUploadResponse StudyUpload(StudyUploadRequest param)
        {
            string action = "studyupload";
            string toBeSend = tool.EncodeJson(param);
            var resTuple = HttpHelper.DoRequest(action, toBeSend);
            var res = tool.DecodeJson<StudyUploadResponse>(resTuple.Item2);
            return (StudyUploadResponse)tool.AddRequestId(res, resTuple.Item1);
        }

        /**
         * 检查AI结果查询接口 非CT
         * @param param 请求参数
         * @return 
         * @throws Exception
         */
        public AIResultResponse GetAIResult(AIResultRequest param)
        {
            string action = "airesult";
            string toBeSend = tool.EncodeJson(param);
            var resTuple = HttpHelper.DoRequest(action, toBeSend);
            var res = tool.DecodeJson<AIResultResponse>(resTuple.Item2);
            return (AIResultResponse)tool.AddRequestId(res, resTuple.Item1);
        }

        /**
	     * 检查AI CT结果查询接口
	     * @param param 请求参数
	     * @return 
	     * @throws Exception
	     */ 
        public DicomResultResponse GetDicomAIResult(DicomResultRequest param)
        {
            string action = "dicomresult";
            string toBeSend = tool.EncodeJson(param);
            var resTuple = HttpHelper.DoRequest(action, toBeSend);
            var res = tool.DecodeJson<DicomResultResponse>(resTuple.Item2);
            return (DicomResultResponse)tool.AddRequestId(res, resTuple.Item1);
        }

        /**
         * 检查报告上传接口
         * @param param 请求参数
         * @return 
         * @throws Exception
         */
        public ReportUploadResponse ReportUpload(ReportUploadRequest param)
        {
            string action = "reportupload";
            string toBeSend = tool.EncodeJson(param);
            var resTuple = HttpHelper.DoRequest(action, toBeSend);
            var res = tool.DecodeJson<ReportUploadResponse>(resTuple.Item2);
            return (ReportUploadResponse)tool.AddRequestId(res, resTuple.Item1);
        }

        /**
         * 检查信息上传接口 CT
         * @param param 请求参数
         * @return 
         * @throws Exception
         */
        public UploadDicomResponse UploadDicom(UploadDicomRequest param)
        {
            string action = "uploadDicom";
            string toBeSend = tool.EncodeJson(param);
            var resTuple = HttpHelper.DoRequest(action, toBeSend);
            var res = tool.DecodeJson<UploadDicomResponse>(resTuple.Item2);
            return (UploadDicomResponse)tool.AddRequestId(res, resTuple.Item1);
        }
    }
}
