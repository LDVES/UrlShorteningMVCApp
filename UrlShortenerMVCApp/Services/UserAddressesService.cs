
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortenerMVCApp.Models;
using UrlShortenerMVCApp.Repositories;

namespace UrlShortenerMVCApp.Services
{
    public class UserAddressesService : IAddressesService
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAddressesService(IAddressesRepository addressesRepository, UserManager<ApplicationUser> userManager)
        {

            _addressesRepository = addressesRepository;
            _userManager = userManager;


        }



        public async Task<List<Address>> GetAddresses(string userId)
        {

            var userAddresses = await _addressesRepository.GetAddresses();
            userAddresses = userAddresses.Where(a => a.ApplicationUserId == userId).ToList();
            return userAddresses;
        }



        public async Task<Address> GetAddressDetails(string userId, int addressId)
        {
            var address = await _addressesRepository.GetAddress(addressId);
            if (address != null && address.ApplicationUserId == userId)
                return address;
            else
                return null;

        }

        public async Task<Address> CreateAddress(string userId, Address address)
        {
            address.ApplicationUserId = userId;
            var result = await _addressesRepository.CreateAddress(address);
            return result;
        }

        public async Task<Address> UpdateAddress(string userId, int addressId, Address address)
        {
            if (addressId != address.Id || userId != address.ApplicationUserId)
                return null;
            return await _addressesRepository.UpdateAddress(addressId, address);

        }

        public async Task<Address> DeleteAddress(string userId, int addressId)
        {
            var address = await _addressesRepository.GetAddress(addressId);
            if (address.ApplicationUserId != userId)
                return null;
            var result = await _addressesRepository.DeleteAddress(addressId);
            return result;
        }
    }
}
