using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    public class Home : Console_Print
    {
        private void Print()
        {
            WriteLine("# Cars Rental #\n");
            //the label what we want to print in console
            WriteLine("1- Inquire about");
            WriteLine("2- Login");
            WriteLine("3- Exit");
        }
        private char Press() //for choise without press enter
        {
            char key = Press_check();
            if (key < '1' || key > '3')
            {
                throw new ArgumentOutOfRangeException();//exception when enter wrong will get message fo it
            }
            return key; //return it
        }

        private void Move()
        {
            if (key == '1') //here for check of choise and teleport to function of choise
            {
                this.Close(); //close home page
                Inquire_About inquire = new Inquire_About(); //create object to access to inquire page
                inquire.Show(); //to start show inquire page
            }
            else if (key == '2')
            {
                this.Close();
                Login login = new Login();//create object to access to login page
                login.Show(); //to start show login page
            }
            else
            {
                Environment.Exit(0);
                reload = false; //exit from application
               
            }
        }

        public void Show() //access to home page method
        {
            this.Home_Page();
        }


        private void Home_Page()
        {
            while (reload)//here we use it if reload is false turn of this function
            {
                this.Close(); this.Print(); // here for clear terminal then show home page this if page reload
                do
                {
                    try
                    {
                        test = false; // here we put false it means chosen succesfully
                        key = this.Press();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        test = true; // Choise is invalid
                        Write("\rChoose Again : ");
                    }
                } while (test);
                this.Move();
            }
        }
    }
}
