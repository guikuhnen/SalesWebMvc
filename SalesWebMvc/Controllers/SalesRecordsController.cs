using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
	public class SalesRecordsController : Controller
	{
		// GET: SalesRecords
		public async Task<IActionResult> Index()
		{
			return View();
		}

		// GET: SimpleSearch
		public async Task<IActionResult> SimpleSearch()
		{
			return View();
		}

		// GET: GroupingSearch
		public async Task<IActionResult> GroupingSearch()
		{
			return View();
		}
	}
}
