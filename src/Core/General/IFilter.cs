using System;
using System.Collections.Generic;

namespace M4Graphs.Core.General
{
    public interface IFilter<TFiltering>
    {
        List<Predicate<TFiltering>> GetFilters();
    }
}
