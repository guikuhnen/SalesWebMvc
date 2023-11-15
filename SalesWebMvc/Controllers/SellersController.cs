﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
	public class SellersController : Controller
	{
		private readonly SellerService _sellerService;
		private readonly DepartmentService _departmentService;

		public SellersController(SellerService sellerService, DepartmentService departmentService)
		{
			_sellerService = sellerService;
			_departmentService = departmentService;
		}

		// GET: Sellers
		public async Task<IActionResult> Index()
		{
			return View(await _sellerService.FindAllWithDepartment());
		}

		// GET: Sellers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return BadRequest();

			try
			{
				var seller = await _sellerService.FindByIdWithDepartment(id.Value);

				return View(seller);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}

		// GET: Sellers/Create
		public IActionResult Create()
		{
			var departments = _departmentService.FindAll();
			SellerViewModel viewModel = new SellerViewModel { Departments = departments.Result };

			return View(viewModel);
		}

		// POST: Sellers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Email,BirthDate,BaseSalary,DepartmentId")] Seller seller)
		{
			if (ModelState.IsValid)
			{
				await _sellerService.Add(seller);

				return RedirectToAction(nameof(Index));
			}

			return View(seller);
		}

		// GET: Sellers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return BadRequest();

			try
			{
				Seller seller = await _sellerService.FindById(id.Value);
				var departments = _departmentService.FindAll();
				SellerViewModel viewModel = new SellerViewModel { Seller = seller, Departments = departments.Result };

				return View(viewModel);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}

		// POST: Sellers/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,BirthDate,BaseSalary,DepartmentId")] Seller seller)
		{
			if (id != seller.Id)
				return BadRequest();

			try
			{
				if (ModelState.IsValid)
				{
					await _sellerService.Update(seller);

					return RedirectToAction(nameof(Index));
				}

				return View(seller);
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

		// GET: Sellers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return BadRequest();

			try
			{
				var seller = await _sellerService.FindByIdWithDepartment(id.Value);

				return View(seller);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}

		// POST: Sellers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _sellerService.Delete(id);

			return RedirectToAction(nameof(Index));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
