using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
	public class DepartmentsController : Controller
	{
		private readonly DepartmentService _departmentService;

		public DepartmentsController(DepartmentService departmentService)
		{
			_departmentService = departmentService;
		}

		// GET: Departments
		public async Task<IActionResult> Index()
		{
			return View(await _departmentService.FindAllAsync());
		}

		// GET: Departments/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });

            try
			{
				var department = await _departmentService.FindByIdAsync(id.Value);

				return View(department);
			}
			catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

		// GET: Departments/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Departments/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
		{
			if (ModelState.IsValid)
			{
				await _departmentService.CreateAsync(department);

				return RedirectToAction(nameof(Index));
			}

			return View(department);
		}

		// GET: Departments/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });

            try
			{
				var department = await _departmentService.FindByIdAsync(id.Value);

				return View(department);
			}
			catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

		// POST: Departments/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
		{
			if (id != department.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch!" });

            try
			{
				if (ModelState.IsValid)
				{
					await _departmentService.UpdateAsync(department);

					return RedirectToAction(nameof(Index));
				}

				return View(department);
			}
			catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

		// GET: Departments/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });

            try
			{
				var department = await _departmentService.FindByIdAsync(id.Value);

				return View(department);
			}
			catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message }); ;
            }
        }

		// POST: Departments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _departmentService.DeleteAsync(id);

			return RedirectToAction(nameof(Index));
		}

        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }
    }
}
