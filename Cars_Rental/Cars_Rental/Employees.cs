using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Rental
{
    class Employees : Personal_Info
    {
        public Date BirthDay { set; get; }
        public Date HireDate { set; get; }
        public int Salary { set; get; }

    }
}
