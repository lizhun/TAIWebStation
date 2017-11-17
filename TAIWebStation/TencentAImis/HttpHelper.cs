﻿using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace TencentAImis
{
    class HttpHelper
    {
        private string partnerId;
        private string token;
        private string baseUrl;

        private Tools tool;

        public HttpHelper(string partnerId, string token, string baseUrl, bool debug)
        {
            this.partnerId = partnerId;
            this.token = token;
            this.baseUrl = baseUrl;
            this.tool = new Tools(debug); 
        }

        private bool CheckValidationResult(object sender,
        X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;// Always accept
        }

        public Tuple<string,string> DoRequest(string action, string content)
        {
            try
            {
                string url = baseUrl + "/" + action + "/" + partnerId;
                string uuid = Guid.NewGuid().ToString();
                tool.Logger("请求正在执行, url:", url, "request id:", uuid);
                var webRequest = System.Net.WebRequest.Create(url);
                if (webRequest != null)
                {
                    webRequest.Method = "POST";
                    webRequest.Timeout = AImisEnum.TIMEOUT;
                    webRequest.ContentType = "application/json";
                    string vtime = tool.GetTimeStamp();
                    string signature = tool.CreateToken(partnerId + vtime, token);

                    webRequest.Headers
                        .Add("god-portal-signature", signature);
                    webRequest.Headers
                        .Add("god-portal-timestamp", vtime);
                    webRequest.Headers
                        .Add("god-portal-request-id", uuid);
                    webRequest.Headers
                        .Add("aimis-sdk-version", AImisEnum.SDK_VERSION);

                    tool.Logger("构造头部信息成功！头部信息：",webRequest.Headers.ToString());

                    using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        streamWriter.Write(content);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback =
                                new RemoteCertificateValidationCallback(CheckValidationResult);
                    }

                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            string jsonResponse = sr.ReadToEnd();
                            tool.Logger("请求发送成功！返回内容：", jsonResponse);
                            return Tuple.Create(uuid, jsonResponse);
                        }
                    }
                }
                else throw new WebException("初始化请求失败！请重新发起请求");
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    if (response != null)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        tool.Logger("请求发送时发生错误!!! 错误码/内容: ", httpResponse.StatusCode.ToString());
                        using (Stream data = response.GetResponseStream())
                        using (var reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            tool.Logger(text);
                            throw e;
                        }
                    }
                    else
                    {
                        tool.Logger("请求未发送完成，请检查网络连接！");
                        throw e;
                    }
                }
            }
        }
    }
}
