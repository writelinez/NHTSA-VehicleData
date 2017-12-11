using System;
using System.Collections.Generic;
using System.Text;

namespace NHTSACLI
{
    public static class Art
    {
        public static void PrintApplicationTitle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  _  _ _  _ _____ ___   _       _   ___ ___    ___ _    ___ 
 | \| | || |_   _/ __| /_\     /_\ | _ \_ _|  / __| |  |_ _|
 | .` | __ | | | \__ \/ _ \   / _ \|  _/| |  | (__| |__ | | 
 |_|\_|_||_| |_| |___/_/ \_\ /_/ \_\_| |___|  \___|____|___|                                                            
");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            PrintHR();
            Console.ResetColor();
        }

        public static void PrintHR()
        {
            Console.WriteLine(new string('-', Console.BufferWidth));
        }

        public static void PrintTableRow(int tableWidth, params string[] columns)
        {
            Console.WriteLine(new string('-', tableWidth == 0 ? Console.BufferWidth : tableWidth));

            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
            Console.WriteLine(new string('-', tableWidth == 0 ? Console.BufferWidth : tableWidth));
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }





        #region Internal
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        #endregion
    }
}
