using System;
using System.Diagnostics;
using System.Text;
using Axiomatic;

namespace Camelot
{
    [DebuggerDisplay("({Line},{Column})")]
    internal sealed class TokenNavigator
    {
        #region Internal Instance Data

        private readonly string html;
        private int index = -1;
        
        #endregion

        #region .Ctor

        internal TokenNavigator(string html)
        {
            CodeContract.Begin(html, "html").CannotBe.Null();
            this.html = html;
            Line = 1;
            Column = 0;
        }

        #endregion

        #region Auto Implemented Properties

        public int Line { get; private set; }

        public int Column { get; private set; }

        #endregion

        #region Methods

        internal bool CanMoveNext()
        {
            return (index + 1) < html.Length;
        }

        internal char ReadCurrent()
        {
            return html[index];
        }

        internal char ReadNext()
        {
            index++;
            return UpdateLocation(html[index]);
        }

        internal void MoveNext()
        {
            index++;
            UpdateLocation(html[index]);
        }

        internal void MoveToNextNonWhitespaceToken()
        {
            var token = ReadCurrent();

            while (char.IsWhiteSpace(token))
            {
                token = ReadNext();
            }
        }

        internal void AssertCurrentTokenIs(char token)
        {
            if (ReadCurrent() != token)
            {
                // Todo: Signal what is wrong, where and expected token!
                throw new InvalidOperationException();
            }
        }

        #endregion

        #region Helpers

        private char UpdateLocation(char token)
        {
            Column++;

            if (token == '\n')
            {
                Line++;
                Column = 0;
            }

            return token;
        }

        #endregion
    }
}