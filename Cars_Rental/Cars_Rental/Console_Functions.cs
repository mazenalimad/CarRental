using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Functions
    {
        //home page
        public static void Home_Page() 
        {
            bool reload = true; //for escape from stackoverflow logic error

            while (reload)//here we use it if reload is false turn of this function
            {
                Home home = new Home();
                home.Close(); home.Show(); // here for clear termenal then show home page this if page reload
                bool test; char key = '0'; //bool for if the user choise invalid can do it again & key for choise
                do
                {
                    try
                    {
                        test = false; // here we put false it's mean choise succesful
                        key = home.Press();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        test = true; // Choise is invalid
                        Write("\nEnter Again to Exit : ");
                    }
                } while (test); //if true then is invalid choise then do it again
                if (key == '1') //here for check of choise and teleport to function of choise
                {
                    home.Close();
                    Inquire_Page();
                }
                else if (key == '2')
                {

                }
                else
                {
                    reload = false; 
                    //here is mean turn off this function and this mean turn back 1 step & in home it's mean exit from application
                }
            }
        }

        //Inquire Page =>> then every single line is the same proccess of home page
        static void Inquire_Page()
        {
            bool reload = true;

            while (reload)
            {
                Inquire_About inquire = new Inquire_About();
                inquire.Close(); inquire.Show();
                bool test; char key = '0';
                do
                {
                    try
                    {
                        test = false;
                        key = inquire.Press();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        test = true;
                        Write("\nChoise Again : ");
                    }
                } while (test);
                reload = false; 
            }
        }
    }
}
