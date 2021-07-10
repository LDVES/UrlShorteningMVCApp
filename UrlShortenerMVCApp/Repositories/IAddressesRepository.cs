using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortenerMVCApp.Models;

namespace UrlShortenerMVCApp.Repositories
{
    public interface IAddressesRepository
    {
        Task<List<Address>> GetAddresses(string id);
        Task<Address> GetAddress(int id);
        Task<Address> CreateAddress(Address addressToCreate);
        Task<Address> UpdateAddress(int id, Address addressToCreate);
        Task<Address> DeleteAddress(int id);

    }
}
