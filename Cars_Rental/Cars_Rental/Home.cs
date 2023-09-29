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
        public char Press() //for choise without press enter
        {
            char key = Press_check();
            if (key < '1' || key > '3')
            {
                throw new ArgumentOutOfRangeException();//exception when enter wrong will get message fo it
            }
            return key;
        }

        public void Move()
        {
            if (key == '1') //here for check of choise and teleport to function of choise
            {
                this.Close();
                Inquire_About inquire = new Inquire_About();
                inquire.Show();
            }
            else if (key == '2')
            {
                this.Close();
                Login login = new Login();
                login.Show();
            }
            else
            {
                reload = false;
            }
        }

        public void Show()
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
                        Write("\nChoose Again : ");
                    }
                } while (test);
                this.Move();
            }
        }
    }
}
