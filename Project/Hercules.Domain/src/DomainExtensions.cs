public static class DomainExtensions
{
    public static bool IsBetween(this int val, int bot, int top, bool isStrict = false)
    {
        if (bot > top)
            throw new ArgumentException("Bottom value cannot be bigger than top value");

        bool upper = isStrict ? val < top : val <= top;
        bool lower = isStrict ? val > bot : val >= bot;

        if (lower && upper)
            return true;

        return false;
    }
}