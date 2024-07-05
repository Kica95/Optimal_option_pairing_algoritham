using GoogleOR;

public class Option
{
    public string Name
    {
        init;
        get;
    }

    public string PositionType
    {
        init; get;
    }
    public int current_price  //Try to set a global variable at compile time that  would be equal to the current price
    {
        init; get;
    }

    public int Strike
    {
        init; get;
    }
    public string Type
    {
        init; get;
    }
    public int Premium
    {
        init; get;
    }

    public Option(string name, int Current_price, int strike, string type, int premium, string positionType)
    {
        Name = name;
        current_price = Current_price;
        Strike = strike;
        Type = type;
        Premium = premium;
        PositionType = positionType;
    }
    public static int operator +(Option option1, Option option2)
    {
        if (option1.PositionType == "long" && option2.PositionType == "long")
        {
            if (option1.Type == "put" && option2.Type == "call")
            {
                return CCcalculator.LongLongPutCall(option1, option2);
                //return;

            }
            else if (option1.Type == "call" && option2.Type == "put")
            {
                return CCcalculator.LongLongPutCall(option2, option1);
                //return;
            }
            else if (option1.Type == "put" && option2.Type == "put")
            {
                return CCcalculator.LongLongPutPut(option1, option2);
                //return;
            }
            else if (option1.Type == "call" && option2.Type == "call")
            {
                return CCcalculator.LongLongCallCall(option1, option2);
                //return;

            }
            else
            {
                throw new ArgumentOutOfRangeException("option type must be put or call");
                //return 1000;//exeption should be thrown
            }
        }

        if ((option1.PositionType == "short" && option2.PositionType == "long") || (option1.PositionType == "long" && option2.PositionType == "short"))
        {
            return CCcalculator.ShortLongLongsfortPutCall(option1, option2);

        }
        if (option1.PositionType == "short" && option2.PositionType == "short")
        {
            return CCcalculator.ShortShortCallPut(option1, option2);

        }


        else
        {
            throw new ArgumentOutOfRangeException("option type must be put or call");
            //return 1000;//exeption should be thrown
        }

    }
    public override string ToString() => $"{this.Type}, {this.Strike}, {this.PositionType}, {this.Premium}";
}