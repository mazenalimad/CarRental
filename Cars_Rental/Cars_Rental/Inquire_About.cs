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
            this.ShowCarData();
            WriteLine("\n- Press any key to Back");
        }
        
        private void ShowCarData()
        {
            //TODO Dalton will extarct table inquire from sql and select ID, Brand, Car Model, Year, Status
            AccessMySql userDate = new AccessMySql();
            List<List<string>> result = new List<List<string>>();


            result = userDate.SqlQuary($"SELECT id, brand, model, year, state from car");
            var c = String.Format("{0,15}{1,15}{2,15}{3,15}{4,15}\n\n","id", "brand", "model", "year", "state");
            WriteLine(c);
            foreach (var items in result)
            {
                foreach (var item in items)
                    Console.Write("{0,15}",item);
                Console.Write("\n");
            }

        }

        public void Show() //to access inquire_page method in class cause it's private
        {
            this.Inquire_Page();
        }
        private void Inquire_Page()
        {
            this.Close(); this.Print(); //clear everything then print information //wait a user enter any key to exit from inquire page
            this.Press();
        }
        protected override char Press()
        {
            ReadKey();
            return ' ';
        }
    }
}
