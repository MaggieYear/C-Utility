using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpHelper
{
     public class ReportDto
    {
        public int code { get; set; }
        public string message { get; set; }
        public AppReport appReport { get; set; }
        
        public ReportDto()
        {

        }
    }

    public class AppReport
    {
        public int id { get; set; }
        public string report_number { get; set; }
        public int report_type { get; set; }
        public int report_status { get; set; }
        public long start_time { get; set; }
        public long end_time { get; set; }
        public int user_guid { get; set; }

        public AppReport()
        {

        }

    }
}
