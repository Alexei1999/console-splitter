using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disply
{
    public enum Mode {Default,Choose};
    static public class Map
    {
        static Mode mode = Mode.Default;
        public static int Height = 30;
        public static int Width = 120;
        static char[,] map = new char[Map.Height, Map.Width];
        static ConsoleColor[,] foreColor = new ConsoleColor[Height, Map.Width];
        static ConsoleColor[,] backColor = new ConsoleColor[Map.Height, Map.Width];
        static public void Live()//Как всунуть режимы?
        {
            DrowGrid();
            Display();
            RemoveGrid();
            Display();
        }
        static public void SetDefaultMode()
        {
            mode = Mode.Default;
            Console.WindowHeight = Height;
            Console.WindowWidth = Width;
            map = new char[Map.Height, Map.Width];
            foreColor = new ConsoleColor[Height, Map.Width];
            backColor = new ConsoleColor[Map.Height, Map.Width];
        }
        static public void DrowGrid(int v = 3, int h = 3)//v - vertical, h - horizontal cells
        {
            int ls, ks;
            int m = 0, n = 0;
            int l = (Width + v + 1) / v;
            while ((ls = Width + v + 1 - l * v) > v) l++;
            int k = (Height+ h + 1) / h;
            while ((ks = Height + h + 1 - k * h) > h) k++;
            Console.WindowWidth = Width + v + 1;
            Console.WindowHeight = Height + h + 1;
            map = new char[Height + h + 1, Width + v + 1];
            foreColor = new ConsoleColor[Height + h + 1, Width + v + 1];
            backColor = new ConsoleColor[Height + h + 1, Width + v + 1];
            for (int i = 0; i < map.GetLength(0); i++)//38
            {
                if (i == (h + 1 - ks) * k)
                {
                    m = (h - ks) * k;
                    ks = h;
                    k++;
                }
                int rs = ls;
                int r = l;
                if (i == Height + h || (i-m) % k == 0)
                {
                    char a = ' ', b = ' ', c = ' ', d = ' ';
                    if (i == 0)
                    { a = '┌'; b = '┐'; c = '┬'; d = '─'; }
                    else if (i == Height + h)
                    { a = '└'; b = '┘'; c = '┴'; d = '─'; }
                    else if ((i-m) % k == 0)
                    { a = '├'; b = '┤'; c = '┼'; d = '─'; }
                    for (int s = 0; s < map.Length / map.GetLength(0); s++)
                    {
                        if (s == (v + 1 - rs) * r)
                        {
                            n = (v - rs) * r;
                            rs = v;
                            r++;
                        }
                        foreColor[i,s] = ConsoleColor.Red;
                        if (s == 0) map[i,s] = a;
                        else if (s == Width + v) map[i, s] = b;
                        else if ((s - n) % r == 0) map[i, s] = c;
                        else map[i, s] = d;
                    }
                    n = 0;
                    continue;
                }
                rs = ls;
                r = l;
                for (int j = 0; j < map.Length / map.GetLength(0); j++)
                {
                    if (j == (v + 1 - rs)*r)
                    {
                        n = (v - rs) * r;
                        rs = v;
                        r++;
                    }
                    if ((j - n) % r == 0 || j == Width + v) { 
                        foreColor[i,j] = ConsoleColor.Red;
                        map[i,j] = '│'; 
                        continue; 
                    }
                }
                n = 0;
            }
        }
        static public void RemoveGrid(int v = 3, int h = 3)
        {
            int ls, ks;
            int m = 0, n = 0;
            int l = (Width + v + 1) / v;
            while ((ls = Width + v + 1 - l * v) > v) l++;
            int k = (Height + h + 1) / h;
            while ((ks = Height + h + 1 - k * h) > h) k++;
            for (int i = 0; i < map.GetLength(0); i++)//38
            {
                if (i == (h + 1 - ks) * k)
                {
                    m = (h - ks) * k;
                    ks = h;
                    k++;
                }
                int rs = ls;
                int r = l;
                if (i == Height + h || (i - m) % k == 0)
                {
                    for (int s = 0; s < map.Length / map.GetLength(0); s++)
                    {
                        if (s == (v + 1 - rs) * r)
                        {
                            n = (v - rs) * r;
                            rs = v;
                            r++;
                        }
                        foreColor[i, s] = ConsoleColor.Gray;
                        map[i, s] = ' ';
                    }
                    n = 0;
                    continue;
                }
                rs = ls;
                r = l;
                for (int j = 0; j < map.Length / map.GetLength(0); j++)
                {
                    if (j == (v + 1 - rs) * r)
                    {
                        n = (v - rs) * r;
                        rs = v;
                        r++;
                    }
                    foreColor[i, j] = ConsoleColor.Gray;
                    if ((j - n) % r == 0 || j == Width + v)
                    {
                        map[i, j] = ' ';
                        continue;
                    }
                }
                n = 0;
            }
            Console.WindowWidth = Width;
            Console.WindowHeight = Height;
            Display();
            map = new char[Height, Width];
            foreColor = new ConsoleColor[Height, Width];
            backColor = new ConsoleColor[Height, Width];
            Display();
        }
        static public void Display()
        {
            for (int i = 0; i < map.GetLength(0); i++)//30
            {
                for (int j = 0; j < map.Length / map.GetLength(0); j++)//120
                {
                    if (foreColor[i, j] != ConsoleColor.Gray) Console.ForegroundColor = foreColor[i, j];
                    if (backColor[i, j] != ConsoleColor.Black) Console.BackgroundColor = backColor[i, j];
                    Console.Write(map[i, j]);
                    if (foreColor[i, j] != ConsoleColor.Gray || backColor[i, j] != ConsoleColor.Black) Console.ResetColor();
                }
            }
            Console.SetCursorPosition(0, 0);
        }
        static public void Write(Unit a)
        {
            map[a.Pos.y, a.Pos.x] = a.c;
            if (a.team == Team.enemy) foreColor[a.Pos.y, a.Pos.x] = ConsoleColor.Red;
            if (a.team == Team.friend) foreColor[a.Pos.y, a.Pos.x] = ConsoleColor.Blue;
        }
        static public void Write(Coordinate a, char c, ConsoleColor forColor = ConsoleColor.Gray, ConsoleColor bakColor = ConsoleColor.Black) {
            if (a.y < 0 || a.y >= Map.Height || a.x < 0 || a.x >= Map.Width) return;
            map[a.y, a.x] = c;
            foreColor[a.y, a.x] = forColor;
            backColor[a.y, a.x] = bakColor;
        }
        static public void Clear()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.Length / map.GetLength(0); j++)
                {
                    map[i, j] = ' ';
                    foreColor[i, j] = ConsoleColor.Gray;
                    backColor[i, j] = ConsoleColor.Black;
                }
            Console.SetCursorPosition(0, 0);
        }
        static public void Control(ConsoleKeyInfo a)
        {
            if (a.Key == ConsoleKey.Spacebar) DrowGrid();
        }
        static Map()
        {
            Clear();
            Console.CursorVisible = false;
            Program.Step += Live;
        }
    }
}