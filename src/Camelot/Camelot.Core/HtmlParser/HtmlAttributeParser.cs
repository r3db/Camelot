using System;
using System.Collections.Generic;
using System.Text;
using Camelot.Collections.Specialized;

namespace Camelot
{
    internal sealed class HtmlAttributeParser : BaseParser
    {
        #region Internal Static Data

        // Todo: Rename!
        private static readonly ISet<char> keyIllegalTokens = new HashSet<char>
        {
            '\u0000',
            '\u0020',
            '\u0009',
            '\u000A',
            '\u000C',
            '\u000D',
            '"',
            '\'',
            '>',
            '/',
            '='    
        };

        private static readonly ISet<char> closerToken = new HashSet<char>
        {
            '>',
            '/',  
        };

        #endregion

        #region .Ctor

        public HtmlAttributeParser(TokenNavigator navigator)
            : base(navigator)
        {
        }

        #endregion

        #region Methods

        public HtmlAttributeSet Parse()
        {
            var token = Navigator.ReadCurrent();
            var attributes = new HtmlAttributeSet();

            while (closerToken.Contains(token) == false)
            {
                Navigator.MoveToNextNonWhitespaceToken();
                var key = ParseAttributeKey();

                if (string.IsNullOrEmpty(key))
                {
                    break;
                }

                Navigator.MoveToNextNonWhitespaceToken();

                if (closerToken.Contains(Navigator.ReadCurrent()))
                {
                    attributes.Add(new HtmlAttribute(key, key));
                    break;
                }

                if (Navigator.ReadCurrent() != '=')
                {
                    attributes.Add(new HtmlAttribute(key, key));
                    continue;
                }

                Navigator.AssertCurrentTokenIs('=');
                Navigator.MoveNext();
                Navigator.MoveToNextNonWhitespaceToken();

                ParseAttribute(attributes, key);
            }

            return attributes;
        }

        #endregion

        #region Helpers

        private string ParseAttributeKey()
        {
            var token = Navigator.ReadCurrent();
            var sb = new StringBuilder();

            while (IsValidKeyToken(token))
            {
                sb.Append(token);
                token = Navigator.ReadNext();
            }

            return sb.ToString();
        }

        private void ParseAttribute(HtmlAttributeSet attributes, string key)
        {
            var expectedQuote = Navigator.ReadCurrent();

            switch (expectedQuote)
            {
                case '\'':
                case '"':
                {
                    Navigator.MoveNext();

                    // Todo: Escape Characters
                    var value = ParseAttributeValue(expectedQuote);

                    attributes.Add(new HtmlAttribute(key, value));
                    Navigator.AssertCurrentTokenIs(expectedQuote);
                    Navigator.MoveNext();

                    break;
                }
                default:
                {
                    // Todo: Escape Characters
                    var value = ParseAttributeValue(' ');
                    attributes.Add(new HtmlAttribute(key, value));

                    break;
                }
            }
        }

        private string ParseAttributeValue(char quote)
        {
            var token = Navigator.ReadCurrent();

            var sb = new StringBuilder();

            while (token != quote)
            {
                sb.Append(token);
                token = Navigator.ReadNext();
            }

            return sb.ToString();
        }

        private static bool IsValidKeyToken(char c)
        {
            return keyIllegalTokens.Contains(c) == false;
        }

        #endregion
    }
}