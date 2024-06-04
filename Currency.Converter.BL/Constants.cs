namespace Currency.Converter.BL
{
    internal class Constants
    {
        public static readonly Dictionary<uint, string> NumberNames = new Dictionary<uint, string>
        {
            {1, "one" },
            {2, "two" },
            {3, "three" },
            {4, "four" },
            {5, "five" },
            {6, "six" },
            {7, "seven" },
            {8, "eight" },
            {9, "nine" },
            {10, "ten" },
            {11, "eleven" },
            {12, "twelve" },
            {13, "thirteen" },
            {14, "fourteen" },
            {15, "fifteen" },
            {16, "sixteen" },
            {17, "seventeen" },
            {18, "eighteen" },
            {19, "nineteen" },
            {20, "twenty" },
            {30, "thirty" },
            {40, "forty" },
            {50, "fifty" },
            {60, "sixty" },
            {70, "seventy" },
            {80, "eighty" },
            {90, "ninety" },
        };

        public static readonly string[] PowersOfThousandNames = new string[] { string.Empty, "thousand", "million", "billion" };

        public const string Hundred = "hundred";
        public const string Dollar = "dollar";
        public const string Cent = "cent";
        public const string ZeroDollars = "zero dollars";
    }
}
