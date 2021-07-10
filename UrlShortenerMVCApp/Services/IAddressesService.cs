
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortenerMVCApp.Models;

namespace UrlShortenerMVCApp.Services
{
    public interface IAddressesService
    {
        Task<List<Address>> GetAddresses(string userId);
        Task<Address> GetAddressDetails(string userId, int addressId);
        Task<Address> CreateAddress(string userId, Address address);
        Task<Address> UpdateAddress(string userId, int addressId, Address address);

        Task<Address> DeleteAddress(string userId, int addressId);
    }
}
