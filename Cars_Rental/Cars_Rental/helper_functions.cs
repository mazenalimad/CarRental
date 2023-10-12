using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Rental
{
    class Credit
    {
        public bool CreditNumber(long credit_card) => check_validity(credit_card);

        //Function error card
        private bool check_validity(long ccn)
        {
            int len = length(ccn);
            if (!(checklength(len) && checksum(ccn, len)))
            {
                //Console.WriteLine("INVALID");
                return false;
            }

            return print_card_brand(ccn);
        }


        private int length(long n)
        {
            int len = 0;
            while (n > 0)
            {
                len++;
                n /= 10;
            }
            return len;
        }

        //true
        private bool checklength(int len)
        {
            if (len == 13 || len == 15 || len == 16)
            {
                return true;
            }
            return false;
        }

        //Mathematical operation, Lohan algorithm
        private bool checksum(long ccn, int len)
        {
            long sum = 0;
            for (int i = 0; i < len; i++, ccn /= 10)
            {
                if (i % 2 == 0)
                {
                    sum += (ccn % 10);
                }
                else
                {
                    long digit = (2 * (ccn % 10));
                    sum += (digit / 10 + digit % 10);
                }
            }

            if (sum % 10 == 0)
            {
                return true;
            }
            return false;
        }

        //function Select  of credit card type
        private bool print_card_brand(long ccn)
        {
            if ((ccn >= 340000000000000 && ccn < 350000000000000) || (ccn >= 370000000000000 && ccn < 380000000000000))
            {
                //print AMEX
                //Console.WriteLine("AMEX");
                return true;
            }
            else if ((ccn >= 5100000000000000 && ccn < 5600000000000000))
            {
                //print MASTERCARD
                Console.WriteLine("MASTERCARD");
            }
            else if ((ccn >= 40000000000000 && ccn < 50000000000000) || (ccn >= 4000000000000000 && ccn < 5000000000000000))
            {
                //print VISA
                //Console.WriteLine("VISA");
                return true;
            }
            //erorr print INVALID
            //Console.WriteLine("INVALID");
            return false;

        }

    }
}
