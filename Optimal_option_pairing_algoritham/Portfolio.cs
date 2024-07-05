

using Google.OrTools.LinearSolver;


namespace GoogleOR
{
    public class Portfolio
    {


        private readonly Dictionary<(int, int), (Variable variable, Option option1, Option option2)> _optimizationVariables = new();
        private readonly Dictionary<int, (Variable variable, Option option)> _augmentedVariables = new();
        private readonly Dictionary<(int, int), (Variable variable, Equity equity, Option option)> _equityPortfolio = new();
        Equity[] Equities;
        LinkedList<Option> _options = new();
        public Solver _solver;
        int price = 848;
        int quantity = 450;

        public Portfolio()
        {
            _solver = Solver.CreateSolver("SCIP");

            if (_solver is null)
                throw new NullReferenceException("Solver can't be null");
        }


        public void Initialize()
        {
            Equities = GetEquities(quantity);
            _options = GetOptions(price);
            InitialOptionNumber = _options.Count;

            int variableIndex1 = 1;
            int variableIndex2 = variableIndex1 + 1;

            int optionIndex1 = 0;
            int optionIndex2 = 1;

            for (int i = 1; i <= InitialOptionNumber; i++)
            {
                for (int j = 1; j <= Equities.Length; j++)
                {

                    Variable variable = CreateVariable($"x{i}{j}E");
                    _equityPortfolio.TryAdd((i, j), (variable, Equities[j - 1], _options.ElementAt(i - 1)));
                }
            }

            while (variableIndex1 <= InitialOptionNumber)
            {
                if (variableIndex1 < InitialOptionNumber)
                {
                    Variable variable = CreateVariable($"x{variableIndex1}{variableIndex2}");
                    _optimizationVariables.TryAdd((variableIndex1, variableIndex2), (variable, _options.ElementAt(optionIndex1), _options.ElementAt(optionIndex2)));

                    variableIndex2++;
                    optionIndex2++;

                    if (InitialOptionNumber >= variableIndex2)
                        continue;
                }

                Variable augmentedVariable = CreateVariable($"x{variableIndex1}a");
                _augmentedVariables.TryAdd(variableIndex1, (augmentedVariable, _options.First()));

                _options.Remove(_options.First);

                optionIndex2 = 1;
                variableIndex1++;
                variableIndex2 = variableIndex1 + 1;
            }
        }

        public void DetermineCapitalCharge()
        {
            LinearExpr objectiveLinearExpression = GetObjectiveExpression();
            _solver.Minimize(objectiveLinearExpression);
        }

        public Equity[] GetEquities(int quantity)
        {
            int size = quantity / 100;
            Equity[] equityPortfolio = new Equity[size];
            for (int i = 0; i < size; i++)
            {
                equityPortfolio.SetValue(new Equity("NVDIA", price), i);
            }
            return equityPortfolio;
        }

        private LinearExpr GetObjectiveExpression()
        {
            LinearExpr objectiveLinearExpression = new();
            foreach (var optimizationPair in _optimizationVariables)
            {
                objectiveLinearExpression += optimizationPair.Value.variable * (optimizationPair.Value.option1 + optimizationPair.Value.option2);
            }

            foreach (var augmentedPortfolio in _augmentedVariables)
            {
                objectiveLinearExpression += augmentedPortfolio.Value.variable * augmentedPortfolio.Value.option.Premium;
            }

            foreach (var equityPortfolio in _equityPortfolio)
            {
                objectiveLinearExpression += equityPortfolio.Value.variable * (equityPortfolio.Value.equity + equityPortfolio.Value.option);
            }

            return objectiveLinearExpression;
        }

        public void SetSystemOfConstraintsEnsuringUniquenessOfOptionPair()
        {
            for (int i = InitialOptionNumber; i >= 1; i--)
            {
                LinearExpr linearExpression = new();

                for (int e = 1; e <= Equities.Count(); e++)
                {
                    linearExpression += _equityPortfolio[(i, e)].variable;
                }

                for (int j = 1; j <= InitialOptionNumber; j++)
                {
                    if (i == j)
                        continue;
                    if (_optimizationVariables.ContainsKey((i, j)))
                    {
                        linearExpression += _optimizationVariables[(i, j)].variable;
                    }
                    else
                    {
                        linearExpression += _optimizationVariables[(j, i)].variable;
                    }
                }
                linearExpression += _augmentedVariables[i].variable;

                _solver.Add(linearExpression == 1);
            }
            LinearExpr equitypairconstraints = new LinearExpr();
            foreach (var equitypair in _equityPortfolio)
            {
                equitypairconstraints += equitypair.Value.variable;
            }
            _solver.Add(equitypairconstraints <= quantity / 100);
        }

        private LinkedList<Option> GetOptions(int price)
        {
            //will be adapted to the sp call


            int current_price = price;

            LinkedList<Option> options = new();

            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 835, "put", 14, "long"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 840, "put", 16, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 860, "call", 20, "short"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            options.AddLast(new Option("NVDIA", current_price, 845, "call", 28, "long"));
            return options;
        }

        private Variable CreateVariable(string variableName)
        {
            Variable variable = _solver.MakeBoolVar(variableName);
            return variable;
        }

        public void PortfolioReportCC()
        {
            Solver.ResultStatus resultStatus = _solver.Solve();

            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Console.WriteLine("The problem does not have an optimal solution!");
                return;
            }

            Console.WriteLine("Solution:");
            Console.WriteLine("Minimum CC = " + _solver.Objective().Value());

            foreach (var position in _optimizationVariables)
            {
                if (position.Value.variable.SolutionValue() == 1)
                {
                    Console.WriteLine($"{position.Key} = " +
                        $"" + position.Value.variable.SolutionValue() +
                        "  " + position.Value.option1.ToString() +
                        "  " + position.Value.option2.ToString()
                        + " " + "CC: "
                        + position.Value.variable.SolutionValue() * (position.Value.option1 + position.Value.option2));

                }
                else
                    continue;
            }

            foreach (var position in _augmentedVariables)
            {
                if (position.Value.variable.SolutionValue() == 1)
                {
                    Console.WriteLine($"{position.Key} = "
                        + position.Value.variable.SolutionValue() +
                        "  " + position.Value.option.ToString() +
                        "  This option is unpaired and it's CC is: "
                        + position.Value.option.Premium);
                }
                else
                    continue;
            }

            foreach (var position in _equityPortfolio)
            {
                if (position.Value.variable.SolutionValue() == 1)
                {
                    Console.WriteLine($"{position.Key} = "
                        + position.Value.variable.SolutionValue() +
                        "  " + position.Value.option.ToString() +
                        "  " + position.Value.equity.ToString() +
                        "   CC: "
                        + +position.Value.variable.SolutionValue() * (position.Value.equity + position.Value.option));
                }
                else
                    continue;
            }
        }

        private int InitialOptionNumber { get; set; }
    }
}
