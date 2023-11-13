using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
	public class DepartmentService
	{
		private readonly SalesWebMvcContext _context;

		public DepartmentService(SalesWebMvcContext context)
		{
			_context = context;
		}

		public async Task<ICollection<Department>> FindAll()
		{
			return await _context.Department.ToListAsync();
		}


		public async Task<Department> FindById(int id)
		{
			var department = await _context.Department
				.FirstOrDefaultAsync(m => m.Id == id);

			if (department == null)
				throw new InvalidOperationException("Unable to find data for the given ID!");

			return department;
		}

		public async Task Add(Department department)
		{
			_context.Add(department);

			await _context.SaveChangesAsync();
		}

		public async Task Edit(Department department)
		{
			try
			{
				_context.Update(department);

				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DepartmentExists(department.Id))
				{
					throw new InvalidOperationException("Unable to find data for the given ID!");
				}
				else
				{
					throw;
				}
			}
		}
		private bool DepartmentExists(int id)
		{
			return _context.Department.Any(e => e.Id == id);
		}

		public async Task Delete(int id)
		{
			var department = await FindById(id);

			_context.Department.Remove(department);

			await _context.SaveChangesAsync();
		}
	}
}
