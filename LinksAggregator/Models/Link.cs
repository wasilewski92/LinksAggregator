using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinksAggregator.Models
{
    public class Link
    {        
        public long Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Data dodania")]
        [DataType(DataType.DateTime)]
        public DateTime AddingDate { get; set; }
    }
}
