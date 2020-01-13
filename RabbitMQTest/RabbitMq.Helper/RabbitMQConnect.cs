using RabbitMq.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Helper
{
    public class RabbitMQConnect
    {
        public static ConnectionFactory factory = null;
        static string _rabbitHost = ConstDefind.RABBIT_HOST;
        static string _rabbitUser = ConstDefind.RABBIT_USER;
        static string _rabbitPwd = ConstDefind.RABBIT_PASSWORD;
        static int _rabbitProt = ConstDefind.RABBIT_PROT;
        static RabbitMQConnect()
        {
            if (_rabbitHost == ConstDefind.LOCALHOST)
            {
                factory = new ConnectionFactory() { HostName = _rabbitHost };
            }
            else
            {
                factory = new ConnectionFactory() { HostName = _rabbitHost, UserName = _rabbitUser, Password = _rabbitPwd, Port = _rabbitProt };
            }
        }
    }
}
