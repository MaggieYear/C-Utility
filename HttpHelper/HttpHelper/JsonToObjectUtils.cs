using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace HttpHelper
{
    /// <summary>
    /// json格式字符串转换成对象
    /// </summary>
    public class JsonToObjectUtils
    {
        /*
        static void Main(string[] args)
        {
            string jsonStr = "{\"code\":205,\"message\":\"Operation successfully\",\"appReport\":{\"id\":0,\"report_number\":\"f7a7214d355514688fb8bdd3e4303f66\"," +
                "\"report_type\":11,\"report_status\":4,\"start_time\":1514260672011,\"end_time\":1514260762252,\"user_guid\":729}}";

           /*
            string objTOJson = GetJsonString();
             Console.WriteLine(objTOJson);

            Console.ReadKey();
           */
           /*
           JSONStringToList(jsonStr);
          

        }
    */
    

        public static string MapToJsonStr(Map map)
        {

        }

        //对象与数组转JSON
        public static string GetJsonString()
        { 
            //初始化对象
            AppReport aReport = new AppReport()
            {
                id = 1,
                report_number = "sdkjfslfj",
                report_type = 10,
                report_status = 4,
                start_time = 1514260672011,
                end_time = 1514260672011,
                user_guid = 729
            };

            ReportDto reportDto = new ReportDto() { code=200,message="success", appReport=aReport };

          
            //序列化
            string objTOJson = new JavaScriptSerializer().Serialize(reportDto);

            return objTOJson;

            //数组转json
            /*
            List<obj> products = new List<obj>(){
            new Obj(){Name="苹果",Price=5.5},
            new Obj(){Name="橘子",Price=2.5},
            new Obj(){Name="干柿子",Price=16.00}
            };

            ProductList productlist = new ProductList();
            productlist.GetProducts = products;
            //序列化
            string os = new JavaScriptSerializer().Serialize(productlist);
            //输出 "{\"GetProducts\":[{\"Name\":\"苹果\",\"Price\":5.5},{\"Name\":\"橘子\",\"Price\":2.5},{\"Name\":\"干柿子\",\"Price\":16}]}"
            */
        }


      

        //json转对象、数组, 反序列化
        public static ReportDto JSONStringToList(string JsonStr)
        {

            //json格式字符串
            JavaScriptSerializer Serializer = new JavaScriptSerializer();

            //json字符串转为对象, 反序列化

            ReportDto reportDto = Serializer.Deserialize<ReportDto>(JsonStr);
           
            return reportDto;
            /*
            //json格式字符串
            string JsonStrs = "[{Name:'苹果',Price:5.5},{Name:'橘子',Price:2.5},{Name:'柿子',Price:16}]";

            JavaScriptSerializer Serializers = new JavaScriptSerializer();

            //json字符串转为数组对象, 反序列化
            List<obj> objs = Serializers.Deserialize<list<obj>>(JsonStrs);

            foreach (var item in objs)
            {
                Console.Write(item.Name + ":" + item.Price + "\r\n");
            }
            */
        }
        
    }
}
