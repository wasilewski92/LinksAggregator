using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LinksAggregator.Models
{
    public class LinksAggregatorContext : DbContext
    {
        public LinksAggregatorContext (DbContextOptions<LinksAggregatorContext> options)
            : base(options)
        {
        }

        public DbSet<LinksAggregator.Models.Link> Link { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var sampleLinks = new List<Link>();

            for (int i = 1; i <= 1000; i++)
            {
                Link link = new Link();
                link.Id = i;
                link.AddingDate = DateTime.Now.AddMinutes(-(i * 10));
                link.Address = @"www.google.com/search?q=a" + i;
                link.Title = "Automatycznie dodany link nr " + i;

                sampleLinks.Add(link);
            }

            Link portfolio = new Link
            {
                Id = 1001,
                AddingDate = DateTime.Now,
                Title = "Mateusz Wasilewski - portfolio",
                Address = @"http://lodywas.vot.pl/mateuszwasilewski/"
            };

            sampleLinks.Add(portfolio);

            modelBuilder.Entity<Link>().HasData(sampleLinks);
        }
    }
}
