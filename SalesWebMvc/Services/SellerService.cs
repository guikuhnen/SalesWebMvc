using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
	public class SellerService
	{
		private readonly SalesWebMvcContext _context;

		public SellerService(SalesWebMvcContext context)
		{
			_context = context;
		}

		public async Task<ICollection<Seller>> FindAllAsync()
		{
			return await _context.Seller.ToListAsync();
		}

		public async Task<ICollection<Seller>> FindAllWithDepartmentAsync()
		{
			return await _context.Seller
				.Include(x => x.Department)
				.ToListAsync();
		}

		public async Task<Seller> FindByIdAsync(int id)
		{
			var seller = await _context.Seller
				.FirstOrDefaultAsync(x => x.Id == id);

			return seller ?? throw new NotFoundException("Unable to find Seller with provided ID!");
		}

		public async Task<Seller> FindByIdWithDepartmentAsync(int id)
		{
			var seller = await _context.Seller
				.Include(x => x.Department)
				.FirstOrDefaultAsync(y => y.Id == id);

			return seller ?? throw new NotFoundException("Unable to find Seller with provided ID!");
		}

		public async Task CreateAsync(Seller seller)
		{
			_context.Add(seller);

			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Seller seller)
		{
			bool hasAny = await SellerExistsAsync(seller.Id);
			if (!hasAny)
				throw new NotFoundException("Seller not found!");

			try
			{
				_context.Update(seller);

				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw new DbConcurrencyException(ex.Message);
			}
		}

		private async Task<bool> SellerExistsAsync(int id)
		{
			return await _context.Seller.AnyAsync(x => x.Id == id);
		}

		public async Task DeleteAsync(int id)
		{
			try
			{
				var seller = await FindByIdAsync(id);

				_context.Seller.Remove(seller);

				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				throw new IntegrityException("Cannot remove seller because there are related sales!");
			}
		}
	}
}
