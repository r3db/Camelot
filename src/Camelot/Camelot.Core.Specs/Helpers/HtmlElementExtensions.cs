using System;
using System.Collections.Generic;
using System.Linq;
using Epiphany.Extensions;

namespace Camelot
{
    public static class HtmlElementExtensions
    {
        #region Methods

        // Todo: Refactor!
        public static bool AreEqual(HtmlElement expected, HtmlElement actual)
        {
            var expectedFlat = MakeNodeHierarchyFlat(expected).ToList();
            var actualFlat = MakeNodeHierarchyFlat(actual).ToList();

            var minElements = Math.Min(expectedFlat.Count, actualFlat.Count);

            for (int i = 0; i < minElements; ++i)
            {
                if (expectedFlat[i].Kind != actualFlat[i].Kind)
                {
                    Console.WriteLine("Expected Kind: {0}", expectedFlat[i].Kind);
                    Console.WriteLine("Actual Kind  : {0}", actualFlat[i].Kind);
                    return false;
                }

                if (AreParentKindsEqual(expectedFlat[i].Parent, actualFlat[i].Parent) == false)
                {
                    Console.WriteLine("Expected Parent Kind: {0}", DisplayParentKind(expectedFlat[i].Parent));
                    Console.WriteLine("Actual Parent Kind  : {0}", DisplayParentKind(actualFlat[i].Parent));
                    return false;
                }

                if (expectedFlat[i].HasDescendents != actualFlat[i].HasDescendents)
                {
                    Console.WriteLine("Expected Is Leaf: {0}", expectedFlat[i].HasDescendents);
                    Console.WriteLine("Actual Is Leaf  : {0}", actualFlat[i].HasDescendents);
                    return false;
                }

                if (expectedFlat[i].HasDescendents)
                {
                    if (expectedFlat[i].Content != actualFlat[i].Content)
                    {
                        Console.WriteLine("Expected Content: {0}", expectedFlat[i].Content);
                        Console.WriteLine("Actual Content  : {0}", actualFlat[i].Content);
                        return false;
                    }
                }

                var expectedAttributes = expectedFlat[i].Attributes.ToList();
                var actualAttributes = actualFlat[i].Attributes.ToList();

                var minAttributes = Math.Min(expectedAttributes.Count, actualAttributes.Count);

                for (int k = 0; k < minAttributes; ++k)
                {
                    if (expectedAttributes[k].Key != actualAttributes[k].Key)
                    {
                        Console.WriteLine("Expected Attribute Key: {0}", expectedAttributes[k].Key);
                        Console.WriteLine("Actual Attribute Key  : {0}", actualAttributes[k].Key);
                        return false;
                    }

                    if (expectedAttributes[k].Value != actualAttributes[k].Value)
                    {
                        Console.WriteLine("Expected Attribute Value: {0}", expectedAttributes[k].Value);
                        Console.WriteLine("Actual Attribute Value  : {0}", actualAttributes[k].Value);
                        return false;
                    }
                }

                if (expectedAttributes.Count != actualAttributes.Count)
                {
                    Console.WriteLine("Expected Attribute Count: {0}", expectedAttributes.Count);
                    Console.WriteLine("Actual Attribute Count  : {0}", actualAttributes.Count);
                    return false;
                }
            }

            if (expectedFlat.Count != actualFlat.Count)
            {
                Console.WriteLine("Expected Node Count: {0}", expectedFlat.Count);
                Console.WriteLine("Actual Node Count  : {0}", actualFlat.Count);
                return false;
            }

            return true;
        }

        #endregion

        #region Helpers

        private static bool AreParentKindsEqual(HtmlElement first, HtmlElement second)
        {
            if (first == null && second == null)
            {
                return true;
            }

            if ((first != null && second == null) || first == null)
            {
                return false;
            }

            return first.Kind.OrdinalEquals(second.Kind);
        }

        private static string DisplayParentKind(HtmlElement element)
        {
            return element == null ? null : element.Kind;
        }

        private static IEnumerable<HtmlElement> MakeNodeHierarchyFlat(HtmlElement node)
        {
            var result = new List<HtmlElement>();
            MakeNodeHierarchyFlat(node, result);
            return result;
        }

        private static void MakeNodeHierarchyFlat(HtmlElement node, ICollection<HtmlElement> store)
        {
            store.Add(node);

            foreach (var item in node.GetDescendents())
            {
                MakeNodeHierarchyFlat(item, store);
            }
        }

        #endregion
    }
}