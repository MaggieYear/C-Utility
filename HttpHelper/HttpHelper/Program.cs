using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HttpHelper
{

    //主要的执行类
    class Program
    {

        static void Main(string[] args)
        {
            /*
            try
            {
            */
                string Url = "http://localhost:8080/web-http/l/HttpWebRequestGet";

                long start_time = TimeUtils.GetNowMillisecond();
                long end_time = start_time + 120000;
                //string postDataStr = "user_guid=729&username=yorkyhwe@bisa.com.hk&report_type=11&start_time=1514181372958&end_time=1514181872958";
                string postDataStr = "user_guid=729&username=yorkyhwe@bisa.com.hk&report_type=11&start_time=" + start_time + "&end_time=" + end_time;
                //http请求
                //get方法测试
                // string result = HttpGet(Url, postDataStr);

                // string postUrl = "http://localhost:8080/web-http/l/HttpWebRequestPOST";
                string postUrl = "http://hk-server.bisahealth.com/l/create_report_public";
                //post方法测试
                string jsonResult = HttpUtils.HttpPost(postUrl, postDataStr);

                ReportDto reportDto = new ReportDto();
                reportDto = JsonToObjectUtils.JSONStringToList(jsonResult.ToString());
                  
                Console.WriteLine(jsonResult);

                //当返回状态码为205和402时，才会有报告返回
                if (reportDto.code == 205 )
                {
                    Console.WriteLine("Code:" + reportDto.code + "|Msg:" + reportDto.message + "\r\n");
                    Console.WriteLine("开启报告成功！");
                /*
                 * 开启报告之后上传心电数据
                 * 这里我写了请求上传心电数据的方法，修改zip文件的本地路径，去掉注释就可以联动运行。
                 */
                string downDataUrl = "http://hk-data.bisahealth.com/l/downData";
              

                 UploadZipFilePost();

            }
            else if ( reportDto.code == 402)
                {
                    Console.WriteLine(reportDto.code + ":" + reportDto.message + "\r\n" + reportDto.appReport.report_number);


                    int user_guid = 729;
                    string report_number = reportDto.appReport.report_number;
                    int report_status = 6;
                    Console.WriteLine("修改报告状态...");
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


        public static void UploadZipFilePost(string LocalFilePath,string FileName,string ReportNumber,string ReportType)
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
            //
        }

    }
}
