using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Common
{
    public static class CommonMethod
    {

        public static int ConvertInt32(this string text, int deafultVal)
        {
            int result = 0;
            bool isSuccess = int.TryParse(text, out result);
            return isSuccess ? result : deafultVal;
        }

    }
}
