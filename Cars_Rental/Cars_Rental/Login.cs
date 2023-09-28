using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Login : Console_Print
    {
        private void Print()
        {
            WriteLine("# Login #");
            //the label what we want to print in console
            Write("- Press \"Enter\" to start Loign or any key to Back : ");
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
                key = this.Press();
                if (key == 13)
                {
                    Write("\n\nUsername : ");
                    string username = ReadLine();
                    Write("Password : ");
                    string password = "";
                    do
                    {
                        key = ReadKey(true).KeyChar;

                        // Backspace Should Not Work
                        if (key != 8)
                        {
                            if(key == 13)
                            {
                                break;
                            }
                            password += key;
                            Write("*");
                        }
                        else
                        {
                            Write("\b");
                        }
                    }
                    // Stops Receving Keys Once Enter is Pressed
                    while (key != 13);

                    if (username == "Admin" && password == "admin")
                    {
                        // if(type == "admin")
                        WriteLine("\nLogin Successful");
                        ReadKey();
                    }
                    else
                    {
                        WriteLine("\nLogin Failed...\nPress Any key to retry");
                        ReadKey();
                    }
                }
                else
                {
                    reload = false;
                }
            }
        }
        public char Press() //for choise without press enter
        {
            key = Press_check();
            return key;
        }
    }
}
