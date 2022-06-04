using Common;
using System.Collections.Generic;

namespace Common
{
    public class PagedList<T>: Pagination where T: class
    {
        public IEnumerable<T> Results { get; set; }
    }
}
