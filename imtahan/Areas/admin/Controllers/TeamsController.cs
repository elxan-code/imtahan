using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using imtahan.Data;
using imtahan.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace imtahan.Areas.admin.Controllers
{
    [Area("admin")]
    public class TeamsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: admin/Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        // GET: admin/Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // GET: admin/Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Teams teams)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (teams.ImageFile!=null)
                    {
                        if (teams.ImageFile.ContentType=="image/jpeg"|| teams.ImageFile.ContentType == "image/png")
                        {
                            if (teams.ImageFile.Length<=3000000)
                            {
                                string FileName = Guid.NewGuid() + "-" + teams.ImageFile.FileName;
                                string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", FileName);
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    teams.ImageFile.CopyTo(stream);
                                }
                                teams.Image = FileName;
                                _context.Teams.Add(teams);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));

                            }
                            else
                            {
                                ModelState.AddModelError("", "you can choose only 3 mb image file");
                                return View(teams);
                            }
                            

                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only image file");
                            return View(teams);

                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", " choose image file");
                        return View(teams);

                    }

                }
            }
            return View(teams);
        }

        // GET: admin/Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }
            return View(teams);
        }

        // POST: admin/Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Teams teams)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (teams.ImageFile != null)
                    {
                        if (teams.ImageFile.ContentType == "image/jpeg" || teams.ImageFile.ContentType == "image/png")
                        {
                            if (teams.ImageFile.Length <= 3000000)
                            {
                                string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", teams.Image);
                                if (System.IO.File.Exists(olddata)) 
                                {
                                    System.IO.File.Delete(olddata);
                                }
                                string FileName = Guid.NewGuid() + "-" + teams.ImageFile.FileName;
                                string FilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", FileName);
                                using (var stream = new FileStream(FilePath, FileMode.Create))
                                {
                                    teams.ImageFile.CopyTo(stream);
                                }
                                teams.Image = FileName;
                                _context.Teams.Update(teams);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));

                            }
                            else
                            {
                                ModelState.AddModelError("", "you can choose only 3 mb image file");
                                return View(teams);
                            }


                        }
                        else
                        {
                            ModelState.AddModelError("", "you can choose only image file");
                            return View(teams);

                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", " choose image file");
                        return View(teams);

                    }

                }
            }
            return View(teams);
        }
        // GET: admin/Portfolios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // POST: admin/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string olddata = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", teams.Image);
            if (System.IO.File.Exists(olddata))
            {
                System.IO.File.Delete(olddata);
            }
            var teams = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(teams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
