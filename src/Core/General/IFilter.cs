using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.Core.General
{
    public interface IFilter<TFiltering>
    {
        List<Predicate<TFiltering>> GetFilters();
    }
}
