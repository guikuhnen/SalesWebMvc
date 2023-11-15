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

		public async Task<ICollection<Seller>> FindAll()
		{
			return await _context.Seller.ToListAsync();
		}

		public async Task<ICollection<Seller>> FindAllWithDepartment()
		{
			return await _context.Seller
				.Include(x => x.Department)
				.ToListAsync();
		}

		public async Task<Seller> FindById(int id)
		{
			var seller = await _context.Seller
				.FirstOrDefaultAsync(x => x.Id == id);

			return seller ?? throw new NotFoundException("Unable to find Seller with provided ID!");
		}

		public async Task<Seller> FindByIdWithDepartment(int id)
		{
			var seller = await _context.Seller
				.Include(x => x.Department)
				.FirstOrDefaultAsync(y => y.Id == id);

			return seller ?? throw new NotFoundException("Unable to find Seller with provided ID!");
		}

		public async Task Add(Seller seller)
		{
			_context.Add(seller);

			await _context.SaveChangesAsync();
		}

		public async Task Update(Seller seller)
		{
			if (!SellerExists(seller.Id))
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

		private bool SellerExists(int id)
		{
			return _context.Seller.Any(x => x.Id == id);
		}

		public async Task Delete(int id)
		{
			var seller = await FindById(id);

			_context.Seller.Remove(seller);

			await _context.SaveChangesAsync();
		}
	}
}
