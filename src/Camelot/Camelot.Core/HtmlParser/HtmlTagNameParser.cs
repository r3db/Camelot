using System;
using System.Text;

namespace Camelot
{
    internal sealed class HtmlTagNameParser : BaseParser
    {
        #region .Ctor

        public HtmlTagNameParser(TokenNavigator navigator)
            : base(navigator)
        {
        }

        #endregion

        #region Static Methods

        public static bool IsValidToken(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
        }

        #endregion

        #region Methods

        internal string Parse()
        {
            var sb = new StringBuilder();
            var token = Navigator.ReadCurrent();

            while (IsValidToken(token))
            {
                sb.Append(token);
                token = Navigator.ReadNext();
            }

            return sb.ToString();
        }

        #endregion
    }
}