using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpHelper
{
    class TimeUtils
    {

        
        public static long GetNowMillisecond()
        {

            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;

        }
    }
}
