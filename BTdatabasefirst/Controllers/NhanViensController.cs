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
    public class NhanviensController : Controller
    {
        private readonly CshapCuoikiContext _context;

        public NhanviensController(CshapCuoikiContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> timten([FromQuery] string ten)
        {

            

            var result = from n in _context.Nhanviens
                         where (n.Tennhanvien.Contains(ten))
                         select n;
            var x = result.Include(n => n.IdpbNavigation).Distinct();
            return View(await x.ToListAsync());


        }
        public async Task<IActionResult> nv10tr()
        {

            var luong = from p in _context.Luongs
                        where p.Luong1 > 10
                        select p;

            var result = from n in _context.Nhanviens
                         join i in luong on n.Idnv equals i.Idnhanvien
                         select n;
            var x = result.Include(n => n.IdpbNavigation).Distinct();
            return View(await x.ToListAsync());
        }

        public async Task<IActionResult> truongphong()
        {
            var nhanViens = from n in _context.Nhanviens
                            where n.Idcv.Equals("cv01")
                            select n;


            var x = nhanViens.Include(n => n.IdpbNavigation);
            return View(await x.ToListAsync());

        }

        // GET: Nhanviens
        public async Task<IActionResult> Index()
        {
            var cshapCuoikiContext = _context.Nhanviens.Include(n => n.IdcvNavigation).Include(n => n.IdpbNavigation);
            return View(await cshapCuoikiContext.ToListAsync());
        }

        // GET: Nhanviens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .Include(n => n.IdcvNavigation)
                .Include(n => n.IdpbNavigation)
                .FirstOrDefaultAsync(m => m.Idnv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // GET: Nhanviens/Create
        public IActionResult Create()
        {
            ViewData["Idcv"] = new SelectList(_context.Chucvus, "Idcv", "Idcv");
            ViewData["Idpb"] = new SelectList(_context.Phongbans, "Idpb", "Idpb");
            return View();
        }

        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idnv,Tennhanvien,Idpb,Idcv,Ngaysinh,Sdt")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem số điện thoại có 3 số đầu là "098" không
                if (nhanvien.Sdt.StartsWith("098") || nhanvien.Sdt.StartsWith("090")|| nhanvien.Sdt.StartsWith("091")|| nhanvien.Sdt.StartsWith("031")|| nhanvien.Sdt.StartsWith("035")|| nhanvien.Sdt.StartsWith("038"))
                {
                    _context.Add(nhanvien);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Sdt", "Số điện thoại không hợp lệ. Số điện thoại phải bắt đầu bằng  '090', '098', '091', '031', '035', '038'.");
                }
            }
            ViewData["Idcv"] = new SelectList(_context.Chucvus, "Idcv", "Idcv", nhanvien.Idcv);
            ViewData["Idpb"] = new SelectList(_context.Phongbans, "Idpb", "Idpb", nhanvien.Idpb);
            return View(nhanvien);
        }

        // GET: Nhanviens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            ViewData["Idcv"] = new SelectList(_context.Chucvus, "Idcv", "Idcv", nhanvien.Idcv);
            ViewData["Idpb"] = new SelectList(_context.Phongbans, "Idpb", "Idpb", nhanvien.Idpb);
            return View(nhanvien);
        }

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Idnv,Tennhanvien,Idpb,Idcv,Ngaysinh,Sdt")] Nhanvien nhanvien)
        {
            if (id != nhanvien.Idnv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.Idnv))
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
            ViewData["Idcv"] = new SelectList(_context.Chucvus, "Idcv", "Idcv", nhanvien.Idcv);
            ViewData["Idpb"] = new SelectList(_context.Phongbans, "Idpb", "Idpb", nhanvien.Idpb);
            return View(nhanvien);
        }

        // GET: Nhanviens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .Include(n => n.IdcvNavigation)
                .Include(n => n.IdpbNavigation)
                .FirstOrDefaultAsync(m => m.Idnv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien != null)
            {
                _context.Nhanviens.Remove(nhanvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(string id)
        {
            return _context.Nhanviens.Any(e => e.Idnv == id);
        }
    }
}
