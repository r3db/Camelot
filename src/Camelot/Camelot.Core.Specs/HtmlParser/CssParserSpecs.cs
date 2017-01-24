using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Camelot.Builders;
using NUnit.Framework;

namespace Camelot
{
    [TestFixture]
    public sealed class CssParserSpecs
    {
        #region Methods

        [Test]
        public void AssertParseMethodFromReference001()
        {
            // Arrange
            var parser = new CssParser(File.ReadAllText(@"..\..\..\Resources\Reference-001.css"));

            var expected = new List<CssSelector>
            {
                CssSelectorBuilder.Begin
                    .Component(CssSelectorComponentBuilder.Begin
                        .Kind("aaa")
                        .Attribute("data-mode1", CssAttributeOperator.IsMatch, "boxes1")
                        .Build(), CssCombinator.Descendent)
                    .Component(CssSelectorComponentBuilder.Begin
                        .AppendClass("tabs-box")
                        .Build(), CssCombinator.Descendent)
                    .Component(CssSelectorComponentBuilder.Begin
                        .AppendClass("tab")
                        .Build(), CssCombinator.DirectSibling)
                    .Component(CssSelectorComponentBuilder.Begin
                        .Kind("h2")
                        .PseudoClass("before1")
                        .Build())
                    .Build(),

                CssSelectorBuilder.Begin
                    .Component(CssSelectorComponentBuilder.Begin
                        .AppendClass("tabs-box")
                        .Attribute("data-mode2", CssAttributeOperator.ContainsInList, "boxes2")
                        .Build(), CssCombinator.Descendent)
                    .Component(CssSelectorComponentBuilder.Begin
                        .AppendClass("tab")
                        .Build(), CssCombinator.Descendent)
                    .Component(CssSelectorComponentBuilder.Begin
                        .Kind("h3")
                        .PseudoClass("before2")
                        .Build())
                    .Build(),

                CssSelectorBuilder.Begin
                    .Component(CssSelectorComponentBuilder.Begin
                        .Kind("a")
                        .Identifier("b")
                        .AppendClass("tabs-box")
                        .Attribute("data-mode2", CssAttributeOperator.Any, "boxes3")
                        .PseudoElement("after")
                        .Build(), CssCombinator.Descendent)
                    .Component(CssSelectorComponentBuilder.Begin
                        .AppendClass("tab")
                        .Build(), CssCombinator.Descendent)
                    .Component(CssSelectorComponentBuilder.Begin
                        .Kind("h4")
                        .PseudoClass("before3")
                        .Build(), CssCombinator.Descendent)
                    .Component(CssSelectorComponentBuilder.Begin
                        .Attribute("a", CssAttributeOperator.IsMatch, "b")
                        .Build())
                    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("div")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("hr")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("html")
                //        .Build())
                //    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("a")
                //        .AppendClass("b")
                //        .AppendClass("c")
                //        .AppendClass("d")
                //        .AppendClass("e")
                //        .AppendClass("f")
                //        .PseudoClass("before")
                //        .Build(), CssCombinator.DirectSibling)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("x")
                //        .AppendClass("y")
                //        .AppendClass("z")
                //        .PseudoClass("after")
                //        .Build())
                //    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("w")
                //        .AppendClass("b")
                //        .AppendClass("c")
                //        .AppendClass("d")
                //        .AppendClass("e")
                //        .AppendClass("f")
                //        .PseudoClass("before")
                //        .Build(), CssCombinator.DirectDescendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("k")
                //        .AppendClass("y")
                //        .AppendClass("z")
                //        .PseudoClass("after")
                //        .Build(), CssCombinator.Sibling)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("k")
                //        .PseudoClass("afteryou")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("diva")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("divb")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("divc")
                //        .Build())
                //    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("div")
                //        .AppendClass("a")
                //        .AppendClass("b")
                //        .AppendClass("c")
                //        .AppendClass("d")
                //        .AppendClass("e")
                //        .AppendClass("f")
                //        .PseudoClass("pseudo")
                //        .Build())
                //    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("hr1")
                //        .AppendClass("a")
                //        .AppendClass("b")
                //        .PseudoClass("before")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("hr2")
                //        .AppendClass("x")
                //        .AppendClass("y")
                //        .PseudoClass("after")
                //        .Build())
                //    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("oldie")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("time_track1")
                //        .PseudoClass("before")
                //        .Build())
                //    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("oldie")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("time_track2")
                //        .PseudoClass("after")
                //        .Build())
                //    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("oldie")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("time_track3")
                //        .PseudoClass("before")
                //        .Build())
                //    .Build(),

                //CssSelectorBuilder.Begin
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("oldie")
                //        .Build(), CssCombinator.Descendent)
                //    .Component(CssSelectorComponentBuilder.Begin
                //        .Kind("*")
                //        .AppendClass("time_track4")
                //        .PseudoClass("after")
                //        .Build())
                //    .Build(),
            };

            // Act
            var actual = parser.Parse().ToList();

            // Assert
            Assert.IsTrue(CssSelectorExtensions.AreEqual(expected, actual));
        }

        #endregion
    }
}