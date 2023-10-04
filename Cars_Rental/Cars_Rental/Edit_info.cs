using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace Cars_Rental
{
    class Edit_info:Employee_Form
    {
        private void print()
        {
            
            WriteLine("Enter ID number of Customer\n");
            int id = int.Parse(ReadLine());
        //    GetCutomer(id);
        }
        
        public void show()
        {
            this.print();
        }

        
        private  void GetCustomer(int id)
        {

            /// here we send the ID to the database and print the result then confirming the Edition 



            WriteLine("Are You Sure of Editing this Record? Y/N");
       
        var c = ReadKey(true).Key;
            while(c!=ConsoleKey.Y || c!=ConsoleKey.N)
            {
                WriteLine("Try again,press Y or N ");
                c = ReadKey(true).Key;
            }
            if(c== ConsoleKey.Y)
            {

            }
            else
            {
            }
        }
        


    }
}
