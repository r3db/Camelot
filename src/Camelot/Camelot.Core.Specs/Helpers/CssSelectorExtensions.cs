using System;
using System.Collections.Generic;
using Epiphany.Extensions;

namespace Camelot
{
    public static class CssSelectorExtensions
    {
        #region Methods

        // Todo: Refactor!
        public static bool AreEqual(IList<CssSelector> expected, IList<CssSelector> actual)
        {
            var minSelectors = Math.Min(expected.Count, actual.Count);

            for (int i = 0; i < minSelectors; ++i)
            {
                var minComponentCombinators = Math.Min(expected[i].Combinators.Count, actual[i].Combinators.Count);

                for (int k = 0; k < minComponentCombinators; ++k)
                {
                    var expectedCombinator = expected[i].Combinators[k];
                    var actualCombinator = actual[i].Combinators[k];

                    if (expectedCombinator != actualCombinator)
                    {
                        Console.WriteLine("Expected Combinators: {0}", expectedCombinator);
                        Console.WriteLine("Actual Combinators  : {0}", actualCombinator);
                        return false;
                    }
                }

                if (expected[i].Combinators.Count != actual[i].Combinators.Count)
                {
                    Console.WriteLine("Expected Combinators Count: {0}", expected[i].Combinators.Count);
                    Console.WriteLine("Actual Combinators Count  : {0}", actual[i].Combinators.Count);
                    return false;
                }

                var minComponentSelectors = Math.Min(expected[i].Selectors.Count, actual[i].Selectors.Count);

                for (int k = 0; k < minComponentSelectors; ++k)
                {
                    var expectedSelector = expected[i].Selectors[k];
                    var actualSelector = actual[i].Selectors[k];

                    if (expectedSelector.Kind != actualSelector.Kind)
                    {
                        Console.WriteLine("Expected Kind: {0}", expectedSelector.Kind);
                        Console.WriteLine("Actual Kind  : {0}", actualSelector.Kind);
                        return false;
                    }

                    if (expectedSelector.PseudoSelector != actualSelector.PseudoSelector)
                    {
                        Console.WriteLine("Expected PseudoSelector: {0}", expectedSelector.PseudoSelector);
                        Console.WriteLine("Actual PseudoSelector  : {0}", actualSelector.PseudoSelector);
                        return false;
                    }

                    var minClasses = Math.Min(expectedSelector.Classes.Count, actualSelector.Classes.Count);

                    for (int w = 0; w < minClasses; ++w)
                    {
                        var expectedClass = expectedSelector.Classes[w];
                        var actualClass = actualSelector.Classes[w];

                        if (expectedClass != actualClass)
                        {
                            Console.WriteLine("Expected Class: {0}", expectedClass);
                            Console.WriteLine("Actual Class  : {0}", actualClass);
                            return false;
                        }
                    }

                    if (expectedSelector.Classes.Count != actualSelector.Classes.Count)
                    {
                        Console.WriteLine("Expected Class Count: {0}", expectedSelector.Classes.Count);
                        Console.WriteLine("Actual Class Count  : {0}", actualSelector.Classes.Count);
                        return false;
                    }
                }

                if (expected[i].Selectors.Count != actual[i].Selectors.Count)
                {
                    Console.WriteLine("Expected Selectors Count: {0}", expected[i].Selectors.Count);
                    Console.WriteLine("Actual Selectors Count  : {0}", actual[i].Selectors.Count);
                    return false;
                }
            }

            if (expected.Count != actual.Count)
            {
                Console.WriteLine("Expected Count: {0}", expected.Count);
                Console.WriteLine("Actual Count  : {0}", actual.Count);
                return false;
            }

            return true;
        }

        #endregion
    }
}