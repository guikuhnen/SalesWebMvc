using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
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
			return await _context.Department.OrderBy(x => x.Name).ToListAsync();
		}


		public async Task<Department> FindById(int id)
		{
			var department = await _context.Department
				.FirstOrDefaultAsync(m => m.Id == id);

			return department ?? throw new NotFoundException("Unable to find Department with provided ID!");
		}

		public async Task Add(Department department)
		{
			_context.Add(department);

			await _context.SaveChangesAsync();
		}

		public async Task Update(Department department)
		{
			if (!DepartmentExists(department.Id))
				throw new NotFoundException("Department not found!");

			try
			{
				_context.Update(department);

				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw new DbConcurrencyException(ex.Message);
			}
		}

		private bool DepartmentExists(int id)
		{
			return _context.Department.Any(x => x.Id == id);
		}

		public async Task Delete(int id)
		{
			var department = await FindById(id);

			_context.Department.Remove(department);

			await _context.SaveChangesAsync();
		}
	}
}
