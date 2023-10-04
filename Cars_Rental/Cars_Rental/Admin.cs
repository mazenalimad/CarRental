using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Admin : Console_Print
    {
        private void Print(string username, int id)
        {
            WriteLine("# Admin #");
            WriteLine("Username : " + username);
            WriteLine("ID Account : " + id + "\n");
            WriteLine("1- Cars");
            WriteLine("2- Employees");
            WriteLine("3- Back");
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

        private void Move(string username, int id)
        {
            if (key == '1') //here for check of choise and teleport to function of choise
            {
                Cars_List car_list = new Cars_List();
                car_list.Show(username,id);
            }
            else if (key == '2')
            {
                Employees_List emp_list = new Employees_List();
                emp_list.Show(username, id);
            }
            else
            {
                reload = false; //exit from application
            }
        }

        public void Show(string username, int id) //access to home page method
        {
            this.Admin_Page(username,id);
        }


        private void Admin_Page(string username, int id)
        {
            while (reload)//here we use it if reload is false turn of this function
            {
                this.Close(); this.Print(username, id); // here for clear terminal then show home page this if page reload
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
                this.Move(username,id);
            }
        }
    }
}
