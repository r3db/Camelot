using System;
using System.Collections.Generic;
using System.Text;
using Epiphany.Extensions;

namespace Camelot
{
    public sealed class CssSelector
    {
        // Todo: Should this be private!
        public CssSelector(IList<CssSelectorComponent> selectors, IList<CssCombinator> combinators)
        {
            Selectors = selectors;
            Combinators = combinators;
        }

        public IList<CssSelectorComponent> Selectors { get; private set; }

        public IList<CssCombinator> Combinators { get; private set; }

        public void Add(CssSelectorComponent selector)
        {
            Selectors.Add(selector);
        }

        public void Add(CssSelectorComponent selector, CssCombinator combinator)
        {
            Selectors.Add(selector);
            Combinators.Add(combinator);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Selectors.Count; i++)
            {
                var item = Selectors[i];

                if (i + 1 == Selectors.Count)
                {
                    sb.Append(item);
                }
                else
                {
                    sb.Append(item);
                }

                //if (i + 1 < Selectors.Count)
                //{
                //    var format = Combinators[i] == CssCombinator.Descendent ? "{0}" : " {0} ";
                //    sb.Append(format.ToInvariantFormat(GetCombinatorAsString(Combinators[i])));
                //}
            }

            return sb.ToString();
        }

        private static string GetCombinatorAsString(CssCombinator combinator)
        {
            switch (combinator)
            {
                case CssCombinator.Descendent:       return " ";
                case CssCombinator.DirectDescendent: return ">";
                case CssCombinator.Sibling:          return "~";
                case CssCombinator.DirectSibling:    return "+";
            }

            throw new NotSupportedException();
        }
    }
}