using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("# Cars Rental #\n");
            Home home = new Home();
            home.Show();
            home.Close();
            ReadKey();
        }
    }
}
