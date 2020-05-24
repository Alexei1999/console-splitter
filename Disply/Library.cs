using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disply
{
    static class Library
    {
        public static List<Unit> units = new List<Unit>();
        static void Live()
        {
            
        }
        static Library()
        {
            Program.Step += Live;
        }
    }
}
