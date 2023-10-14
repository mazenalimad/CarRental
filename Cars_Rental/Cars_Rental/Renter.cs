using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    public interface IPayment_method//this interface declares method to detrmine the way the Client want to pay
    {
        char GetPayment_meth();
    }
   public  class Renter:Personal_Info,IPayment_method
    {
       
        public string payment_method { set; get; }
        public int Driver_licence { get; set; }
        public Date due_date { get; set; }
        public string received_date { get; set; }
        public Renter(string name=" ",int ssn=0,int phone=0,int driver_lice_num=0):base(name,ssn,phone) {

            Driver_licence = driver_lice_num;
        }
        
        public char GetPayment_meth()// implmentation of Ipayment_method interface
        {
            Write($"\nChoose the way to pay the fee (1) By Cash , (2) by Credit Card: ");
            char c = ReadKey(true).KeyChar;
            while (c < '1' || c > '2')
            {
                Write("\nPlease Enter again, Choose 1 or 2 ");
                c = ReadKey(true).KeyChar;
            }
            return c;
        }
        public override string ToString()
        {
            return$" {base.ToString(),10} "+ $" {Driver_licence,7} {payment_method,5} {due_date,6} " ;
        }

    }
}
