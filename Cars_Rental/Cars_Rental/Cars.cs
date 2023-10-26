using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Rental
{
    public class Cars : MNF
    {
        public int id { set; get; }
        public string Model { set; get; }
        private int year;
        public int Year {
            set {
                if (value < 2005 || value > 2023)
                {
                    throw new ArgumentOutOfRangeException();
                }
                year = value;
            }
            get { return year; } }
        public int Price { set; get; }
        public string License_plate_no { set; get; }
        public Lessor lessor { set; get; }
        public Renter renter { set; get; }
        public string Status { set; get; } = "Available";

        public Cars()
        {
            renter = new Renter();
            lessor = new Lessor();
        }
       
        
    }
}
