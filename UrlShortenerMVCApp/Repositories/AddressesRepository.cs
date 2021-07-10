using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortenerMVCApp.Data;
using UrlShortenerMVCApp.Models;

namespace UrlShortenerMVCApp.Repositories
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAddresses()
        {
            return await _context.Addresses.Include( a => a.User).ToListAsync();
            //return await _context.Addresses.Where(a => a.ApplicationUserId == id).Include( a => a.User).ToListAsync();
            
        }
        public async Task<Address> GetAddress(int id)
        {
            
            return await _context.Addresses
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        
        public async Task<Address> CreateAddress(Address addressToCreate)
        {
            _context.Add(addressToCreate);
            await _context.SaveChangesAsync();
            return addressToCreate;
        }
        public async Task<Address> UpdateAddress(int id, Address addressToUpdate)
        {
            _context.Update(addressToUpdate);
            await _context.SaveChangesAsync();
            return addressToUpdate;
        }

        public async Task<Address> DeleteAddress(int id)
        {
            Address addressToDelete = _context.Addresses.Find(id);
            
            _context.Addresses.Remove(addressToDelete);
            await _context.SaveChangesAsync();
            return addressToDelete;
            
            
        }

        

        
    }
}
