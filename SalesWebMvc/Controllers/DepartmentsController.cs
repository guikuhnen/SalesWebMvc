using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
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
			return View(await _departmentService.FindAll());
		}

		// GET: Departments/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return BadRequest();

			try
			{
				var department = await _departmentService.FindById(id.Value);

				return View(department);
			}
			catch (NotFoundException)
			{
				return NotFound();
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
				await _departmentService.Add(department);

				return RedirectToAction(nameof(Index));
			}

			return View(department);
		}

		// GET: Departments/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return BadRequest();

			try
			{
				var department = await _departmentService.FindById(id.Value);

				return View(department);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}

		// POST: Departments/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
		{
			if (id != department.Id)
				return BadRequest();

			try
			{
				if (ModelState.IsValid)
				{
					await _departmentService.Update(department);

					return RedirectToAction(nameof(Index));
				}

				return View(department);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch (DbConcurrencyException)
			{
				return BadRequest();
			}
		}

		// GET: Departments/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return BadRequest();

			try
			{
				var department = await _departmentService.FindById(id.Value);

				return View(department);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}

		// POST: Departments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _departmentService.Delete(id);

			return RedirectToAction(nameof(Index));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
