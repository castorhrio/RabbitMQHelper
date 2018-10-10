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
            //List<int> list = new List<int>();
            //for(int i = 0; i < 1000; i++)
            //{
            //    list.Add(i);
            //}

            //new Task(() =>
            //{
            //    if (list.Count > 0)
            //    {
            //        Console.WriteLine(list.Last().ToString());
            //        Console.ReadKey();
            //    }
            //}).Start();

            //Console.WriteLine("qqqqqq");
            //Console.ReadKey();

            List<string> list = new List<string>();
            for(int i = 0; i <= 9; i++)
            {
                for(int j = 0; i <= 9; j++)
                {
                    for(int m = 0; m <= 9; m++)
                    {
                        for(int n = 0; n <= 9; n++)
                        {
                            string qq = string.Format("95746{0}{1}{2}{3}@qq.com", i, j, m, n);
                            list.Add(qq);
                        }
                    }
                }
            }
        }
    }
}
