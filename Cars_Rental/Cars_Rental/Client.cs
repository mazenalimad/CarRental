/*This class will be a general class to inheret the general properties and fields */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    public class Client
    {
        public string Name { set; get; }
        public int SSN { set; get; }
        public int Phone_num { set; get; }
        public Client(string name=" ",int ssn=0,int phone=0)
        {
            Name = name;
            SSN = ssn;
            Phone_num = phone;
        }
        

    }
}
