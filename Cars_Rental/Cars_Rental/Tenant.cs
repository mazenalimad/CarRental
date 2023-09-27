using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    class Tenant
    {
        public int ID { get; }
        public string Name { set; get; }
       public Cars Cartype=new Cars();
        public int driver_licence { get; set; }


    }
}
