using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinksAggregator.Models
{
    public class Pagination
    {
        public Pagination()
        {
            this.IsEnabled = false;
        }
        public Pagination(bool isEnabled, int page)
        {
            this.IsEnabled = isEnabled;
            this.Page = page;
            this.Size = 100;
        }
        public bool IsEnabled { get; set; } = false;
        public int Page { get; set; }
        public int Size { get; set; }
        public int PagesAmount { get; set; }

        public int NextPage { get { return this.Page + 1; } }
        public int PreviousPage { get { return this.Page - 1; } }
    }
}
