using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
   public class Inquire_About : Console_Print
    {
        private void Print() //print in console
        {
            WriteLine("# Inquire #");
            this.SQL();
            WriteLine("\nmnbnm,- Press any key to Back");
        }
        
        private void SQL()
        {
            //TODO Dalton will extarct table inquire from sql and select ID, Brand, Car Model, Year, Status

        }

        public void Show() //to access inquire_page method in class cause it's private
        {
            this.Inquire_Page();
        }
        private void Inquire_Page()
        {
                this.Close(); this.Print(); //clear everything then print information
                ReadKey(); //wait a user enter any key to exit from inquire page
        }
    }
}
