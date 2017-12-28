using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;

namespace HttpHelper
{

    //主要的执行类
    class Program
    {

        static void Main(string[] args)
        {
            long start_time = TimeUtils.GetNowMillisecond();
            long end_time = start_time + 120000;

            string postDataStr = "user_guid=729&username=yorkyhwe@bisa.com.hk&report_type=11&start_time=" + start_time + "&end_time=" + end_time;

            //开启报告
            CreateReport(postDataStr);
            /*
             * 开启报告之后上传心电数据              
             */

            string filepath = "C:\\Users\\Administrator.DIY-20170222TLQ\\Desktop\\ecd\\HC_001611000134_20170401171522.zip";
            //上传zip文件
           // SendFilePost(filepath);

            //下载zip
           // string Url = "http://hk-data.bisahealth.com/l/downData";

            string Url = "http://192.168.1.68:8080/health-data/l/downData";
            string getDataStr = "keyName=594/137649590b755372/";
            //http请求
            //get方法测试           
            string localFilePath = "C:\\Users\\Administrator.DIY-20170222TLQ\\Desktop\\ecd\\httptest";
           // bool result = HttpUtils.HttpDownload(Url, localFilePath);
           // Console.WriteLine(result);
            Console.ReadKey();

        }
        #region 开启报告
        /// <summary>
        /// 开启报告
        /// </summary>
        /// <param name="postDataStr"></param>
        private static void  CreateReport(string postDataStr)
            {
                string postUrl = "http://hk-server.bisahealth.com/l/create_report_public";
               
                //post方法返回json格式返回值
                string jsonResult = HttpUtils.HttpPost(postUrl, postDataStr);
                
                ReportDto reportDto = new ReportDto();
                //将json返回值转换成ReportDto对象
                reportDto = JsonToObjectUtils.JSONStringToList(jsonResult.ToString());
                          
                Console.WriteLine(jsonResult);

                //当返回状态码为205和402时，才会有报告返回
                if (reportDto.code == 205 )
                {
                    Console.WriteLine("Code:" + reportDto.code + "|Msg:" + reportDto.message + "\r\n");
                    Console.WriteLine("开启报告成功！");

            }
            else if ( reportDto.code == 402)
                {


                    Console.WriteLine(reportDto.code + ":" + reportDto.message + "\r\n" + reportDto.appReport.report_number);


                    int user_guid = 729;
                    string report_number = reportDto.appReport.report_number;
                    int report_status = 6;
                   
                /*
                 * 以防万一，我增加了一个手动修改报告状态的接口，设置报告状态status=6，即为失效状态。
                 * 如果开启报告一直报402，并返回了未上传数据的报告，此时你可以进行两种操作：
                 * 如需要使用该方法，请去掉注释，再启动。
                 */

                //1、补充上传这个报告的心电数据


                //2、设置这个报告的状态为失效report_status=6（用下面这个ChangeReportStatus方法）
                //ChangeReportStatus(user_guid, report_number, report_status);

            }
            else
                {
                    Console.WriteLine("Code:"+reportDto.code + "|Msg:" + reportDto.message + "\r\n");
                }
                

                Console.ReadKey();
               /*
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                Console.ReadKey();
            }
            */
        }

        public static void ChangeReportStatus(int user_guid, string report_number, int report_status)
        {
            string httpGetUrl = "http://hk-server.bisahealth.com/l/change_report_status";
            string getDataStr = "user_guid="+user_guid+"&report_number="+ report_number + "&report_status="+ report_status;
            string jsonResult = HttpUtils.HttpGet(httpGetUrl, getDataStr);

            ReportDto reportDto = new ReportDto();
            reportDto = JsonToObjectUtils.JSONStringToList(jsonResult.ToString());

            if (reportDto.code == 200)
            {
                Console.WriteLine("修改状态成功！修改状态为"+ report_status);
            }
            else
            {
                Console.WriteLine(jsonResult);
            }
            Console.ReadKey();
        }


#endregion
        public static bool SendFilePost(string filepath)
        {

            Console.WriteLine("SendFilePost>>>>");
            bool isok = false;
            try
            {

                //判断文件是否存在
                if (!File.Exists(filepath))
                {
                    Console.WriteLine("文件不存在");
                    return false;
                }

               /*

               string url = "http://hk-server.bisahealth.com/l/updateReportTime";

               NameValueCollection dicr = new NameValueCollection
               {
               {"user_guid", "729" },
                {"report_number", "4475d7e8eb7194649ecc4c2383ac3014" },
                 {"start_time", "120324" },
                  {"end_time", "125012" }
               };

 */

                 //string url = "http://hk-data.bisahealth.com/l/updat";
               string url = "http://192.168.1.68:8080/health-data/l/updat";

                NameValueCollection dicr = new NameValueCollection();
                dicr.Add("user_guid", "594");
                dicr.Add("report_type", "10");
                dicr.Add("uninid", "972a0e4964d7ed3fdea2946b98b1c451");
                dicr.Add("file_name", "HC_001611000134_20170401171522.zip");
                dicr.Add("is_close", "1");

                /*
            NameValueCollection dicr = new NameValueCollection
            {
            {"user_guid", "594" },
            { "report_type", "10" },
            { "uninid", "972a0e4964d7ed3fdea2946b98b1c451" },
            { "file_name", "HC_001611000134_20170401171522.zip" },
            { "is_close", "1" }
            };

         */

                string sttuas = HttpUtils.HttpPostData(url, 300000, "file", filepath, dicr);

                Console.WriteLine(sttuas);
            
                if (sttuas.IndexOf("10000") >= 0)
                {
                    isok = true;
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
            }
           
            Console.ReadKey();
            return isok;
        }

        public static void UploadZipFilePost(string LocalFilePath,string FileName,string ReportNumber,string ReportType)
        {
            try
            {          
            int IsClose = 0;
            //判断文件是否存在
            if (!File.Exists(LocalFilePath))
            {
                Console.WriteLine("文件不存在");
                return;           
            }
            //判断文件类型
            //如果是zip 

            IsClose = 0;
            //如果是ECD
            IsClose = 1;
            string uploadDataUrl = "https://hk-data.bisahealth.com/l/updat";
             }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                Console.ReadKey();
            }
        
        }

    }
}
