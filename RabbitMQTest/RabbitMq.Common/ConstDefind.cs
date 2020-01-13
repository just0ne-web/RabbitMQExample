using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Common
{
    /// <summary>
    /// 常量管理类
    /// </summary>
    public class ConstDefind
    {
        #region  队列 账号

        /// <summary>
        /// 本地
        /// </summary>
        public const string LOCALHOST = "localhost";

        /// <summary>
        /// 队列地址
        /// </summary>
        public static string RABBIT_HOST = System.Configuration.ConfigurationManager.AppSettings["RabbbitHost"];

        /// <summary>
        /// 队列账号
        /// </summary>
        public static string RABBIT_USER = System.Configuration.ConfigurationManager.AppSettings["RabbitUser"];

        /// <summary>
        /// 队列密码
        /// </summary>
        public static string RABBIT_PASSWORD = System.Configuration.ConfigurationManager.AppSettings["RabbitPassword"];

        /// <summary>
        /// 通讯端口
        /// </summary>
        public static int RABBIT_PROT = System.Configuration.ConfigurationManager.AppSettings["RabbitProt"].ConvertInt32(5672);

        #endregion

        #region  队列交换机

        /// <summary>
        /// 交换机(频道)
        /// </summary>
        public const string RABBITMQEXANGE_FIRST = "EXANGE_FIRST";

        #endregion

        #region  队列名

        /// <summary>
        /// 队列名(路由)
        /// </summary>
        public const string RABBITMQNAME_FIRST = "RABBITMQNAME_FIRST";

        #endregion
    }
}
