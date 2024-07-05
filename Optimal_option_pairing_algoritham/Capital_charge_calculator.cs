using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleOR
{
    public static class CCcalculator
    {
        public static int LongLongPutCall(Option option1, Option option2)
        {
            if (option1.current_price > option1.Strike && option1.current_price < option2.Strike)
            {
                return option1.Premium + option2.Premium;
            }
            else if (option1.current_price < option1.Strike)
            {
                return Math.Abs(option1.Premium + option1.Strike - option1.current_price);
            }
            else if (option1.current_price > option2.Strike)
            {
                return Math.Abs(option1.Premium + option2.Strike - option1.current_price);
            }
            else
            {
                //throw new ArgumentException("Invalid strikes");
                return 1000;//exeption should be thrown
            }
        }

        public static int LongLongPutPut(Option option1, Option option2)
        {
            if (option1.current_price > option1.Strike && option1.current_price > option2.Strike)
            {
                return option1.Premium + option2.Premium;
            }
            else if (option1.current_price < option1.Strike && option1.current_price < option2.Strike)
            {
                return Math.Abs(option1.Premium + option2.Premium - option1.current_price);
            }
            else if (option1.current_price > option1.Strike && option1.current_price < option2.Strike)
            {
                return Math.Abs(option1.Premium + option2.Strike - option1.current_price);
            }
            else if (option1.current_price < option1.Strike && option1.current_price > option2.Strike)
            {
                return Math.Abs(option1.Premium + option1.Strike - option1.current_price);
            }
            else
            {
                //throw new ArgumentException("Invalid strikes");
                return 1000;//exeption should be thrown
            }
        }
        public static int LongLongCallCall(Option option1, Option option2)
        {
            if (option1.current_price < option1.Strike && option1.current_price < option2.Strike)
            {
                return option1.Premium + option2.Premium;
            }
            else if (option1.current_price > option1.Strike && option1.current_price > option2.Strike)
            {
                return Math.Abs(option1.Premium + option2.Premium - option1.current_price);
            }
            else if (option1.current_price > option1.Strike && option1.current_price < option2.Strike)
            {
                return Math.Abs(option1.Premium + option1.Strike - option1.current_price);
            }
            else if (option1.current_price < option1.Strike && option1.current_price > option2.Strike)
            {
                return Math.Abs(option1.Premium + option2.Strike - option1.current_price);
            }
            else
            {
                //throw new ArgumentException("Invalid strikes");
                return 1000;//exeption should be thrown

            }
        }
        public static int ShortLongLongsfortPutCall(Option option1, Option option2)
        {
            if (option1.Type == "call" && option2.Type == "put")
            {
                return Math.Abs(option1.Strike - option2.Strike);
            }
            else if (option1.Type == "put" && option2.Type == "call")
            {
                return Math.Abs(option1.Strike - option2.Strike);
            }

            else if ((option1.Type == "call" && option2.Type == "call") || (option1.Type == "putt" && option2.Type == "put"))
            {
                return Math.Abs(option1.Strike - option2.Strike);
            }
            else if ((option1.Type == "put" && option2.Type == "put") || (option1.Type == "putt" && option2.Type == "put"))
            {
                return Math.Abs(option1.Strike - option2.Strike);
            }

            else
            {
                throw new ArgumentOutOfRangeException("option type must be put or call");
                //return 1000;//exeption should be thrown
            }
        }
        public static int ShortShortCallPut(Option option1, Option option2)
        {
            if (option1.Type == "call" && option2.Type == "put")
            {
                return Math.Abs(option1.Strike - option2.Strike);
            }
            else if (option1.Type == "put" && option2.Type == "call")
            {
                return Math.Abs(option1.Strike - option2.Strike);
            }
            else
            {
                //throw new ArgumentOutOfRangeException("option type must be put or call");
                return 1000;//exeption should be thrown
            }
        }
    }
}
