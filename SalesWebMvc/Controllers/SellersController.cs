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
			return View(await _sellerService.FindAllWithDepartmentAsync());
		}

		// GET: Sellers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Error), new { message = "Id not provided!" });

			try
			{
				var seller = await _sellerService.FindByIdWithDepartmentAsync(id.Value);

				return View(seller);
			}
			catch (NotFoundException ex)
			{
				return RedirectToAction(nameof(Error), new { message = ex.Message });
			}
		}

		// GET: Sellers/Create
		public async Task<IActionResult> Create()
		{
			var departments = await _departmentService.FindAllAsync();
			SellerViewModel viewModel = new SellerViewModel { Departments = departments };

			return View(viewModel);
		}

		// POST: Sellers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Email,BirthDate,BaseSalary,DepartmentId")] Seller seller)
		{
			if (ModelState.IsValid)
			{
				await _sellerService.CreateAsync(seller);

				return RedirectToAction(nameof(Index));
			}

			var departments = _departmentService.FindAllAsync();
			SellerViewModel viewModel = new SellerViewModel { Seller = seller, Departments = departments.Result };

			return View(viewModel);
		}

		// GET: Sellers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Error), new { message = "Id not provided!" });

			try
			{
				Seller seller = await _sellerService.FindByIdAsync(id.Value);
				var departments = _departmentService.FindAllAsync();
				SellerViewModel viewModel = new SellerViewModel { Seller = seller, Departments = departments.Result };

				return View(viewModel);
			}
			catch (NotFoundException ex)
			{
				return RedirectToAction(nameof(Error), new { message = ex.Message });
			}
		}

		// POST: Sellers/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,BirthDate,BaseSalary,DepartmentId")] Seller seller)
		{
			if (id != seller.Id)
				return RedirectToAction(nameof(Error), new { message = "Id mismatch!" });

			try
			{
				if (ModelState.IsValid)
				{
					await _sellerService.UpdateAsync(seller);

					return RedirectToAction(nameof(Index));
				}

				var departments = _departmentService.FindAllAsync();
				SellerViewModel viewModel = new SellerViewModel { Seller = seller, Departments = departments.Result };

				return View(viewModel);
			}
			catch (ApplicationException ex)
			{
				return RedirectToAction(nameof(Error), new { message = ex.Message });
			}
		}

		// GET: Sellers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Error), new { message = "Id not provided!" });

			try
			{
				var seller = await _sellerService.FindByIdWithDepartmentAsync(id.Value);

				return View(seller);
			}
			catch (NotFoundException ex)
			{
				return RedirectToAction(nameof(Error), new { message = ex.Message }); ;
			}
		}

		// POST: Sellers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			try
			{
				await _sellerService.DeleteAsync(id);

				return RedirectToAction(nameof(Index));
			}
			catch (IntegrityException ex)
			{
				return RedirectToAction(nameof(Error), new { message = ex.Message });
			}
		}

		public IActionResult Error(string message)
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
		}
	}
}
