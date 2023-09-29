/*This class will be a general class to inheret the general properties and fields */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    public class Client:Cars
    {
        public int Id { get; }
        public string Name { set; get; }
        public int SSN { set; get; }
        public int Phone_num { set; get; }
        public int Driver_licence { get; set; }
        public Client(string name=" ",int ssn=0,int phone=0, int driver_lice=0 )
        {
            //id= //this will be gotten from sql 
            Name = name;
            SSN = ssn;
            Phone_num = phone;
            Driver_licence = driver_lice;
        }
        

    }
}
