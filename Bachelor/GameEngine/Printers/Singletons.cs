using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Printers
{
    public class Singletons
    {
        static IPrinter printer;
        public static IPrinter GetPrinter()
        {
            if (printer == null)
                UseDefaultPrinter();
            return printer;
        }

        public static void UseDefaultPrinter()
        {
            printer = new DetailPrinter();
        }

        public static void UseSilientPrinter()
        {
            printer = new SilientPrinter();
        }
    }
}
