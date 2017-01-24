using System;
using Axiomatic;
using Epiphany;

namespace Camelot.Builders
{
    internal sealed class CssSelectorComponentBuilder : FluentBuilder<CssSelectorComponent>
    {
        #region .Ctor

        private CssSelectorComponentBuilder(CssSelectorComponent selector)
            : base(selector)
        {
        }

        #endregion

        #region Factory .Ctor

        public static CssSelectorComponentBuilder Begin
        {
            get
            {
                return new CssSelectorComponentBuilder(new CssSelectorComponent());
            }
        }

        #endregion

        #region Methods

        public CssSelectorComponentBuilder Kind(string kind)
        {
            CodeContract.Begin(kind, "kind").CannotBe.NullOrEmpty();
            EnforceProperty(() => Instance.Kind, kind);
            return this;
        }

        public CssSelectorComponentBuilder Identifier(string identifier)
        {
            CodeContract.Begin(identifier, "identifier").CannotBe.NullOrEmpty();
            EnforceProperty(() => Instance.Identifier, identifier);
            return this;
        }

        public CssSelectorComponentBuilder AppendClass(string name)
        {
            CodeContract.Begin(name, "name").CannotBe.NullOrEmpty();
            Instance.Classes.Add(name);
            return this;
        }

        public CssSelectorComponentBuilder Attribute(string key, CssAttributeOperator @operator, string value)
        {
            CodeContract.Begin(key, "key").CannotBe.NullOrEmpty();
            CodeContract.Begin(value, "value").CannotBe.NullOrEmpty();
            
            Instance.AttributeKey = key;
            Instance.AttributeOperator = @operator;
            Instance.AttributeValue = value;

            return this;
        }

        public CssSelectorComponentBuilder PseudoClass(string pseudoClass)
        {
            CodeContract.Begin(pseudoClass, "pseudoClass").CannotBe.NullOrEmpty();
            Instance.PseudoSelector = pseudoClass;
            return this;
        }

        public CssSelectorComponentBuilder PseudoElement(string pseudoElement)
        {
            CodeContract.Begin(pseudoElement, "pseudoElement").CannotBe.NullOrEmpty();
            Instance.PseudoElement = pseudoElement;
            return this;
        }

        #endregion
    }
}