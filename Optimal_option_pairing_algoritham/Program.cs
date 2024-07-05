// See https://aka.ms/new-console-template for more information

using GoogleOR;

internal class Program
{
    private static void Main(string[] args)
    {


        Portfolio portfolioTest = new Portfolio();

        portfolioTest.Initialize();

        //adding constraints

        portfolioTest.SetSystemOfConstraintsEnsuringUniquenessOfOptionPair();

        portfolioTest.DetermineCapitalCharge();

        portfolioTest.PortfolioReportCC();
        
        //Console.ReadKey();
    }
}



