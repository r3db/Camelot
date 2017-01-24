using System;
using System.IO;
using Camelot.Builders;
using NUnit.Framework;

namespace Camelot
{
    [TestFixture]
    public sealed class HtmlParserSpecs
    {
        #region Methods

        [Test]
        public void AssertParseMethodFromReference001()
        {
            // Arrange
            var parser = new HtmlParser(File.ReadAllText(@"..\..\..\Resources\Reference-001.htm"));

            var expected = HtmlElementBuilder.Begin
                .Kind("root")
                .Parent(null)
                .Descendent(HtmlElementBuilder.Begin
                    .Kind("#declaration")
                    .Parent("root")
                    .Content("DOCTYPE html")
                    .Build())
                .Descendent(HtmlElementBuilder.Begin
                    .Kind("#text")
                    .Parent("root")
                    .Content("\r\nInvalid 1\r\nInvalid 2\r\n")
                    .Build())
                .Descendent(HtmlElementBuilder.Begin
                    .Kind("div")
                    .Parent("root")
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#text")
                        .Parent("div")
                        .Content("\r\n    ")
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("span")
                        .Parent("div")
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("span")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("meta")
                            .Parent("span")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("span")
                            .Content("\r\n        Weird\r\n")
                            .Build())
                        .Build())
                    .Build())
                .Descendent(HtmlElementBuilder.Begin
                    .Kind("#text")
                    .Parent("root")
                    .Content("\r\n")
                    .Build())
                .Descendent(HtmlElementBuilder.Begin
                    .Kind("html")
                    .Parent("root")
                    .Attribute("checked")
                    .Attribute("home", "a")
                    .Attribute("harvest", "http://www.sapo.")
                    .Attribute("pt")
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#text")
                        .Parent("html")
                        .Content("\r\n    Invalid 3\r\n    ")
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#comment")
                        .Parent("html")
                        .Content("")
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#text")
                        .Parent("html")
                        .Content("\r\n    ")
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#comment")
                        .Parent("html")
                        .Content("Comment")
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#text")
                        .Parent("html")
                        .Content("\r\n    ")
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("head")
                        .Parent("html")
                        .Attribute("home", "a")
                        .Attribute("checked")
                        .Attribute("harvest", "http://www.sapo. pt")
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("link")
                            .Parent("head")
                            .Attribute("rel", "author1")
                            .Attribute("href", "humans1.txt")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("link")
                            .Parent("head")
                            .Attribute("rel", "author2")
                            .Attribute("href", "humans2.txt")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("meta")
                            .Parent("head")
                            .Attribute("name", "apple-mobile-web-app-capable1")
                            .Attribute("content", "no")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("meta")
                            .Parent("head")
                            .Attribute("name", "apple-mobile-web-app-capable2")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("meta")
                            .Parent("head")
                            .Attribute("name", "apple-mobile-web-app-capable3")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("title")
                            .Parent("head")
                            .Content("Read this.")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("#text")
                                .Parent("title")
                                .Content("Read this.")
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("div")
                            .Parent("head")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("#text")
                                .Parent("div")
                                .Content("\r\n            ")
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("A")
                                    .Build())
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("#text")
                                .Parent("div")
                                .Content("\r\n        ")
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("script")
                            .Parent("head")
                            .Attribute("src", "http://code.jquery.com/jquery-1.10.1.min.js")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("style")
                            .Parent("head")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("#text")
                                .Parent("style")
                                .Content("\r\n            body {\r\n                font-family: Segoe UI;\r\n                font-size: 14px;\r\n            }\r\n        ")
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("link")
                            .Parent("head")
                            .Attribute("rel", "author3")
                            .Attribute("href", "humans3.txt")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("head")
                            .Content("\r\n    ")
                            .Build())
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#text")
                        .Parent("html")
                        .Content("\r\n    ")
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("body")
                        .Parent("html")
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("h4")
                            .Parent("body")
                            .Attribute("itemscope")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("#text")
                                .Parent("h4")
                                .Content("\r\n            ")
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("h4")
                                .Attribute("itemprop1", string.Empty)
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("\r\n                Jorge Jesus\r\n                ")
                                    .Build())
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("span")
                                    .Parent("span")
                                    .Attribute("itemprop2", string.Empty)
                                    .Attribute("class", string.Empty)
                                    .Descendent(HtmlElementBuilder.Begin
                                        .Kind("#text")
                                        .Parent("span")
                                        .Content("Por Pedro Azevedo")
                                        .Build())
                                    .Build())
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("\r\n                ")
                                    .Build())
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("span")
                                    .Parent("span")
                                    .Attribute("itemprop3", string.Empty)
                                    .Attribute("class", string.Empty)
                                    .Descendent(HtmlElementBuilder.Begin
                                        .Kind("#text")
                                        .Parent("span")
                                        .Content("(rr.sapo.pt)")
                                        .Build())
                                    .Build())
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("\r\n        ")
                                    .Build())
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        Some Text here 1!\r\n        Some Text here 2!")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("hr")
                            .Parent("body")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("hr")
                            .Parent("body")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("div")
                            .Parent("body")
                            .Attribute("style", "background-color:red;position:absolute;left:020px;top:100px;")
                            .Attribute("class", "a1")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("1000")
                                    .Build())
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Attribute("class", "a2")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("$")
                                    .Build())
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("div")
                            .Parent("body")
                            .Attribute("style", "background-color:red;position:absolute;left:340px;top:200px;")
                            .Attribute("class", "b1")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("2000")
                                    .Build())
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Attribute("class", "b2")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("&cent;")
                                    .Build())
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("div")
                            .Parent("body")
                            .Attribute("style", "background-color:red;position:absolute;left:742px;top:530px;")
                            .Attribute("class", "c1")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("3000")
                                    .Build())
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Attribute("class", "c2")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("&pound;")
                                    .Build())
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("div")
                            .Parent("body")
                            .Attribute("style", "background-color:red;position:absolute;left:384px;top:344px;")
                            .Attribute("class", "d1")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("4000")
                                    .Build())
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Attribute("class", "d2")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("&yen;")
                                    .Build())
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("div")
                            .Parent("body")
                            .Attribute("style", "background-color:red;position:absolute;left:653px;top:127px;")
                            .Attribute("class", "e1")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("5000")
                                    .Build())
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Attribute("class", "e2")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("&#128;")
                                    .Build())
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("div")
                            .Parent("body")
                            .Attribute("style", "background-color:red;position:absolute;left:213px;top:654px;")
                            .Attribute("class", "f1")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("6000")
                                    .Build())
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Attribute("class", "f2")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("&#x20A3;")
                                    .Build())
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n        ")
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("div")
                            .Parent("body")
                            .Attribute("style", "background-color:red;position:absolute;left:218px;top:658px;")
                            .Attribute("class", "g1")
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("7000")
                                    .Build())
                                .Build())
                            .Descendent(HtmlElementBuilder.Begin
                                .Kind("span")
                                .Parent("div")
                                .Attribute("class", "g2")
                                .Descendent(HtmlElementBuilder.Begin
                                    .Kind("#text")
                                    .Parent("span")
                                    .Content("   &#x22; ")
                                    .Build())
                                .Build())
                            .Build())
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("body")
                            .Content("\r\n    ")
                            .Build())
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#text")
                        .Parent("html")
                        .Content("\r\n\r\n    ")
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("script")
                        .Parent("html")
                        .Attribute("type", "text/javascript")
                        .Descendent(HtmlElementBuilder.Begin
                            .Kind("#text")
                            .Parent("script")
                            .Content("\r\n\r\n        function Vector(x, y)\r\n        {\r\n            this.X = x;\r\n            this.Y = y;\r\n            this.Z = 0;\r\n        }\r\n\r\n    ")
                            .Build())
                        .Build())
                    .Descendent(HtmlElementBuilder.Begin
                        .Kind("#text")
                        .Parent("html")
                        .Content("\r\n")
                        .Build())
                    .Build())
                .Descendent(HtmlElementBuilder.Begin
                    .Kind("#text")
                    .Parent("root")
                    .Content("\r\nLast")
                    .Build())
                .Build();

            // Act
            var actual = parser.Parse();

            // Assert
            Assert.IsTrue(HtmlElementExtensions.AreEqual(expected, actual));
        }

        #endregion
    }
}