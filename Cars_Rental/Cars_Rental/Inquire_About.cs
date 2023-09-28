using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Inquire_About : Console_Print
    {
        private void Print()
        {
            WriteLine("# Inquire #");
            //the label what we want to print in console
            WriteLine("- Press any key to Back");
        }


        public void Show()
        {
            this.Inquire_Page();
        }
        private void Inquire_Page()
        {
            while (reload)
            {
                this.Close(); this.Print();
                ReadKey();
                reload = false;
            }
        }
    }
}
