using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<Seller> FindById(int id)
        {
            var seller = await _context.Seller
                .FirstOrDefaultAsync(m => m.Id == id);

            if (seller == null)
                throw new InvalidOperationException("Unable to find data for the given ID!");

            return seller;
        }

        public async Task Add(Seller seller)
        {
			// TODO - Temporary, allows to insert a seller
			seller.Department = _context.Department.First();
            _context.Add(seller);

            await _context.SaveChangesAsync();
        }

        public async Task Edit(Seller seller)
        {
            try
            {
                _context.Update(seller);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerExists(seller.Id))
                {
                    throw new InvalidOperationException("Unable to find data for the given ID!");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool SellerExists(int id)
        {
            return _context.Seller.Any(e => e.Id == id);
        }

        public async Task Delete(int id)
        {
            var seller = await FindById(id);

            _context.Seller.Remove(seller);

            await _context.SaveChangesAsync();
        }
    }
}
