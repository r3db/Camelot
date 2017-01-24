using System;
using Axiomatic;

namespace Camelot
{
    internal abstract class BaseParser
    {
        #region Internal Instance Data

        private readonly TokenNavigator navigator;

        #endregion

        #region .Ctor

        protected BaseParser(TokenNavigator navigator)
        {
            CodeContract.Begin(navigator, "navigator").CannotBe.Null();
            this.navigator = navigator;
        }

        #endregion

        #region Properties

        protected TokenNavigator Navigator
        {
            get { return navigator; }
        }

        #endregion
    }
}