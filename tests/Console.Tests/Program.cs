using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Threading;
namespace TestRedisConnection
{
    public class Incr
    {
        public long Id { get; set; }
    }

    public class IncrResponse
    {
        public long Result { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //new LongRunningRedisPubSubServer().Execute("10.0.0.9");
            //new HashStressTest().Execute("127.0.0.1");
            //new HashStressTest().Execute("10.0.0.9");

            // new HashCollectionStressTests().Execute("10.0.0.9", noOfThreads: 64);

            List<string> hosts = new List<string>();
            hosts.Add("192.168.60.33:22200");
            hosts.Add("192.168.60.34:22200");
            using (var redisSentinel = new RedisSentinel(hosts, "master1"))
            {
                while (true)
                {

                    using (var redis = redisSentinel.Setup())
                    {
                        using (RedisClient client = (RedisClient)redis.GetClient())
                        {
                            client.ChangeDb(15);
                            var data = client.Lists["vms01371|io|svctm|sda2"].GetRange(0, 10);
                            Console.WriteLine("150000==" + data.Count);
                            // }
                            //}

                            //using (var redis = redisSentinel.Setup())
                            //{
                            // using (var client = redis.GetClient())
                            // {
                            client.ChangeDb(10);
                            var data2 = client.Lists["svr4911hp580|logins/sec"].GetRange(0, 10);
                            Console.WriteLine("100000==" + data2.Count);
                        }
                    }

                    Thread.Sleep(1000);
                }
            }

            Console.ReadLine();

        }
    }
}
