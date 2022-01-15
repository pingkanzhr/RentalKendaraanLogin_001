﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalKendaraan.Models;

namespace RentalKendaraan.Controllers
{
    public class KondisiKendaraansController : Controller
    {
        private readonly RentKendaraanContext _context;

        public KondisiKendaraansController(RentKendaraanContext context)
        {
            _context = context;
        }

        // GET: KondisiKendaraans
        public async Task<IActionResult> Index(string ktsd, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            //buat list menyimpan ketersediaan
            var ktsdList = new List<string>();
            //Query mengambil data
            var ktsdQuery = from d in _context.KondisiKendaraans orderby d.NamaKondisi select d.NamaKondisi;

            ktsdList.AddRange(ktsdQuery.Distinct());

            //untuk menampilkan di view
            ViewBag.ktsd = new SelectList(ktsdList);

            //panggil db context
            var menu = from m in _context.KondisiKendaraans select m;

            //untuk memilih dropdownlist ketersediaan
            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.NamaKondisi == ktsd);
            }

            //untuk search data
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.NamaKondisi.Contains(searchString));
            }

            //untuk sorting
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            switch (sortOrder)
            {
                case "name_desc":
                    menu = menu.OrderByDescending(s => s.NamaKondisi);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NamaKondisi);
                    break;
            }
            //membuat pagedlist
            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            int pageSize = 5;

            return View(await PaginatedList<KondisiKendaraan>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

            // GET: KondisiKendaraans/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiKendaraan = await _context.KondisiKendaraans
                .FirstOrDefaultAsync(m => m.IdKondisi == id);
            if (kondisiKendaraan == null)
            {
                return NotFound();
            }

            return View(kondisiKendaraan);
        }

        // GET: KondisiKendaraans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KondisiKendaraans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKondisi,NamaKondisi")] KondisiKendaraan kondisiKendaraan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kondisiKendaraan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kondisiKendaraan);
        }

        // GET: KondisiKendaraans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiKendaraan = await _context.KondisiKendaraans.FindAsync(id);
            if (kondisiKendaraan == null)
            {
                return NotFound();
            }
            return View(kondisiKendaraan);
        }

        // POST: KondisiKendaraans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKondisi,NamaKondisi")] KondisiKendaraan kondisiKendaraan)
        {
            if (id != kondisiKendaraan.IdKondisi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kondisiKendaraan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KondisiKendaraanExists(kondisiKendaraan.IdKondisi))
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
            return View(kondisiKendaraan);
        }

        // GET: KondisiKendaraans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiKendaraan = await _context.KondisiKendaraans
                .FirstOrDefaultAsync(m => m.IdKondisi == id);
            if (kondisiKendaraan == null)
            {
                return NotFound();
            }

            return View(kondisiKendaraan);
        }

        // POST: KondisiKendaraans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kondisiKendaraan = await _context.KondisiKendaraans.FindAsync(id);
            _context.KondisiKendaraans.Remove(kondisiKendaraan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KondisiKendaraanExists(int id)
        {
            return _context.KondisiKendaraans.Any(e => e.IdKondisi == id);
        }
    }
}
