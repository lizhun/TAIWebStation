using System;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace TencentAImis
{
    class Tools
    {
        private bool isDebug;
        private JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public Tools(bool isDebug)
        {
            this.isDebug = isDebug;
        }

        public void Logger(params string[] content)
        {
            if (isDebug)
            {
                for (int i = 0; i < content.Length; i++)
                {
                    Console.WriteLine(content[i]);
                }
            }
        }
        public string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashmessage).Replace("-", string.Empty).ToLower();
            }
        }
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        public string EncodeJson(BaseRequest param)
        {
            try
            {
                return JsonConvert.SerializeObject(param, Formatting.Indented, Settings);
            }
            catch (JsonSerializationException e)
            {
                Logger("编码json失败，错误信息：", e.StackTrace);
                throw e;
            }
        }
        public T DecodeJson<T>(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString, Settings);
            }
            catch (JsonReaderException e)
            {
                Logger("解码json失败，错误信息：", e.StackTrace);
                throw e;
            }
        }
        public BaseResponse AddRequestId(BaseResponse res, string id)
        {
            res.Message += "|requestId:" + id;
            return res;
        }
    }
}
