using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinksAggregator.Models;

namespace LinksAggregator.Controllers
{
    public class LinksController : Controller
    {
        private readonly LinksAggregatorContext _context;

        public LinksController(LinksAggregatorContext context)
        {
            _context = context;
        }
        
        // GET: Links
        public async Task<IActionResult> Index(int? page)
        {
            var viewModel = new LinkViewModel();

            if (page > 0)
            {
                var links = await _context.Link.
                     Where(l => l.AddingDate.CompareTo(DateTime.Now.AddDays(-5)) > 0).
                     OrderByDescending(l => l.AddingDate).
                     ToListAsync();

                viewModel.Pagination = new Pagination(true, (int)page);

                int skipAmount = viewModel.Pagination.Size * ((int)page - 1);
                int takeAmount = viewModel.Pagination.Size;
                int pagesAmount = links.Count() / viewModel.Pagination.Size;

                viewModel.Pagination.PagesAmount = pagesAmount;
                viewModel.Links = links.Skip(skipAmount).Take(takeAmount);
            }

            else
            {
                viewModel.Pagination = new Pagination();
                viewModel.Links = await _context.Link.
                    Where(l => l.AddingDate.CompareTo(DateTime.Now.AddDays(-5)) > 0).
                    OrderByDescending(l => l.AddingDate).
                    ToListAsync();
            }

            return View(viewModel);
        }

        // GET: Links/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Link
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // GET: Links/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Links/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Address")] Link link)
        {
            link.AddingDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(link);
                    await _context.SaveChangesAsync();
                    return View("AddingSuccess");
                }
                catch
                {
                    return View("AddingFailure");
                }
            }
            return View(link);
        }

        private object AddingFailure()
        {
            return View();
        }

        private object AddingSuccess()
        {
            return View();
        }

        // GET: Links/Create
        public IActionResult EnablePagination()
        {
            return RedirectToAction("Index", new { page = 1 });
        }

        // GET: Links/Create
        public IActionResult DisablePagination()
        {
            return RedirectToAction("Index");
        }

        //// GET: Links/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var link = await _context.Link.FindAsync(id);
        //    if (link == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(link);
        //}

        //// POST: Links/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Address,AddingDate")] Link link)
        //{
        //    if (id != link.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(link);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LinkExists(link.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(link);
        //}

        //// GET: Links/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var link = await _context.Link
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (link == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(link);
        //}

        //// POST: Links/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    var link = await _context.Link.FindAsync(id);
        //    _context.Link.Remove(link);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LinkExists(long id)
        //{
        //    return _context.Link.Any(e => e.Id == id);
        //}
    }
}
