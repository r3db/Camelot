using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epiphany.Extensions;

namespace Camelot
{
    public sealed class CssSelectorComponent
    {
        #region .Ctor

        public CssSelectorComponent()
            : this(null)
        {
        }

        private CssSelectorComponent(string kind)
            : this(kind, new List<string>())
        {
        }


        public CssSelectorComponent(string kind, IEnumerable<string> classes)
        {
            Kind = kind;
            // Todo: Don't do this!
            Classes = classes.ToList();
        }

        #endregion

        #region Auto Implemented Properties

        public string Kind { get; private set; }

        public string Identifier { get; set; }

        public IList<string> Classes { get; private set; }

        public string PseudoSelector { get; set; }

        public string PseudoElement { get; set; }

        public string AttributeKey { get; set; }

        internal CssAttributeOperator? AttributeOperator { get; set; }

        public string AttributeValue { get; set; }

        #endregion

        // Todo: Remove!
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(Kind) || string.IsNullOrWhiteSpace(Kind))
            {
                sb.Append("*");
            }
            else
            {
                sb.Append(Kind);
            }

            if (Identifier != null)
            {
                sb.Append("#{0}".ToInvariantFormat(Identifier));
            }

            if (Classes.IsEmpty() == false)
            {
                sb.Append(".{0}".ToInvariantFormat(string.Join(".", Classes)));
            }

            if (string.IsNullOrEmpty(AttributeKey) == false)
            {
                sb.Append(string.IsNullOrEmpty(AttributeValue) == false && AttributeOperator.HasValue
                    ? "[{0}{1}\"{2}\"]".ToInvariantFormat(AttributeKey, GetTextualAttributeOperator(AttributeOperator.Value), AttributeValue)
                    : "[{0}]".ToInvariantFormat(AttributeKey));
            }

            if (PseudoSelector != null)
            {
                sb.Append(":{0}".ToInvariantFormat(PseudoSelector));
            }

            if (PseudoElement != null)
            {
                sb.Append("::{0}".ToInvariantFormat(PseudoElement));
            }

            return sb.ToString();
        }

        private static string GetTextualAttributeOperator(CssAttributeOperator @operator)
        {
            switch (@operator)
            {
                case CssAttributeOperator.IsMatch:
                    return "=";
                case CssAttributeOperator.ContainsInList:
                    return "|=";
                case CssAttributeOperator.Any:
                    return "~=";
                case CssAttributeOperator.StartsWith:
                    return "^=";
                case CssAttributeOperator.EndsWith:
                    return "$=";
                case CssAttributeOperator.Contains:
                    return "*=";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}