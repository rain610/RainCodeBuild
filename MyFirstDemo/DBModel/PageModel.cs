using System;
using System.Collections.Generic;
using System.Text;

namespace DBModel
{
    public class PageModel<T>
    {
        public IList<T> rows { get; set; }
        public long total { get; set; }
    }
}
