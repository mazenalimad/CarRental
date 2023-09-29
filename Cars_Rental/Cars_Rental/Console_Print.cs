using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    public class Console_Print
    {
        protected char key; //key for choise
        protected bool test; //bool for if the user choise invalid can do it again
        protected bool reload = true; //for escape from stackoverflow logic error
        public void Close()
        {
            Clear(); //for close page 
        }
        protected char Press_check() //for choise without press enter
        {
            return ReadKey(true).KeyChar;
        }
    }
}
