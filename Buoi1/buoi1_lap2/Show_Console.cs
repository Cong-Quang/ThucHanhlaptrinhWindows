using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buoi1_lap2
{
    public class Show_Console
    {
        public static string Print(string s, int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            try
            {
                if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
                {
                    Console.ForegroundColor = color;
                    Console.SetCursorPosition(x, y);
                    Console.Write(s ?? "");
                    Console.ResetColor();
                }
            }
            catch {}
            return s;
        }

    }
}
