using System;
using Camelot.Collections.Specialized;

namespace Camelot
{
    internal sealed class HtmlElementParser : BaseParser
    {
        #region Internal Instance Data

        private readonly HtmlTagNameParser tagNameParser;
        private readonly HtmlAttributeParser attributeParser;

        #endregion

        #region .Ctor

        public HtmlElementParser(TokenNavigator navigator)
            : base(navigator)
        {
            tagNameParser = new HtmlTagNameParser(navigator);
            attributeParser = new HtmlAttributeParser(navigator);
        }

        #endregion

        #region Methods

        public HtmlElement Parse(HtmlElement parent)
        {
            var kind = tagNameParser.Parse();

            Navigator.MoveToNextNonWhitespaceToken();

            var attributes = Navigator.ReadCurrent() == '>'
                ? new HtmlAttributeSet()
                : attributeParser.Parse();

            return new HtmlElement(kind, attributes)
            {
                Parent = parent
            };
        }

        #endregion
    }
}                                               