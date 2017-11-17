using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TencentAImis
{
    public class AImisEnum
    {
        /**
         * 患者性别（0未知 1 男 2 女 3 其他）
         */
        public static class PATIENT_GENDER
        {

            public static readonly string UNKNOWN = "0";
            public static readonly string MAN = "1";
            public static readonly string WOMAN = "2";
            public static readonly string OTHER = "3";
        }

        /**
         * 眼底必须上传，描述图像对应的部位，眼底需要传
           未知：0，左眼：1， 右眼：2。其他待定
         */
        public static class DESC_POSITION
        {
            public static readonly string UNKNOWN = "0";
            public static readonly string LEFT_EYE = "1";
            public static readonly string RIGHT_EYE = "2";
            public static readonly string ESOPHAGUS = "3";
            public static readonly string LUNG = "4";
        }

        /**
	     * -1 不知道该序列是否传完  
 	 	   0该序列没有传完
	 	   1该序列已传完
	     */
        public static class DICOM_UPLOAD_FLAG
        {
            public static readonly string UNKNOWN = "-1";
            public static readonly string UPLOADING = "0";
            public static readonly string DONE = "1";
        }

        public static readonly string SDK_VERSION = "c#_sdk_1.0.0";

        public static readonly int TIMEOUT = 10000;
    }
}
