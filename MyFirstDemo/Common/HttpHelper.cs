using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class HttpHelper
    {
        /// <summary>
        /// post请求数据
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="requestString">post数据</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="contentType">传输格式如：application/json</param>
        /// <param name="nameValues">增加的header</param>
        /// <returns>返回数据</returns>
        public static string Post(string url,string requestString,Encoding encoding,string contentType, NameValueCollection nameValues)
        {
            var result = string.Empty;
            try
            {
                var mRequest = (HttpWebRequest)WebRequest.Create(url);
                var data = Encoding.UTF8.GetBytes(requestString);
                mRequest.Method = "Post";
                mRequest.ContentType = contentType;
                mRequest.ContentLength = data.Length;
                mRequest.Headers.Add(nameValues);
                var requestStream = mRequest.GetRequestStream();
                requestStream.Write(data,0,data.Length);
                requestStream.Close();

                var mResponse = (HttpWebResponse)mRequest.GetResponse();
                var responseStream = mResponse.GetResponseStream();
                if (responseStream != null)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream,encoding))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
                responseStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// post请求数据
        /// httpClient.DefaultRequestHeaders.Add("Host", "www.oschina.net");
        //  httpClient.DefaultRequestHeaders.Add("Method", "Post");
        //  httpClient.DefaultRequestHeaders.Add("KeepAlive", "false");   // HTTP KeepAlive设为false，防止HTTP连接保持
        //  httpClient.DefaultRequestHeaders.Add("UserAgent",
        //   "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11");
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string postData,TimeSpan timeout)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = timeout;
                httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                var stringContent = new StringContent(postData, Encoding.UTF8,"application/json");
                var httpResponse = await httpClient.PostAsync(url, stringContent);
                if (httpResponse.IsSuccessStatusCode)
                {
                    Decompress(httpResponse);
                    return await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return string.Empty;
        }

        private static void Decompress(HttpResponseMessage response)
        {
            if (response.Content.Headers.Contains("gzip"))
            {
                var decompressedStream = new MemoryStream();
                using (var gzipStream = new GZipStream(response.Content.ReadAsStreamAsync().Result, CompressionMode.Decompress))
                {
                    gzipStream.CopyToAsync(decompressedStream);
                }
                decompressedStream.Seek(0, SeekOrigin.Begin);
                response.Content = new StreamContent(decompressedStream);
            }
        }

        public static async Task<string> GetAsync(string url, int timeOut = 10000)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 0, 0, 0, timeOut);
                var httpResponse = await httpClient.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 压缩post数据
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="content">参数信息</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>结果</returns>
        public static async Task<(bool,byte[])> PostCompressAsync(string url, string content, TimeSpan timeout)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = timeout;
                httpClient.DefaultRequestHeaders.Add("Accep-Encoding", "gzip, br");
                var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                var httpResponse = await httpClient.PostAsync(url, stringContent);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var bytes = await httpResponse.Content.ReadAsByteArrayAsync();
                    return (httpResponse.Content.Headers.ContentEncoding.Contains("gzip"), bytes);
                }
            }
            return (false, new byte[0]);
        }
    }
}
