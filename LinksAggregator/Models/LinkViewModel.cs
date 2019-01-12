using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinksAggregator.Models
{
    public class LinkViewModel
    {
        public IEnumerable<LinksAggregator.Models.Link> Links { get; set; }
        public Pagination Pagination { get; set; }
    }
}
