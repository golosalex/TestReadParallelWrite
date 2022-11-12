using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestReadParallelWrite
{
    internal static class ConsoleExt
    {
        static object lockobj = new object();
        static int[] _corentLine = new int[15];
        public static void WriteLine(string st,ConsoleColor color)
        {
            lock (lockobj)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(30 * ((int)color - 1), _corentLine[(int)color]);
                Console.Write(st);
                Console.ForegroundColor = ConsoleColor.White;
                _corentLine[(int)color]++;
            }
        }
    }
}
