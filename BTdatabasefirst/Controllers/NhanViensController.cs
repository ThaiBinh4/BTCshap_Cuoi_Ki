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

    public class NhanViensController : Controller
    {
        private readonly BtefnhanVienPhongBanCongtyContext _context;

        public NhanViensController(BtefnhanVienPhongBanCongtyContext context)
        {
            _context = context;
        }

        // GET: NhanViens
        public async Task<IActionResult> Index1()
        {
            //var btefnhanVienPhongBanCongtyContext = _context.NhanViens.Include(n => n.IdphongbanNavigation);
            //return View(await btefnhanVienPhongBanCongtyContext.ToListAsync());
             var nhanViens = from n in _context.NhanViens
                            select n;
            string idpb = (
                            from p in _context.Phongbans
                           where p.Tenphongban.Contains("marketing")
                           select p.Idphongban).FirstOrDefault();

            nhanViens = nhanViens.Where(n => n.Idphongban.Contains(idpb) && n.Tuoi>=30 && n.Tuoi<=40);
            

            var btefnhanVienPhongBanCongtyContext = nhanViens.Include(n => n.IdphongbanNavigation);
            return View(await btefnhanVienPhongBanCongtyContext.ToListAsync());

        }

        public async Task<IActionResult> Index()
        {
            var btefnhanVienPhongBanCongtyContext = _context.NhanViens.Include(n => n.IdphongbanNavigation);
            return View(await btefnhanVienPhongBanCongtyContext.ToListAsync());
            

        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.IdphongbanNavigation)
                .FirstOrDefaultAsync(m => m.Idnhanvien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public IActionResult Create()
        {
            ViewData["Idphongban"] = new SelectList(_context.Phongbans, "Idphongban", "Idphongban");
            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idnhanvien,Tennhanvien,Tuoi,Idphongban")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idphongban"] = new SelectList(_context.Phongbans, "Idphongban", "Idphongban", nhanVien.Idphongban);
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            ViewData["Idphongban"] = new SelectList(_context.Phongbans, "Idphongban", "Idphongban", nhanVien.Idphongban);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Idnhanvien,Tennhanvien,Tuoi,Idphongban")] NhanVien nhanVien)
        {
            if (id != nhanVien.Idnhanvien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.Idnhanvien))
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
            ViewData["Idphongban"] = new SelectList(_context.Phongbans, "Idphongban", "Idphongban", nhanVien.Idphongban);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.IdphongbanNavigation)
                .FirstOrDefaultAsync(m => m.Idnhanvien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(string id)
        {
            return _context.NhanViens.Any(e => e.Idnhanvien == id);
        }
    }
}

