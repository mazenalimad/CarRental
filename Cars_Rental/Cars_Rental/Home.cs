﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Home : Console_Print
    {
        private void Print()
        {
            WriteLine("# Cars Rental #\n");
            //the label what we want to print in console
            WriteLine("1- Inquire about");
            WriteLine("2- Login");
            WriteLine("3- Exit");
        }
        public void Show()
        {
            Print(); //for print in console for home page
        }
        public char Press() //for choise without press enter
        {
            char key = Press_check();
            if(key < '1' || key > '3')
            {
                throw new ArgumentOutOfRangeException();//exception when enter wrong will get message fo it
            }
            return key;
        }
    }
}
