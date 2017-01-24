using System;
using System.Threading;
using Axiomatic;
using Epiphany;

namespace Camelot.Builders
{
    internal sealed class HtmlElementBuilder : FluentBuilder<HtmlElement>
    {
        #region Internal Static Data

        private static readonly ThreadLocal<Arbitrary> arbitrary = new ThreadLocal<Arbitrary>(() => new Arbitrary());

        #endregion

        #region .Ctor

        private HtmlElementBuilder(HtmlElement node)
            : base(node)
        {
        }

        #endregion

        #region Factory .Ctor

        public static HtmlElementBuilder Begin
        {
            get
            {
                return new HtmlElementBuilder(new HtmlElement(arbitrary.Value.NextString()));
            }
        }

        #endregion

        #region Methods

        public HtmlElementBuilder Kind(string kind)
        {
            CodeContract.Begin(kind, "kind").CannotBe.NullOrEmpty();

            EnforceProperty(() => Instance.Kind, kind);
            return this;
        }

        public HtmlElementBuilder Descendent(HtmlElement element)
        {
            CodeContract.Begin(element, "element").CannotBe.Null();
            var parent = element.Parent;
            Instance.AddDescendent(element);
            element.Parent = parent;
            return this;
        }

        public HtmlElementBuilder Attribute(string key)
        {
            return Attribute(key, key);
        }

        public HtmlElementBuilder Attribute(string key, string value)
        {
            CodeContract.Begin(key, "key").CannotBe.NullOrEmpty();
            CodeContract.Begin(value, "value").CannotBe.Null();
            Instance.Attributes.Add(new HtmlAttribute(key, value));
            return this;
        }

        public HtmlElementBuilder Content(string content)
        {
            EnforceProperty(() => Instance.Content, content);
            return this;
        }

        public HtmlElementBuilder Parent(string kind)
        {
            Instance.Parent = kind == null
                ? null
                : new HtmlElement(kind);

            return this;
        }

        #endregion
    }
}