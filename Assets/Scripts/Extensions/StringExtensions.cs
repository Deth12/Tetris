using UnityEngine;

namespace Extensions
{
    public static class StringExtensions
    {
        public static string ToFormatedScore(this int value, int paddingCharAmount, char paddingChar)
        {
            if (value < 0)
                value = Mathf.Abs(value);
            
            return value.ToString().PadLeft(paddingCharAmount, paddingChar);
        }
    }
}

