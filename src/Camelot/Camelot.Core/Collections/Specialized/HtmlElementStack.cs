using System;
using System.Collections.Generic;

namespace Camelot.Collections.Specialized
{
    internal sealed class HtmlElementStack
    {
        #region Internal Instance Data

        private readonly Stack<KeyValuePair<string, HtmlElement>> stack = new Stack<KeyValuePair<string, HtmlElement>>();

        #endregion

        #region Properties

        internal int Count
        {
            get
            {
                return stack.Count;
            }
        }

        #endregion

        #region Methods

        internal void Push(HtmlElement element)
        {
            stack.Push(new KeyValuePair<string, HtmlElement>(element.Kind, element));
        }

        internal HtmlElement Pop()
        {
            return stack.Pop().Value;
        }

        internal HtmlElement Peek()
        {
            return stack.Peek().Value;
        }

        internal HtmlElement SafePop(HtmlElement @default)
        {
            return Count > 0
                ? Pop()
                : @default;
        }

        internal HtmlElement SafePeek(HtmlElement @default)
        {
            return Count > 0
                ? Peek()
                : @default;
        }

        #endregion
    }
}