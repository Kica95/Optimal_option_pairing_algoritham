using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GoogleOR
{
    public class Equity
    {
        public string Name
        {
            init;
            get;
        }
        public double CurrentPrice
        {
            init;
            get;
        }
        public Equity(string name, int price)
        {
            Name = name;
            CurrentPrice = price;
        }
        public static int operator +(Equity equity, Option option)
        {
            if (option.Type == "call" && option.PositionType == "short")
            {
                if (option.Strike < option.current_price)
                {
                    return -option.Premium + (option.current_price - option.Strike) * 100;
                }
                else
                {
                    return -option.Premium;
                }

            }
            if (option.Type == "put" && option.PositionType == "short")
            {
                if (option.Strike > option.current_price)
                {
                    return -option.Premium + (option.Strike - option.current_price) * 100;
                }
                else
                {
                    return -option.Premium;
                }
            }
            return option.Premium;
        }
        public override string ToString() => $"Equity: {this.Name}, {this.CurrentPrice} batch of 100";

    }
}
