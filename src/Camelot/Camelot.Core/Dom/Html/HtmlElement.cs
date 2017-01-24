using System;
using System.Collections.Generic;
using System.Diagnostics;
using Axiomatic;
using Camelot.Collections.Specialized;
using Epiphany.Extensions;

namespace Camelot
{
    [DebuggerDisplay("{Kind} : {Parent.Kind}")]
    public sealed class HtmlElement
    {
        #region Internal Instance Data

        private readonly List<HtmlElement> descendents;

        #endregion

        #region .Ctor

        public HtmlElement(string kind)
            : this(kind, string.Empty)
        {
        }

        public HtmlElement(string kind, string content)
            : this(kind, content, new HtmlAttributeSet())
        {
        }

        public HtmlElement(string kind, HtmlAttributeSet attributes)
            : this(kind, string.Empty, attributes)
        {
        }

        private HtmlElement(string kind, string content, HtmlAttributeSet attributes)
        {
            CodeContract.Begin(kind, "kind").CannotBe.NullOrEmpty();
            CodeContract.Begin(content, "content").CannotBe.Null();
            CodeContract.Begin(attributes, "attributes").CannotBe.Null();
            
            Kind = kind.ToLowerInvariant();
            Content = content;
            Attributes = attributes;
            descendents = new List<HtmlElement>();
        }

        #endregion

        #region Auto Implemented Properties

        public string Kind { get; private set; }

        public string Content { get; private set; }

        public HtmlAttributeSet Attributes { get; private set; }

        public HtmlElement Parent { get; set; }

        #endregion

        #region Properties

        public bool HasDescendents
        {
            get { return descendents.IsEmpty(); }
        }

        #endregion

        #region Methods

        public IEnumerable<HtmlElement> GetDescendents()
        {
            return descendents;
        }

        public void AddDescendents(IEnumerable<HtmlElement> elements)
        {
            CodeContract.Begin(elements, "elements").CannotBe.NullOrEmpty();
            
            foreach (var item in elements)
            {
                AddDescendent(item);
            }
        }

        public void AddDescendent(HtmlElement element)
        {
            CodeContract.Begin(element, "element").CannotBe.Null();
            element.Parent = this;
            descendents.Add(element);
        }

        public void RemoveDescendents()
        {
            descendents.Clear();
        }

        #endregion
    }
}