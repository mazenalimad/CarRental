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
        static void Home_Page()
        {
            WriteLine("# Cars Rental #\n");
            Home home = new Home();
            home.Show();
            bool test;
            do
            {
                try
                {
                    test = false;
                    char c = home.Press();
                }
                catch (ArgumentOutOfRangeException)
                {
                    test = true;
                    Write("\nChoise Again : ");
                }
            } while (test);
        }

        static void Main(string[] args)
        {
            Home_Page();
            ReadKey();
        }
    }
}
