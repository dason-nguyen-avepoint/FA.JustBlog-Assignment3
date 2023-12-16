using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NWEB.Final.Core.Data;
using NWEB.Final.Core.Models;

namespace NWEB.Final.Core.Controllers
{
    public class TiemChungsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiemChungsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TiemChungs
        public async Task<IActionResult> Index(string searchString, string LoaiVX)
        {
            ViewData["LoaiVX"] = new SelectList(_context.LoaiVacXin, "MaLoaiVX", "TenLoaiVX");
            var applicationDbContext = from x in _context.TiemChung
                                        join y in _context.Congdan on x.MaCD equals y.MaCD
                                        join z in _context.LieuVacXin on x.MaLieuVX equals z.MaLieuVX
                                        join q in _context.LoaiVacXin on z.MaLoaiVX equals q.MaLoaiVX
                                        select (new
                                        {
                                            y.MaCD,
                                            y.HoTen,
                                            z.MaLieuVX,
                                            q.TenLoaiVX,
                                            x.NgayTiemMui1,
                                            x.NgayDKTienMui2,
                                            x.NgayTiemMui2,
                                            x.TrangThai,
                                            x.GhiChu,
                                            x.MaTC,
                                            q.MaLoaiVX
                                        });
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(LoaiVX))
            {
                applicationDbContext = applicationDbContext.Where(s => s.MaCD.Contains(searchString));
            }
            if (String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(LoaiVX))
            {
                applicationDbContext = applicationDbContext.Where(s => s.MaLoaiVX.Contains(LoaiVX));
            }
            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(LoaiVX))
            {
                applicationDbContext = applicationDbContext.Where(s => s.MaCD.Contains(searchString) && s.MaLoaiVX.Contains(LoaiVX));
            }
            return View(applicationDbContext);
        }

        // GET: TiemChungs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiemChung = await _context.TiemChung
                .Include(t => t.Congdans)
                .Include(t => t.LieuVacXins)
                .FirstOrDefaultAsync(m => m.MaTC == id);
            if (tiemChung == null)
            {
                return NotFound();
            }

            return View(tiemChung);
        }

        // GET: TiemChungs/Create
        public IActionResult Create()
        {
            ViewData["MaCD"] = new SelectList(_context.Congdan, "MaCD", "MaCD");
            ViewData["MaLieuVX"] = new SelectList(_context.LieuVacXin, "MaLieuVX", "MaLieuVX");
            return View();
        }

        // POST: TiemChungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTC,MaCD,MaLieuVX,NgayTiemMui1,NgayDKTienMui2,NgayTiemMui2,TrangThai,GhiChu")] TiemChung tiemChung)
        {
            if(_context.Congdan.FirstOrDefault(x => x.MaCD == tiemChung.MaCD) == null)
            {
                ModelState.AddModelError("MaCD", "Ma CD khong ton tai");
            }
            if (_context.LieuVacXin.FirstOrDefault(x => x.MaLieuVX == tiemChung.MaLieuVX) == null)
            {
                ModelState.AddModelError("MaLieuVX", "Ma lieu Vacxin khong ton tai. Hay nhap lai");
            }
            if (ModelState.IsValid)
            {
                var lieuVacXin = _context.LieuVacXin.First(x => x.MaLieuVX == tiemChung.MaLieuVX);
                var ngayNhac = _context.LoaiVacXin.First(x => x.MaLoaiVX == lieuVacXin.MaLoaiVX).SoNgayTienNhac;
                if(tiemChung.NgayDKTienMui2 < tiemChung.NgayTiemMui1.AddDays(ngayNhac))
                {
                tiemChung.NgayDKTienMui2 = tiemChung.NgayTiemMui1.AddDays(ngayNhac);
                }
                _context.Add(tiemChung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaCD"] = new SelectList(_context.Congdan, "MaCD", "MaCD", tiemChung.MaCD);
            ViewData["MaLieuVX"] = new SelectList(_context.LieuVacXin, "MaLieuVX", "MaLieuVX", tiemChung.MaLieuVX);
            return View(tiemChung);
        }

        // GET: TiemChungs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiemChung = await _context.TiemChung.FindAsync(id);
            if (tiemChung == null)
            {
                return NotFound();
            }
            ViewData["MaCD"] = new SelectList(_context.Congdan, "MaCD", "MaCD", tiemChung.MaCD);
            ViewData["MaLieuVX"] = new SelectList(_context.LieuVacXin, "MaLieuVX", "MaLieuVX", tiemChung.MaLieuVX);
            return View(tiemChung);
        }

        // POST: TiemChungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaTC,MaCD,MaLieuVX,NgayTiemMui1,NgayDKTienMui2,NgayTiemMui2,TrangThai,GhiChu")] TiemChung tiemChung)
        {
            if (id != tiemChung.MaTC)
            {
                return NotFound();
            }
            if (_context.LieuVacXin.FirstOrDefault(x => x.MaLieuVX == tiemChung.MaLieuVX) == null)
            {
                ModelState.AddModelError("MaLieuVX", "Ma lieu Vacxin khong ton tai. Hay nhap lai");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(tiemChung.TrangThai == "Da hoan tat")
                    {
                        tiemChung.NgayTiemMui2 = tiemChung.NgayDKTienMui2;
                    }
                    _context.Update(tiemChung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiemChungExists(tiemChung.MaTC))
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
            ViewData["MaCD"] = new SelectList(_context.Congdan, "MaCD", "MaCD", tiemChung.MaCD);
            ViewData["MaLieuVX"] = new SelectList(_context.LieuVacXin, "MaLieuVX", "MaLieuVX", tiemChung.MaLieuVX);
            return View(tiemChung);
        }

        // GET: TiemChungs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiemChung = await _context.TiemChung
                .Include(t => t.Congdans)
                .Include(t => t.LieuVacXins)
                .FirstOrDefaultAsync(m => m.MaTC == id);
            if (tiemChung == null)
            {
                return NotFound();
            }

            return View(tiemChung);
        }

        // POST: TiemChungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tiemChung = await _context.TiemChung.FindAsync(id);
            if (tiemChung != null)
            {
                _context.TiemChung.Remove(tiemChung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiemChungExists(string id)
        {
            return _context.TiemChung.Any(e => e.MaTC == id);
        }
    }
}
