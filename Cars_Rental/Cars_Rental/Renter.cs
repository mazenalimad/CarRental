using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
   public  class Renter:Client
    {
        public string Payment_method { set; get; }
        public int Cash { set; get; }
        public int Driver_licence { get; set; }

    }
}
