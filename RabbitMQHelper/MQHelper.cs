using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQHelper
{
    public class MQHelper<T>
    {
        private static string host_name = "localhost";
        private static int port = 5672;
        private static string user_name = "admin";
        private static string pass_word = "mobstaz";

        private const int max_connection_count = 100;
        private const ushort max_channel_count = 100;

        private const int ConnectionPoolSize = 50;

        private readonly static Semaphore ConnectionPoolSemaphore = new Semaphore(ConnectionPoolSize, ConnectionPoolSize,"MQConnectionPoolSemaphore");

        //分片数，表示逻辑队列背后的实际队列数
        private const int subdivisionCount = 10;  

        public static IConnection CreateConnection()
        {
            var connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = host_name;
            connectionFactory.Port = port;
            connectionFactory.UserName = user_name;
            connectionFactory.Password = pass_word;
            connectionFactory.RequestedChannelMax = max_channel_count;
            return connectionFactory.CreateConnection();
        }

        public static IConnection CreateConnectionPool()
        {
            ConnectionPoolSemaphore.WaitOne();

            IConnection conn = null;

        }

        /// <summary>
        /// 声明交换机
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="exchangename"></param>
        /// <param name="type"></param>
        /// <param name="durable"></param>
        /// <param name="autoDelete"></param>
        /// <param name="arguments"></param>
        private static void ExchangeDeclare(IModel channel,string exchangeName,string type,bool durable,bool autoDelete,IDictionary<string,object> arguments)
        {
            channel.ExchangeDeclare(exchangeName, type, durable, autoDelete, arguments);
        }

        /// <summary>
        /// 声明队列
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="queueName"></param>
        /// <param name="durable"></param>
        /// <param name="exclusive"></param>
        /// <param name="autoDelete"></param>
        /// <param name="arguments"></param>
        private static void QueueDeclare(IModel channel,string queueName,bool durable,bool exclusive,bool autoDelete,IDictionary<string,object> arguments)
        {
            for(int i = 0; i < subdivisionCount; i++)
            {
                string queue_name = queueName + "_" + i;
                channel.QueueDeclare(queue_name, durable, exclusive, autoDelete, arguments);
            }
        }

        /// <summary>
        /// 创建绑定关系
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="queue"></param>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="arguments"></param>
        private void QueueBind(IModel channel,string queue,string exchange,string routingKey,IDictionary<string,object> arguments)
        {
            for(int i = 0; i < subdivisionCount; i++)
            {
                string roukeyName = routingKey + "_" + i;
                string queueName = queue + "_" + i;
                channel.QueueBind(queueName, exchange, roukeyName, arguments);
            }
        }
    }
}
