using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportTracker.Data;

namespace ReportTracker.Controllers
{
    [Authorize]
    public class FileTrackerInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FileTrackerInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FileTrackerInfo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FileTrackerInfos.Include(f => f.DeptFromNavigation).Include(f => f.DeptToNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FileTrackerInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTrackerInfo = await _context.FileTrackerInfos
                .Include(f => f.DeptFromNavigation)
                .Include(f => f.DeptToNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileTrackerInfo == null)
            {
                return NotFound();
            }

            return View(fileTrackerInfo);
        }

        // GET: FileTrackerInfo/Create
        public IActionResult Create()
        {
            ViewData["DeptFrom"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["DeptTo"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: FileTrackerInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,BarCode,DeptFrom,DeptTo")] FileTrackerInfo fileTrackerInfo)
        {
            if (ModelState.IsValid)
            {
                fileTrackerInfo.CreatedDate = DateTime.UtcNow;
                fileTrackerInfo.UpdatedDate = DateTime.UtcNow;
                fileTrackerInfo.CreatedBy = User.Identity.Name;
                fileTrackerInfo.UpdatedBy = User.Identity.Name;
                _context.Add(fileTrackerInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptFrom"] = new SelectList(_context.Departments, "Id", "Name", fileTrackerInfo.DeptFrom);
            ViewData["DeptTo"] = new SelectList(_context.Departments, "Id", "Name", fileTrackerInfo.DeptTo);
            return View(fileTrackerInfo);
        }

        // GET: FileTrackerInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTrackerInfo = await _context.FileTrackerInfos.FindAsync(id);

            if (fileTrackerInfo == null)
            {
                return NotFound();
            }
            ViewData["DeptFrom"] = new SelectList(_context.Departments, "Id", "Name", fileTrackerInfo.DeptFrom);
            ViewData["DeptTo"] = new SelectList(_context.Departments, "Id", "Name", fileTrackerInfo.DeptTo);
            return View(fileTrackerInfo);
        }

        // POST: FileTrackerInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileName,BarCode,DeptFrom,DeptTo")] FileTrackerInfo fileTrackerInfo)
        {
            if (id != fileTrackerInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentFile = await _context.FileTrackerInfos.FirstOrDefaultAsync(x => x.Id == fileTrackerInfo.Id);
                    currentFile.UpdatedBy = User.Identity.Name;
                    currentFile.UpdatedDate = DateTime.UtcNow;
                    currentFile.FileName = fileTrackerInfo.FileName;
                    currentFile.DeptTo = fileTrackerInfo.DeptTo;
                    currentFile.DeptFrom = fileTrackerInfo.DeptFrom;
                    currentFile.BarCode = fileTrackerInfo.BarCode;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileTrackerInfoExists(fileTrackerInfo.Id))
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
            ViewData["DeptFrom"] = new SelectList(_context.Departments, "Id", "Name", fileTrackerInfo.DeptFrom);
            ViewData["DeptTo"] = new SelectList(_context.Departments, "Id", "Name", fileTrackerInfo.DeptTo);
            return View(fileTrackerInfo);
        }

        // GET: FileTrackerInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTrackerInfo = await _context.FileTrackerInfos
                .Include(f => f.DeptFromNavigation)
                .Include(f => f.DeptToNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileTrackerInfo == null)
            {
                return NotFound();
            }

            return View(fileTrackerInfo);
        }

        // POST: FileTrackerInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileTrackerInfo = await _context.FileTrackerInfos.FindAsync(id);
            _context.FileTrackerInfos.Remove(fileTrackerInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileTrackerInfoExists(int id)
        {
            return _context.FileTrackerInfos.Any(e => e.Id == id);
        }
    }
}
