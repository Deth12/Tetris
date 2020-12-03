namespace Tetris.Extensions
{
    public static class StringExtensions
    {
        public static string ToFormatedScore(this int value, int paddingCharAmount, char paddingChar)
        {
            return value.ToString().PadLeft(paddingCharAmount, paddingChar);
        }
    }
}

