using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext _context;

        public EmployeesController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employeeDbContext = _context.Employees.Include(e => e.City).Include(e => e.District).Include(e => e.State);
            return View(await employeeDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.City)
                .Include(e => e.District)
                .Include(e => e.State)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["CityName"] = new SelectList(_context.Cities, "CityName", "CityName");
            ViewData["DistrictName"] = new SelectList(_context.Districts, "DistrictName", "DistrictName");
            ViewData["StateName"] = new SelectList(_context.States, "StateId", "StateName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,EmployeeDesignation,EmployeeSalary,StateId,DistrictId,CityId,EmployeeAddress,EmployeeMobileNo")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", employee.CityId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictId", employee.DistrictId);
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateId", employee.StateId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", employee.CityId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictId", employee.DistrictId);
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateId", employee.StateId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,EmployeeDesignation,EmployeeSalary,StateId,DistrictId,CityId,EmployeeAddress,EmployeeMobileNo")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", employee.CityId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictId", employee.DistrictId);
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateId", employee.StateId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.City)
                .Include(e => e.District)
                .Include(e => e.State)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'EmployeeDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }


        [HttpGet]
        public JsonResult GetDistrictsByState(int stateId)
        {
            var districts = _context.Districts
                .Where(d => d.StateId == stateId)
                .Select(d => new { value = d.DistrictId, text = d.DistrictName })
                .ToList();
            return Json(districts);
        }

        [HttpGet]
        public JsonResult GetCitiesByDistrict(int districtId)
        {
            var cities = _context.Cities
                .Where(c => c.DistrictId == districtId)
                .Select(c => new { value = c.CityId, text = c.CityName })
                .ToList();
            return Json(cities);
        }
    }
}

