using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TencentAImis
{
    /**
	 * 参数对象基类
	 */
    public abstract class BaseRequest { }

    /**
	 * 非CT类检查信息上传接口参数类
	 * @param StudyId 本次检查在医院侧的序列号
	 * @param StudyType 检查类型
	 * @param StudyDate 检查日期，返回1970年1月1日至今的时间戳，单位秒
	 * @param StudyName 检查名称
     * @param PatientGender 患者性别
     * @param PatientBirthday 患者生日，格式2017-08-22
     * @param Images 图片对象列表
	 */
    public class StudyUploadRequest : BaseRequest
    {
        [JsonProperty("studyId")]
        public string StudyId { get; set; }

        [JsonProperty("studyType")]
        public string StudyType { get; set; }

        [JsonProperty("studyDate")]
        public int StudyDate { get; set; }

        [JsonProperty("studyName")]
        public string StudyName { get; set; }

        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        [JsonProperty("patientGender")]
        public string PatientGender { get; set; }

        [JsonProperty("patientBirthday")]
        public string PatientBirthday { get; set; }

        [JsonProperty("images")]
        public List<ImageParams> Images { get; set; }
    }

    /**
	 * 非CT类检查信息结果查询接口参数类
	 * @param StudyId 本次检查在医院侧的序列号
	 */
    public class AIResultRequest : BaseRequest
    {
        [JsonProperty("studyId")]
        public string StudyId { get; set; }
    }

    /**
     * CT类检查信息结果查询接口参数类
     * @param StudyId 本次检查在医院侧的序列号
     */
    public class DicomResultRequest : BaseRequest
    {
        [JsonProperty("studyId")]
        public string StudyId { get; set; }
    }

    /**
     * 检查报告上传接口参数类
     * @param StudyId 本次检查在医院侧的序列号
     * @param Conclusion 图像描述的内容
     * @param ImageDescribe 诊断内容
     */
    public class ReportUploadRequest : BaseRequest
    {
        [JsonProperty("studyId")]
        public string StudyId { get; set; }

        [JsonProperty("conclusion")]
        public string Conclusion { get; set; }

        [JsonProperty("imageDescribe")]
        public string ImageDescribe { get; set; }
    }

    /**
	 * CT类检查信息上传接口参数类
	 * @param StudyId 本次检查在医院侧的序列号
	 * @param StudyType 检查类型
	 * @param StudyName 检查名称
     * @param PatientName 患者姓名
     * @param SeriesNo 医院测序列号
     * @param UploadDone 上传状态
     * @param Images 图片对象列表
	 */
    public class UploadDicomRequest : BaseRequest
    {
        [JsonProperty("studyId")]
        public string StudyId { get; set; }

        [JsonProperty("studyType")]
        public string StudyType { get; set; }

        [JsonProperty("studyName")]
        public string StudyName { get; set; }

        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        [JsonProperty("seriesNo")]
        public string SeriesNo { get; set; }

        [JsonProperty("uploadDone")]
        public string UploadDone { get; set; }

        [JsonProperty("images")]
        public List<ImageParams> Images { get; set; }
    }

    /**
     * CT/非CT类检查信息上传接口图片列表对象
     * @param ImageId 图片在医院侧编号
     * @param Url 影像在医院侧内网地址
     * @param DescPosition 描述图像对应的部位，眼底必须上传
     */
    public class ImageParams
    {
        [JsonProperty("imageId")]
        public string ImageId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("descPosition")]
        public string DescPosition { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    /**
     * 所有接口返回对象基类
     * @param Code 返回状态码，详见状态码相关文档
     * @param Message 返回信息
     */
    public abstract class BaseResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    /**
     * 非CT类检查结果查询接口返回类
     * @param Data 返回结果
     */
    public class AIResultResponse : BaseResponse
    {
        [JsonProperty("data")]
        public AIResultResData Data { get; set; }
    }

    /**
     * 非CT类检查结果查询返回结果对象类
     * @param StudyId 医院侧检查编号
     * @param Result 返回结果，具体参考返回结果码相关文档
     */
    public class AIResultResData
    {
        [JsonProperty("studyId")]
        public string StudyId { get; set; }

        [JsonProperty("result")]
        public int Result { get; set; }
    }

    /**
     * 非CT类检查信息上传接口返回结果对象类
     */
    public class StudyUploadResponse : BaseResponse
    {
    }

    /**
     * CT类检查结果查询接口返回结果类
     */
    public class DicomResultResponse : BaseResponse
    {
        [JsonProperty("data")]
        public DicomResultResData Data { get; set; }
    }

    /**
     * CT类检查结果查询接口返回结果对象类
     * @param StudyId 医院侧检查编号
     * @param MarkList 标注列表
     * @param Result 返回结果，具体参考返回结果码相关文档
     */
    public class DicomResultResData
    {
        [JsonProperty("studyId")]
        public string StudyId { get; set; }

        [JsonProperty("markList")]
        public List<DicomMark> MarkList { get; set; }

        [JsonProperty("result")]
        public int Result { get; set; }
    }

    /**
     * CT类检查结果标注类
     * @param No mark序列号
     * @param shape 标注形状，1：圆
     * @param shapeDesc 形状描述，详见相关文档
     * @param dcmNoList dcm序列,逗号隔开
     * @param MarkType 标注类型，1：AI标注，2：医生标注
     * @param Desc 检查描述和说明
     * @param SeriesNo 对应序列在检查中的编号
     * @param seriesUid 对应序列的唯一Uid
     */
    public class DicomMark
    {
        [JsonProperty("no")]
        public string No { get; set; }

        [JsonProperty("shape")]
        public int Shape { get; set; }

        [JsonProperty("shapeDesc")]
        public string ShapeDesc { get; set; }

        [JsonProperty("dcmNoList")]
        public string DcomNoList { get; set; }

        [JsonProperty("markType")]
        public int MarkType { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("seriesNo")]
        public int SeriesNo { get; set; }

        [JsonProperty("seriesUid")]
        public string SeriesUid { get; set; }
    }

    /**
     * 报告上传返回结果类
     */
    public class ReportUploadResponse : BaseResponse
    {
    }

    /**
     * CT类检查信息上传接口返回结果对象类
     */
    public class UploadDicomResponse : BaseResponse
    {
    }
}
