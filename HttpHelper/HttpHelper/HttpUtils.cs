using System;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Windows.Forms;
//using System.Web.HttpContext;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Collections.Specialized;

namespace HttpHelper
{
   public class HttpUtils
    {
        #region Http Post上传文件
        public static string HttpPostData(string url, int timeOut, string fileKeyName,
                                    string filePath, NameValueCollection stringDict)
        {
            var memStream = new MemoryStream();
            try
            {

            string responseContent;
            
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            // 边界符  
            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            // 边界符  
            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            // 最后的结束符  
            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");
            // 设置属性  
            webRequest.Method = "POST";
            webRequest.Timeout = timeOut;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            //上传文件，如果没有这一句会导致流关闭，出现无法连接远程服务器的问题。  
           // SetHeaderValue(webRequest.Headers, "Connection", "keep-alive");
            System.Net.ServicePointManager.DefaultConnectionLimit = 200;

             // 写入文件  
             const string filePartHeader =
                "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                 "Content-Type: application/octet-stream\r\n\r\n";
            var header = string.Format(filePartHeader, fileKeyName, filePath);
            var headerbytes = Encoding.UTF8.GetBytes(header);
            memStream.Write(beginBoundary, 0, beginBoundary.Length);
            memStream.Write(headerbytes, 0, headerbytes.Length);
            var buffer = new byte[1024];
            int bytesRead; // =0  
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }
            // 写入字符串的Key  
            var stringKeyHeader = "\r\n--" + boundary +
                                   "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                   "\r\n\r\n{1}\r\n";
            foreach (byte[] formitembytes in from string key in stringDict.Keys
                                             select string.Format(stringKeyHeader, key, stringDict[key])
                                                 into formitem
                                             select Encoding.UTF8.GetBytes(formitem))
            {
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }
            // 写入最后的结束边界符  
            memStream.Write(endBoundary, 0, endBoundary.Length);
            webRequest.ContentLength = memStream.Length;
            var requestStream = webRequest.GetRequestStream();
            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
           // memStream.Close();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
           // requestStream.Close();
            var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
            using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),
                  Encoding.GetEncoding("utf-8")))
            {
                responseContent = httpStreamReader.ReadToEnd();
            }
            memStream.Close();

            requestStream.Close();

            httpWebResponse.Close();

            fileStream.Close();

            webRequest.Abort();

                return responseContent;
            }
            catch (WebException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Status);
                Console.WriteLine(e.Message);

                Console.ReadKey();
            }
            finally
            {
                memStream.Close();
            }
            return null;
        }
        /// <summary>
        /// 修改请求头的Host/Connection的值
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        #endregion

        #region Http Get 下载文件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="localFilePath"></param>
        /// <returns></returns>
        public static string HttpGetData(string url,string localFilePath) 
        {
            string name = url.Substring(url.LastIndexOf('/') + 1);//获取名字
            string fileFolder = localFilePath; //UploadConfigContext.UploadPath;
                         string filePath = Path.Combine(fileFolder, name);//存放地址就是本地的upload下的同名的文件
                         if (!Directory.Exists(fileFolder))
                                 Directory.CreateDirectory(fileFolder);
            
             string returnPath = GetSimplePath(filePath);//需要返回的路径
                         if (File.Exists(filePath))
                             {//如果已经存在，那么就不需要拷贝了，如果没有，那么就进行拷贝
                                 return returnPath;
                             }
                         HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                         request.Method = "GET";
                         request.ProtocolVersion = new Version(1, 1);
                         HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                         if (response.StatusCode == HttpStatusCode.NotFound)
                             {
                                 return string.Empty;//找不到则直接返回null
                             }
                         // 转换为byte类型
             System.IO.Stream stream = response.GetResponseStream();
            

             //创建本地文件写入流
             Stream fs = new FileStream(filePath, FileMode.Create);
                         byte[] bArr = new byte[1024];
                         int size = stream.Read(bArr, 0, (int)bArr.Length);
                        while (size > 0)
                             {
                                 fs.Write(bArr, 0, size);
                                 size = stream.Read(bArr, 0, (int)bArr.Length);
                             }
                         fs.Close();
                         stream.Close();
                         return returnPath;
        }

        public static string GetSimplePath(string path)
        {
            //E:\Upload\cms\day_150813\1.jpg
            path = path.Replace(@"\", "/");
            int pos = path.IndexOf("downData");
            if (pos != -1)
            {
                pos = pos - 1;//拿到前面那个/,这样为绝对路径，直接保存在整个项目下的upload文件夹下
                return path.Substring(pos, path.Length - pos);
            }
            return "";
        }


        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public static bool HttpDownload(string url, string path)
        {
            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".zip"; //临时文件
            Console.WriteLine("tempFile:" + tempFile);
            if (System.IO.File.Exists(tempFile))
            {
                Console.WriteLine("删除文件");
                System.IO.File.Delete(tempFile);    //存在则删除
            }
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                Console.WriteLine("流大小："+size);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                //System.IO.File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Console.ReadKey();
            }
            
        }

        #endregion

        #region http get （json格式请求参数）

        #endregion

        #region   #region http post （json格式请求参数）
        
        
        #endregion


        #region Http跨域请求
        /*
          public void sendPost(string url ,string urlArgs, HttpContext context)
          {
              //context.Request["args"]  
              System.Net.WebClient wCient = new System.Net.WebClient();
              wCient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
              byte[] postData = System.Text.Encoding.ASCII.GetBytes("id=" + urlArgs+"&");

              byte[] responseData = wCient.UploadData(url, "POST", postData);

              string returnStr = System.Text.Encoding.UTF8.GetString(responseData);//返回接受的数据   

              context.Response.ContentType = "text/plain";
              context.Response.Write(returnStr);
          }
          */

        #endregion

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
