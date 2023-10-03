using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Cars_Rental
{
    public class Employee : Console_Print
    {
        public void Show() { this.Print(); }
        private void Print()
        {
            WriteLine("#EMPLOYEE#\n");

            WriteLine("what do want to do?\n\n 1- Edit a Customer info 2- Delete a Customer info 3-Display Cars Status   \n");


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
                Edit_info e = new Edit_info();
                e.Show();
            }
            else if (key == '2')
            {
                this.Close();

            }
            else if (key == '3')
            {
                display_status();
            }
        }
        private void display_status()
        {

        }
        private void Employee_Page()
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