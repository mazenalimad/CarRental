using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Rental
{
    public class MNF
    {
        private  int ID { get; }
        protected MNF()
        {
            ID = 5; //will extract from database
        }
        public string Brand { set; get; }
    }
}
