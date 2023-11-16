using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
	public class SalesRecordService
	{
		private readonly SalesWebMvcContext _context;

		public SalesRecordService(SalesWebMvcContext context)
		{
			_context = context;
		}

		public async Task<ICollection<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
		{
			var result = QueryByDate(minDate, maxDate);

			return await result
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderByDescending(x => x.Date)
				.ToListAsync();
		}

		public async Task<ICollection<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
		{
			var query = QueryByDate(minDate, maxDate);

			var result = await query
				.Include(x => x.Seller)
				.Include(x => x.Seller.Department)
				.OrderByDescending(x => x.Date)
				.ToListAsync();

			return result.GroupBy(x => x.Seller.Department).ToList();
		}

		private IQueryable<SalesRecord> QueryByDate (DateTime? minDate, DateTime? maxDate)
		{
			var result = from obj in _context.SalesRecord select obj;

			if (minDate.HasValue)
				result = result.Where(x => x.Date >= minDate);

			if (maxDate.HasValue)
				result = result.Where(x => x.Date <= maxDate); 
			
			return result;
		}
	}
}
