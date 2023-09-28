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
            //the label what we want to print in console
            WriteLine("- Press \"Enter\" to Back");
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
                do
                {
                    try
                    {
                        test = false;
                        key = this.Press();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        test = true;
                        Write("\nEnter Again to Exit : ");
                    }
                } while (test);
                reload = false;
            }
        }
        public char Press() //for choise without press enter
        {
            char key = Press_check();
            if (key != 13)
            {
                throw new ArgumentOutOfRangeException();//exception when enter wrong will get message fo it
            }
            return key;
        }
    }
}
