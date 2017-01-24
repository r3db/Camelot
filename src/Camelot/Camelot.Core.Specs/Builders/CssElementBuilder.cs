using System;
using System.Collections.Generic;
using Axiomatic;
using Epiphany;

namespace Camelot.Builders
{
    internal sealed class CssSelectorBuilder : FluentBuilder<CssSelector>
    {
        #region .Ctor

        private CssSelectorBuilder(CssSelector selector)
            : base(selector)
        {
        }

        #endregion

        #region Factory .Ctor

        public static CssSelectorBuilder Begin
        {
            get
            {
                return new CssSelectorBuilder(new CssSelector(new List<CssSelectorComponent>(), new List<CssCombinator>()));
            }
        }

        #endregion

        #region Methods

        public CssSelectorBuilder Component(CssSelectorComponent selector, CssCombinator combinator)
        {
            CodeContract.Begin(selector, "selector").CannotBe.Null();
            Instance.Add(selector, combinator);
            return this;
        }

        public CssSelectorBuilder Component(CssSelectorComponent selector)
        {
            CodeContract.Begin(selector, "selector").CannotBe.Null();
            Instance.Add(selector);
            return this;
        }

        #endregion
    }
}