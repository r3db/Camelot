using System;
using System.Text;

namespace Camelot.Extensions
{
    internal static class StringBuilderExtensions
    {
        internal static bool EndsWith(this StringBuilder builder, string s)
        {
            if (builder.Length < s.Length)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (builder[builder.Length - i - 1] != s[s.Length - i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        internal static bool EndsWith(this StringBuilder builder, char c)
        {
            if (builder.Length < 1)
            {
                return false;
            }

            if (builder[builder.Length - 1] != c)
            {
                return false;
            }

            return true;
        }
    }
}