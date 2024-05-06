using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTdatabasefirst.Models;

namespace BTdatabasefirst.Controllers
{
    public class LuongsController : Controller
    {
        private readonly CshapCuoikiContext _context;

        public LuongsController(CshapCuoikiContext context)
        {
            _context = context;
        }

        

        
        // GET: Luongs
        public async Task<IActionResult> Index()
        {
            var cshapCuoikiContext = _context.Luongs.Include(l => l.IdnhanvienNavigation);
            return View(await cshapCuoikiContext.ToListAsync());
        }

        // GET: Luongs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs
                .Include(l => l.IdnhanvienNavigation)
                .FirstOrDefaultAsync(m => m.Idnhanvien == id);
            if (luong == null)
            {
                return NotFound();
            }

            return View(luong);
        }

        // GET: Luongs/Create
        public IActionResult Create()
        {
            ViewData["Idnhanvien"] = new SelectList(_context.Nhanviens, "Idnv", "Idnv");
            return View();
        }

        // POST: Luongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idnhanvien,Thoigian,Luong1")] Luong luong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(luong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idnhanvien"] = new SelectList(_context.Nhanviens, "Idnv", "Idnv", luong.Idnhanvien);
            return View(luong);
        }

        // GET: Luongs/Edit/5
        public async Task<IActionResult> Edit(string id, string tg)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs.FindAsync(id,tg);
            if (luong == null)
            {
                return NotFound();
            }
            ViewData["Idnhanvien"] = new SelectList(_context.Nhanviens, "Idnv", "Idnv", luong.Idnhanvien);
            return View(luong);
        }

        // POST: Luongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,string tg, [Bind("Idnhanvien,Thoigian,Luong1")] Luong luong)
        {
            if (id != luong.Idnhanvien && tg!= luong.Thoigian)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(luong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LuongExists(luong.Idnhanvien))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idnhanvien"] = new SelectList(_context.Nhanviens, "Idnv", "Idnv", luong.Idnhanvien);
            return View(luong);
        }

        // GET: Luongs/Delete/5
        public async Task<IActionResult> Delete(string id ,string tg)
        {
            if (id == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs
                .Include(l => l.IdnhanvienNavigation)
                .FirstOrDefaultAsync(m => m.Idnhanvien == id && m.Thoigian==tg);
            if (luong == null)
            {
                return NotFound();
            }

            return View(luong);
        }

        // POST: Luongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, string tg)
        {
            var luong = await _context.Luongs.FindAsync(id,tg);
            if (luong != null)
            {
                _context.Luongs.Remove(luong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LuongExists(string id)
        {
            return _context.Luongs.Any(e => e.Idnhanvien == id);
        }
    }
}
