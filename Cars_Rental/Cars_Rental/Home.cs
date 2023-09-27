using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Home
    {
        private string str = "/c CLS";
        private void Inquire_button()
        {
            WriteLine("1- Inquire about");
        }
        private void Login_button()
        {
            WriteLine("2- Login");
        }
        public void Show()
        {
            Inquire_button();
            Login_button();
        }

    }
}
