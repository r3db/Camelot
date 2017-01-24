using System;

namespace Camelot
{
    public enum CssCombinator
    {
        Descendent,       // [ ]
        DirectDescendent, // [>]
        Sibling,          // [~]
        DirectSibling,    // [+]
    }
}