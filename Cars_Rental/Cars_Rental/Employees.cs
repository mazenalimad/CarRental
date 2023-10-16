using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Rental
{
   public class Employees : Personal_Info
    {
        public int id { set; get; }
        public Date BirthDay { set; get; }
        public Date HireDate { set; get; }
        public int Salary { set; get; }
        public override string ToString()
        {
            return $" {base.ToString()} {BirthDay.ToString(),16} {Salary,17:c} " ;
        }
    }
}
