using System;

namespace RRRStudyProject
{
    public static class TextInterpreter
    {
        public static string Interpret(string textToInterpret)
        {
            if (ulong.TryParse(textToInterpret, out ulong valueToInterpret))
            {
                return ToShiftPrefix(valueToInterpret);
            }
            throw new ArgumentOutOfRangeException("Wrong text input");
        }

        private static string ToShiftPrefix(ulong valueToInterpret)
        {
            if (valueToInterpret < 1000) return valueToInterpret.ToString();
            if (valueToInterpret < 1000000) return $"{valueToInterpret / 1000f:#.000}K";
            if (valueToInterpret < 1000000000) return $"{valueToInterpret / 1000000f:#.000}M";
            if (valueToInterpret < 1000000000000) return $"{valueToInterpret / 1000000000f:#.000}B";
            if (valueToInterpret < 1000000000000000) return $"{valueToInterpret / 1000000000000f:#.000}Q";
            throw new ArgumentOutOfRangeException("Value is out of range");
        }
    }
}