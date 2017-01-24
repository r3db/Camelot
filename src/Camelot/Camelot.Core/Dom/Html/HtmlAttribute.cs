using System;
using System.Diagnostics;

namespace Camelot
{
    [DebuggerDisplay("{Key} = {Value}")]
    public sealed class HtmlAttribute
    {
        #region .Ctor

        public HtmlAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }

        #endregion

        #region Auto Implemented Properties

        public string Key { get; private set; }

        public string Value { get; private set; }

        #endregion
    }
}