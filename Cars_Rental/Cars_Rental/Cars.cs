using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Rental
{
    public class Cars : MNF
    {
        public string Model { set; get; }
        public int Year { set; get; }
        public int Price { set; get; }
        public int License_plate_no { set; get; }
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
