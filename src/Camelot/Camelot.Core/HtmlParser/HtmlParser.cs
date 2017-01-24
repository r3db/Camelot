using System;
using System.Collections.Generic;
using System.Text;
using Axiomatic;
using Camelot.Collections.Specialized;
using Camelot.Extensions;
using Epiphany.Extensions;

namespace Camelot
{
    public sealed class HtmlParser
    {
        #region Internal Static Data

        private static readonly ISet<string> voidElements = new HashSet<string>
        {
            "meta",
            "link",
            "hr",
            "input",
            "img",
            "br",
        };

        #endregion

        #region Internal Instance Data

        private readonly TokenNavigator navigator;
        private readonly HtmlElementParser elementParser;
        private readonly HtmlElement root = new HtmlElement("root");
        private readonly HtmlElementStack elementStack = new HtmlElementStack();
        private readonly StringBuilder scatteredText = new StringBuilder();

        #endregion

        #region .Ctor

        public HtmlParser(string html)
        {
            CodeContract.Begin(html, "html").CannotBe.Null();
            
            navigator = new TokenNavigator(html);
            elementParser = new HtmlElementParser(navigator);
        }

        #endregion

        #region Methods

        public HtmlElement Parse()
        {
            var node = root;

            while (navigator.CanMoveNext())
            {
                char token = navigator.ReadNext();

                switch (token)
                {
                    case '<':
                    {
                        CollectScatteredText(node);
                        // It can be a Comment.      <!
                        // It can be a DOCTYPE.      <!
                        // It can be a closing tag.  </
                        // It can be an opening tag. <
                        node = ParseElement(node);
                        break;
                    }
                    default:
                    {
                        // Collect scattered text.
                        scatteredText.Append(token);
                        break;
                    }
                }
            }

            CollectScatteredText(node);

            return root;
        }

        #endregion

        #region Helpers

        private HtmlElement ParseElement(HtmlElement node)
        {
            char token = navigator.ReadNext();

            if (HtmlTagNameParser.IsValidToken(token))
            {
                // Case: Regular TagName
                // Link: http://www.w3.org/TR/html-markup/syntax.html#tag-name
                node = ParseHtmlElement(node);
            }
            else
            {
                switch (token)
                {
                    case '!':
                    {
                        // Case: Possible Comment!
                        // Case: Possible DocType!
                        navigator.MoveNext();
                        ParseDeclarationOrToken(node);
                        break;
                    }
                    case '/':
                    {
                        // Case: Ending TagName
                        navigator.MoveNext();
                        node = ParseClosingElement();
                        break;
                    }
                    default:
                    {
                        // Todo: Signal what is wrong and where!
                        throw new InvalidOperationException();
                    }
                }
            }

            return node;
        }

        private HtmlElement ParseHtmlElement(HtmlElement node)
        {
            HtmlElement element = elementParser.Parse(elementStack.SafePeek(root));
            node.AddDescendent(element);

            navigator.MoveToNextNonWhitespaceToken();

            if (navigator.ReadCurrent() == '/')
            {
                navigator.MoveNext();
            }
            else
            {
                elementStack.Push(element);
                node = element;
            }

            navigator.AssertCurrentTokenIs('>');

            return node;
        }

        private void ParseDeclarationOrToken(HtmlElement node)
        {
            if (navigator.ReadCurrent() == '-')
            {
                // Possible Comment
                navigator.MoveNext();
                navigator.AssertCurrentTokenIs('-');
                ParseComment(node);
            }
            else
            {
                // Possible Comment
                ParseDeclaration(node);
            }
        }

        private void ParseComment(HtmlElement node)
        {
            var sb = new StringBuilder();

            while (sb.Length < 3 || sb.EndsWith("-->") == false)
            {
                sb.Append(navigator.ReadNext());
            }

            node.AddDescendent(new HtmlElement("#comment", sb.ToString().Substring(0, sb.Length - 3))
            {
                Parent = elementStack.SafePeek(root),
            });
        }

        private void ParseDeclaration(HtmlElement node)
        {
            // Possible DocType
            var sb = new StringBuilder();

            sb.Append(navigator.ReadCurrent());
            sb.Append(navigator.ReadNext());

            while (sb.Length < 1 || sb.EndsWith(">") == false)
            {
                sb.Append(navigator.ReadNext());
            }

            node.AddDescendent(new HtmlElement("#declaration", sb.ToString().Substring(0, sb.Length - 1))
            {
                Parent = elementStack.SafePeek(root)
            });
        }

        private HtmlElement ParseClosingElement()
        {
            var last = elementStack.Pop();
            HtmlElement name = elementParser.Parse(elementStack.SafePeek(root));

            if (last.Kind.OrdinalEquals(name.Kind) == false)
            {
                FixElementTree(last, name);
            }

            return elementStack.SafePeek(root);
        }

        // Note: It accounts for void elements and for unclosed tags.
        private void FixElementTree(HtmlElement last, HtmlElement name)
        {
            while (last.Kind.OrdinalEquals(name.Kind) == false)
            {
                var parent = elementStack.SafePop(root);

                if (voidElements.Contains(last.Kind))
                {
                    parent.AddDescendents(last.GetDescendents());
                    last.RemoveDescendents();
                }
                else
                {
                    // Todo: Log!
                    Console.WriteLine("Unclose tag: '{0}'", last.Kind);
                }

                last = parent;
            }
        }

        private void CollectScatteredText(HtmlElement node)
        {
            if (scatteredText.Length == 0)
            {
                return;
            }

            node.AddDescendent(new HtmlElement("#text", scatteredText.ToString())
            {
                Parent = elementStack.SafePeek(root)
            });

            scatteredText.Clear();
        }

        #endregion
    }
}