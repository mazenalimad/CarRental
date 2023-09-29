﻿/*Lessor is the person who will lease his car in exchange of money , so this class will set all needed info*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Cars_Rental
{
    public class Lessor:Client
    {
        private float commission_price;
        private bool valid_regstration_papers;
        public bool Valid_Regstration_Papers
        {

            get {

                return valid_regstration_papers;

                }
            set
            {
                if(value==false)//the condition validates regsteration_papers
                { throw new ArgumentOutOfRangeException(); }
                valid_regstration_papers = value;
            }
        }
        public float Commission_price {
            get { return commission_price; }

            set {
                if (value <0 ||value > 1)
                {
                    throw new ArgumentOutOfRangeException();//validating commission_price and throwing an exception if it is invalid 
                }
                commission_price = value;   
                        
                }
        }
    }
}