using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            for(int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }
          
            new Task(() =>
            {
                if (list.Count > 0)
                {
                    Console.WriteLine(list.Last().ToString());
                    Console.ReadKey();
                }
            }).Start();

            Console.WriteLine("qqqqqq");
            Console.ReadKey();
        }
    }
}
