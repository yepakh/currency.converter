using System.Text;

namespace Currency.Converter.BL
{
    public static class Converter
    {
        public static string Convert(string incomeString)
        {
            var resultBuilder = new StringBuilder();
            incomeString = incomeString.Replace(" ", "");
            string[] nums = incomeString.Split(',');

            resultBuilder.Append(ConvertDollars(nums[0]));
            if (nums.Length > 1)
            {
                resultBuilder.Append(" and ");
                resultBuilder.Append(ConvertCents(nums[1]));
            }

            return resultBuilder.ToString();
        }

        private static string ConvertCents(string centString)
        {
            StringBuilder resultBuilder = new StringBuilder();

            if (centString.Length > 2) throw new ArgumentException("Cents should be represented by 1 or 2 digits");

            uint cents = uint.Parse(centString);

            if (cents == 0) return String.Empty;
            if (centString.Length == 1) cents *= 10;

            resultBuilder.Append(ConvertThreeDigits(cents));
            resultBuilder.Append(" " + Constants.Cent + GetPluralEnding(cents));

            return resultBuilder.ToString().Trim();
        }

        private static string ConvertDollars(string dollarString)
        {
            StringBuilder resultBuilder = new StringBuilder();

            uint dollars = uint.Parse(dollarString);

            if (dollars == 0) return Constants.ZeroDollars;

            int iteration = 0;
            uint countDollars = dollars;
            while (countDollars > 0)
            {
                uint threeDigitNumber = countDollars % 1000;
                if (threeDigitNumber != 0)
                {
                    string threeDigitResult = ConvertThreeDigits(threeDigitNumber);

                    if (iteration >= 1)
                    {
                        resultBuilder.Insert(0, " " + Constants.PowersOfThousandNames[iteration]);
                    }

                    resultBuilder.Insert(0, " " + threeDigitResult);
                }

                countDollars = countDollars / 1000;
                iteration++;
            }

            resultBuilder.Append(" " + Constants.Dollar + GetPluralEnding(dollars));
            return resultBuilder.ToString().Trim();
        }

        private static string GetPluralEnding(uint number) => number != 1 ? "s" : String.Empty;

        private static string ConvertThreeDigits(uint number)
        {
            if (number == 0) return String.Empty;

            if (number >= 1000) throw new ArgumentException("Parameter 'number' should be less than 1000");

            return (GetHundreds(number / 100) + " " + GetUpToHundred(number % 100)).Trim();
        }

        private static string GetHundreds(uint number) =>
            number > 0 && number < 10
            ? Constants.NumberNames[number] + " " + Constants.Hundred
            : String.Empty;

        private static string GetUpToHundred(uint number)
        {
            if (number == 0 || number >= 100) return String.Empty;
            if ((number >= 1 && number <= 20) || number % 10 == 0) return Constants.NumberNames[number];
            else return Constants.NumberNames[number - number % 10] + "-" + Constants.NumberNames[number % 10];
        }

    }
}