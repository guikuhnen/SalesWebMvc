using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
	public class SalesRecordsController : Controller
	{
		private readonly SalesRecordService _salesRecordService;

		public SalesRecordsController(SalesRecordService salesRecordService)
		{
			_salesRecordService = salesRecordService;
		}

		// GET: SalesRecords
		public IActionResult Index()
		{
			return View();
		}

		// GET: SalesRecords/SimpleSearch/SalesRecordViewModel
		public async Task<IActionResult> SimpleSearch([Bind("MinDate,MaxDate")] SalesRecordViewModel viewModel)
		{
			if (!viewModel.MinDate.HasValue)
				viewModel.MinDate = new DateTime(DateTime.Now.Year, 1, 1);

			if (!viewModel.MaxDate.HasValue)
				viewModel.MaxDate = DateTime.Now;

			ViewData["MinDate"] = viewModel.MinDate.Value.ToString("yyyy-MM-dd");
			ViewData["MaxDate"] = viewModel.MaxDate.Value.ToString("yyyy-MM-dd");

			return View(await _salesRecordService.FindByDateAsync(viewModel.MinDate, viewModel.MaxDate));
		}

		// GET: SalesRecords/GroupingSearch/SalesRecordViewModel
		public async Task<IActionResult> GroupingSearch([Bind("MinDate,MaxDate")] SalesRecordViewModel viewModel)
		{
			if (!viewModel.MinDate.HasValue)
				viewModel.MinDate = new DateTime(DateTime.Now.Year, 1, 1);

			if (!viewModel.MaxDate.HasValue)
				viewModel.MaxDate = DateTime.Now;

			ViewData["MinDate"] = viewModel.MinDate.Value.ToString("yyyy-MM-dd");
			ViewData["MaxDate"] = viewModel.MaxDate.Value.ToString("yyyy-MM-dd");

			var result = await _salesRecordService.FindByDateGroupingAsync(viewModel.MinDate, viewModel.MaxDate);

			return View(result);
		}
	}
}
