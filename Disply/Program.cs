using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Disply
{
    class Program
    {
        public static Action Step = null;
        public static bool pause = false;
        static void Main(string[] args)
        {
            Random srand = new Random();
            Stopwatch watch = new Stopwatch();
            ConsoleKeyInfo c = new ConsoleKeyInfo();
            var a = new Unit(new Coordinate(10,40));
            var b = new Unit(new Coordinate(20,80));
            a.team = Team.friend;
            b.team = Team.enemy;
            b.c = 'o';
            while (1 == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    watch.Start();
                    if (pause == true) Map.Live();
                    else Step?.Invoke();
                    watch.Stop();
                    if (Console.KeyAvailable == true)
                    {
                        c = Console.ReadKey(true);
                        Map.Control(c);
                    }
                    if (watch.ElapsedMilliseconds < 150)
                        Thread.Sleep((int)(150 - watch.ElapsedMilliseconds));
                    watch.Reset();
                }
            //Console.WriteLine();Console.Write(sum/100);
            }
            Console.ReadKey();
        }
    }
}
