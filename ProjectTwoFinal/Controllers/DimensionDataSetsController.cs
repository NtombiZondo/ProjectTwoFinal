using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTwoFinal.Data;
using ProjectTwoFinal.Models;

namespace ProjectTwoFinal.Controllers
{
    public class DimensionDataSetsController : Controller
    {
        private readonly ProjectTwoContext _context;

        public DimensionDataSetsController(ProjectTwoContext context)
        {
            _context = context;
        }

        // GET: DimensionDataSets
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.DimensionDataSet.ToListAsync());
        }

        // GET: DimensionDataSets/Details/5
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dimensionDataSet = await _context.DimensionDataSet
                .FirstOrDefaultAsync(m => m.EmployeeNumber == id);
            if (dimensionDataSet == null)
            {
                return NotFound();
            }

            return View(dimensionDataSet);
        }

        // GET: DimensionDataSets/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DimensionDataSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("Age,Attrition,BusinessTravel,DailyRate,Department,DistanceFromHome,Education,EducationField,EmployeeCount,EmployeeNumber,EnvironmentSatisfaction,Gender,HourlyRate,JobInvolvement,JobLevel,JobRole,JobSatisfaction,MaritalStatus,MonthlyIncome,MonthlyRate,NumCompaniesWorked,Over18,OverTime,PercentSalaryHike,PerformanceRating,RelationshipSatisfaction,StandardHours,StockOptionLevel,TotalWorkingYears,TrainingTimesLastYear,WorkLifeBalance,YearsAtCompany,YearsInCurrentRole,YearsSinceLastPromotion,YearsWithCurrManager")] DimensionDataSet dimensionDataSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dimensionDataSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dimensionDataSet);
        }

        // GET: DimensionDataSets/Edit/5
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dimensionDataSet = await _context.DimensionDataSet.FindAsync(id);
            if (dimensionDataSet == null)
            {
                return NotFound();
            }
            return View(dimensionDataSet);
        }

        // POST: DimensionDataSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> Edit(string id, [Bind("Age,Attrition,BusinessTravel,DailyRate,Department,DistanceFromHome,Education,EducationField,EmployeeCount,EmployeeNumber,EnvironmentSatisfaction,Gender,HourlyRate,JobInvolvement,JobLevel,JobRole,JobSatisfaction,MaritalStatus,MonthlyIncome,MonthlyRate,NumCompaniesWorked,Over18,OverTime,PercentSalaryHike,PerformanceRating,RelationshipSatisfaction,StandardHours,StockOptionLevel,TotalWorkingYears,TrainingTimesLastYear,WorkLifeBalance,YearsAtCompany,YearsInCurrentRole,YearsSinceLastPromotion,YearsWithCurrManager")] DimensionDataSet dimensionDataSet)
        {
            if (id != dimensionDataSet.EmployeeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dimensionDataSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DimensionDataSetExists(dimensionDataSet.EmployeeNumber))
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
            return View(dimensionDataSet);
        }

        // GET: DimensionDataSets/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dimensionDataSet = await _context.DimensionDataSet
                .FirstOrDefaultAsync(m => m.EmployeeNumber == id);
            if (dimensionDataSet == null)
            {
                return NotFound();
            }

            return View(dimensionDataSet);
        }

        // POST: DimensionDataSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dimensionDataSet = await _context.DimensionDataSet.FindAsync(id);
            _context.DimensionDataSet.Remove(dimensionDataSet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DimensionDataSetExists(string id)
        {
            return _context.DimensionDataSet.Any(e => e.EmployeeNumber == id);
        }
    }
}
