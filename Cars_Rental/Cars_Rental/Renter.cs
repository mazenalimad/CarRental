﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Cars_Rental
{
    public interface IPayment_method//this interface declares method to detrmine the way the Client want to pay
    {
        int GetPayment_meth();
    }
   public  class Renter:Client,IPayment_method
    {
       
        public int Cash { set; get; }
        public int Driver_licence { get; set; }
        public Renter(string name=" ",int ssn=0,int phone=0,int driver_lice_num=0):base(name,ssn,phone) {

            Driver_licence = driver_lice_num;

        }
        public int GetPayment_meth()// implmentation of Ipayment_method interface
        {
            WriteLine($"Choose the way to pay the fee please, (1) By Cash , (2) by Credit Card");
            int c = Int32.Parse(ReadLine());
            while ((c < 1 || c > 2))
            {
                WriteLine("please enter again, Choose 1 or 2 ");
                c = Int32.Parse(ReadLine());
            }
            return c;
        }
        
    }
}
