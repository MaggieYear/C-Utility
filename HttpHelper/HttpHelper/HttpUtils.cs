using System;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Windows.Forms;

namespace HttpHelper
{
   public class HttpUtils
    {
       

        #region 无Cookie请求

        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";     
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

           // response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public static  string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion

        #region 带cookie请求

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";//浏览器  
        private static Encoding requestEncoding = System.Text.Encoding.UTF8;//字符集  
        //用于HTTPS的证书（自己生成的测试证书）
       private static string csr = "C:\\Users\\Administrator.DIY-20170222TLQ\\Documents\\C-Utility\\csr\\bisa.csr";

        /// <summary>    
        /// 创建GET方式的HTTP请求    
        /// </summary>    
        /// <param name="url">请求的URL</param>    
        /// <param name="timeout">请求的超时时间</param>    
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>    
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>    
        /// <returns></returns>    
        public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }


        /// <summary>    
        /// 创建POST方式的HTTP请求  
        /// </summary>    
        /// <param name="url">请求的URL</param>    
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>    
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>    
        /// <returns></returns>  
        public static HttpWebResponse CreatePostHttpResponse(string url, string parameters, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            HttpWebRequest request = null;
            Stream stream = null;//用于传参数的流  

            try
            {
                //如果是发送HTTPS请求    
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    //创建证书文件  
                    System.Security.Cryptography.X509Certificates.X509Certificate objx509 = new System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + @csr);
                    //添加到请求里  
                    request.ClientCertificates.Add(objx509);
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }

                request.Method = "POST";//传输方式  
                request.ContentType = "application/x-www-form-urlencoded";//协议                  
                request.UserAgent = DefaultUserAgent;//请求的客户端浏览器信息,默认IE                  
                request.Timeout = 6000;//超时时间，写死6秒  

                //随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空  
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }

                //如果需求POST传数据，转换成utf-8编码  
                byte[] data = requestEncoding.GetBytes(parameters);
                request.ContentLength = data.Length;

                stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);

                stream.Close();
            }
            catch (Exception ee)
            {
                //写日志  
                //LogHelper.  
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return request.GetResponse() as HttpWebResponse;
        }

        //验证服务器证书回调自动验证  
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受    
        }

        /// <summary>  
        /// 获取数据  
        /// </summary>  
        /// <param name="HttpWebResponse">响应对象</param>  
        /// <returns></returns>  
        public static string OpenReadWithHttps(HttpWebResponse HttpWebResponse)
        {
            Stream responseStream = null;
            StreamReader sReader = null;
            String value = null;

            try
            {
                // 获取响应流  
                responseStream = HttpWebResponse.GetResponseStream();
                // 对接响应流(以"utf-8"字符集)  
                sReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                // 开始读取数据  
                value = sReader.ReadToEnd();
            }
            catch (Exception)
            {
                //日志异常  
            }
            finally
            {
                //强制关闭  
                if (sReader != null)
                {
                    sReader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (HttpWebResponse != null)
                {
                    HttpWebResponse.Close();
                }
            }

            return value;
        }

        /// <summary>  
        /// 入口方法：获取传回来的XML文件  
        /// </summary>  
        /// <param name="url">请求的URL</param>    
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>    
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>    
        /// <returns></returns>  
        public static string GetResultXML(string url, string parameters, CookieCollection cookies)
        {
            return OpenReadWithHttps(CreatePostHttpResponse(url, parameters, cookies));
        }
        #endregion

    }
}
