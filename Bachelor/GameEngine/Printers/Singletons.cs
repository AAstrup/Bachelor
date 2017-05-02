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
                UseDetailPrinter();
            return printer;
        }

        public static void UseDetailPrinter()
        {
            printer = new DetailPrinter();
        }

        public static void UseResultsPrinter()
        {
            printer = new ResultsPrinter();
        }

        public static void UseNoPrinter()
        {
            printer = new NoPrinter();
        }
    }
}
