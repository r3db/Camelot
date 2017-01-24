using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axiomatic;
using Camelot.Extensions;
using Epiphany.Extensions;

namespace Camelot
{
    public sealed class CssParser
    {
        #region Internal Instance Data

        private readonly TokenNavigator navigator;
        private readonly IList<CssSelector> selectors = new List<CssSelector>();
        private readonly IList<CssCombinator> combinators = new List<CssCombinator>();
        private readonly IList<CssSelectorComponent> components = new List<CssSelectorComponent>();

        // Simplify!!!
        private readonly StringBuilder textCollector = new StringBuilder();
        private readonly StringBuilder spaceCollector = new StringBuilder();
        private string kind;
        private string identifier;
        private IList<string> classCollector = new List<string>();
        private string pseudoSelector;
        private bool isPseudoElement;

        private string attributeKey ;
        private CssAttributeOperator? attributeOperator;
        private string attributeValue;

        // Todo: Remove 'canParse'!
        private bool canParse = true;
        private bool moveNext = true;

        #endregion

        #region .Ctor

        public CssParser(string css)
        {
            CodeContract.Begin(css, "html").CannotBe.Null();
            navigator = new TokenNavigator(css);
        }

        #endregion

        #region Methods

        public IEnumerable<CssSelector> Parse()
        {
            while (navigator.CanMoveNext())
            {
                char token = moveNext 
                    ? navigator.ReadNext() 
                    : navigator.ReadCurrent();

                moveNext = false;

                switch (token)
                {
                    #region +
                    case '+':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        ProcessCombinator(CssCombinator.DirectSibling);
                        moveNext = true;

                        break;
                    }
                    #endregion
                    #region ~
                    case '~':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        ProcessCombinator(CssCombinator.DirectSibling);
                        moveNext = true;

                        break;
                    }
                    #endregion
                    #region >
                    case '>':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        ProcessCombinator(CssCombinator.DirectSibling);
                        moveNext = true;

                        break;
                    }
                    #endregion
                    #region ' '
                    case ' ':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        CollectKind();

                        // Todo: There is a problem in the way we see combinators!
                        if (combinators.Count == 0 /* combinator == null */)
                        {
                            spaceCollector.Append(token);

                            if (spaceCollector.Length == 1)
                            {
                                ProcessLeftOvers();
                            }
                        }
                        else
                        {
                            ClearSpaceCollector();
                        }

                        if (HasTermComponents())
                        {
                            components.Add(AssemblePartialSelector());
                            ClearSpaceCollector();
                            ClearPartialSelectorStatus();
                        }

                        moveNext = true;
                        break;
                    }
                    #endregion
                    #region .
                    case '.':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        CollectCombinator();
                        ClearSpaceCollector();
                        CollectKind();

                        if (classCollector.IsEmpty() == false)
                        {
                            throw new DllNotFoundException();
                        }

                        IList<string> classes = new List<string>();

                        while (navigator.ReadCurrent() == '.')
                        {
                            navigator.MoveNext();
                            classes.Add(ParseIdentifierName());
                        }

                        classCollector = classes;

                        break;
                    }
                    #endregion
                    #region #
                    case '#':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        CollectCombinator();
                        ClearSpaceCollector();
                        CollectKind();

                        if (identifier != null)
                        {
                            throw new DllNotFoundException();
                        }

                        navigator.MoveNext();
                        identifier = ParseIdentifierName();

                        break;
                    }
                    #endregion
                    #region {
                    case '{':
                    {
                        ClearSpaceCollector();
                        CollectKind();

                        if (HasTermComponents())
                        {
                            components.Add(AssemblePartialSelector());
                        }

                        selectors.Add(new CssSelector(components.ToList(), combinators.ToList()));

                        components.Clear();
                        ClearCombinatorStatus();
                        ClearSpaceCollector();
                        ClearPartialSelectorStatus();
                        canParse = false;
                        moveNext = true;
                        break;
                    }
                    #endregion
                    #region ,
                    case ',':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        ClearSpaceCollector();
                        CollectKind();

                        if (HasTermComponents())
                        {
                            components.Add(AssemblePartialSelector());
                        }

                        selectors.Add(new CssSelector(components.ToList(), combinators.ToList()));

                        components.Clear();
                        ClearCombinatorStatus();
                        ClearSpaceCollector();
                        ClearPartialSelectorStatus();
                        canParse = false;
                        moveNext = true;
                        break;
                    }
                    #endregion
                    #region '}'
                    case '}':
                    {
                        ClearSpaceCollector();
                        canParse = true;
                        moveNext = true;
                        textCollector.Clear();
                        break;
                    }
                    #endregion
                    #region :
                    case ':':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        CollectCombinator();
                        ClearSpaceCollector();
                        CollectKind();

                        if (pseudoSelector != null)
                        {
                            throw new DllNotFoundException();
                        }
                        
                        navigator.MoveNext();

                        if (navigator.ReadCurrent() == ':')
                        {
                            isPseudoElement = true;
                            navigator.MoveNext();
                        }
                        else
                        {
                            isPseudoElement = false;
                        }

                        pseudoSelector = ParseIdentifierName();

                        moveNext = false;

                        break;
                    }
                    #endregion
                    #region [
                    case '[':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        CollectCombinator();
                        ClearSpaceCollector();
                        CollectKind();

                        navigator.MoveNext();
                        attributeKey = ParseIdentifierName();

                        switch (navigator.ReadCurrent())
                        {
                            case '=':
                            {
                                attributeOperator = CssAttributeOperator.IsMatch;
                                navigator.MoveNext();
                                attributeValue = ParseAttribute();

                                if (string.IsNullOrEmpty(attributeValue))
                                {
                                    throw new NotImplementedException();
                                }

                                break;
                            }
                            case '|':
                            case '~':
                            case '^':
                            case '$':
                            case '*':
                            {
                                var current = navigator.ReadCurrent();

                                if (navigator.ReadNext() == '=')
                                {
                                    switch (current)
                                    {
                                        case '|':
                                            attributeOperator = CssAttributeOperator.ContainsInList;
                                            break;
                                        case '~':
                                            attributeOperator = CssAttributeOperator.Any;
                                            break;
                                        case '^':
                                            attributeOperator = CssAttributeOperator.StartsWith;
                                            break;
                                        case '$':
                                            attributeOperator = CssAttributeOperator.EndsWith;
                                            break;
                                        case '*':
                                            attributeOperator = CssAttributeOperator.Contains;
                                            break;
                                        default:
                                            throw new NotImplementedException();
                                    }
                                    
                                    navigator.MoveNext();
                                    attributeValue = ParseAttribute();

                                    if (string.IsNullOrEmpty(attributeValue))
                                    {
                                        throw new NotImplementedException();
                                    }

                                    break;
                                }

                                throw new NotSupportedException();
                            }
                            case ']':
                            {
                                attributeOperator = CssAttributeOperator.IsPresent;
                                break;
                            }
                            default:
                            {
                                throw new NotSupportedException();
                            }
                        }

                        if (string.IsNullOrEmpty(attributeKey))
                        {
                            throw new NotImplementedException();
                        }

                        navigator.AssertCurrentTokenIs(']');

                        moveNext = true;

                        break;
                    }
                    #endregion
                    #region /
                    case '/':
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        // Possible Comment
                        navigator.MoveNext();
                        navigator.AssertCurrentTokenIs('*');

                        string commentDelimiter = new string('-', 50);
                        Console.WriteLine(commentDelimiter);
                        Console.WriteLine(ParseComment());
                        Console.WriteLine(commentDelimiter);
                        moveNext = true;

                        break;
                    }
                    #endregion
                    #region Default
                    default:
                    {
                        if (canParse == false)
                        {
                            moveNext = true;
                            break;
                        }

                        if (char.IsControl(token) == false)
                        {
                            textCollector.Append(token);

                            if (spaceCollector.Length > 0)
                            {
                                combinators.Add(CssCombinator.Descendent);
                                spaceCollector.Clear();
                            }
                        }

                        moveNext = true;
                        break;
                    }
                    #endregion
                }
            }

            return selectors;
        }

        #endregion

        #region Helpers

        private void ProcessLeftOvers()
        {
            if (HasTermComponents())
            {
                components.Add(AssemblePartialSelector());
                ClearPartialSelectorStatus();
                ClearCombinatorStatus();
            }
        }

        private bool HasTermComponents()
        {
            return kind != null ||
                   identifier != null ||
                   classCollector.Count > 0 ||
                   pseudoSelector != null ||
                   attributeKey != null ||
                   attributeOperator != null ||
                   attributeValue != null;
        }

        private void ProcessCombinator(CssCombinator combinator)
        {
            ClearSpaceCollector();
            combinators.Add(combinator);

            if (HasTermComponents())
            {
                components.Add(AssemblePartialSelector());
                ClearPartialSelectorStatus();
            }
        }

        private void CollectCombinator()
        {
            if (spaceCollector.Length > 0)
            {
                combinators.Add(CssCombinator.Descendent);
            }
        }
        
        private void ClearSpaceCollector()
        {
            if (spaceCollector.Length > 0)
            {
                spaceCollector.Clear();
            }
        }
        
        private void CollectKind()
        {
            if (textCollector.Length > 0)
            {
                kind = textCollector.ToString();
                textCollector.Clear();
            }
        }

        // Note: DRY! Sort Of!
        private string ParseAttribute()
        {
            var expectedQuote = navigator.ReadCurrent();

            switch (expectedQuote)
            {
                case '\'':
                case '"':
                {
                    navigator.MoveNext();

                    // Todo: Escape Characters
                    var value = ParseAttributeValue(expectedQuote);
                    navigator.AssertCurrentTokenIs(expectedQuote);
                    navigator.MoveNext();

                    return value;
                }
                default:
                {
                    // Todo: Escape Characters
                    return ParseIdentifierName();
                }
            }
        }

        // Note: DRY!
        private string ParseAttributeValue(char quote)
        {
            var token = navigator.ReadCurrent();

            var sb = new StringBuilder();

            while (token != quote)
            {
                sb.Append(token);
                token = navigator.ReadNext();
            }

            return sb.ToString();
        }

        private string ParseIdentifierName()
        {
            var sb = new StringBuilder();
            var token = navigator.ReadCurrent();

            while (IsValidToken(token))
            {
                sb.Append(token);
                token = navigator.ReadNext();
            }

            var result = sb.ToString();

            if (string.IsNullOrWhiteSpace(result))
            {
                throw new NotSupportedException();
            }

            return result;
        }

        private static bool IsValidToken(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-' || c == '_';
        }

        private string ParseComment()
        {
            var sb = new StringBuilder();

            while (sb.Length < 2 || sb.EndsWith("*/") == false)
            {
                sb.Append(navigator.ReadNext());
            }

            return sb.ToString().Substring(0, sb.Length - 2);
        }

        private CssSelectorComponent AssemblePartialSelector()
        {
            var component = new CssSelectorComponent(kind, classCollector)
            {
                Identifier = identifier
            };

            if (isPseudoElement)
            {
                component.PseudoElement = pseudoSelector;
            }
            else
            {
                component.PseudoSelector = pseudoSelector;
            }

            component.AttributeKey = attributeKey;
            component.AttributeOperator = attributeOperator;
            component.AttributeValue = attributeValue;

            Console.WriteLine(component);
            Console.WriteLine("-------------------");

            return component;
        }

        private void ClearPartialSelectorStatus()
        {
            kind = null;
            identifier = null;
            classCollector.Clear();
            pseudoSelector = null;
            textCollector.Clear();
            spaceCollector.Clear();
            attributeKey = null;
            attributeOperator = null;
            attributeValue = null;
            isPseudoElement = false;
        }

        private void ClearCombinatorStatus()
        {
            if (combinators.Count > 0)
            {
                combinators.Clear();
            }
        }

        #endregion
    }
}