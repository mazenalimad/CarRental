using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    public class Program
    {
        static void Main(string[] args)
        {
            Home home = new Home(); //create object for access to home page
            home.Show(); // go to home page
        }
    }
}
