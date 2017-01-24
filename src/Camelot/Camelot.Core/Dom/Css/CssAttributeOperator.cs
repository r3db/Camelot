using System;

namespace Camelot
{
    internal enum CssAttributeOperator
    {
        IsPresent,        // 
        IsMatch,          // =
        // Todo: Rename
        ContainsInList,   // =|
        Any,              // =~
        StartsWith,       // =^
        EndsWith,         // =$
        Contains,         // =*
    }
}