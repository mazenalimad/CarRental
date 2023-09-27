using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Console_Print
    {
        public void Close()
        {
            Clear(); //for close page 
        }
        protected char Press_check() //for choise without press enter
        {
            return ReadKey().KeyChar;
        }
    }
}
