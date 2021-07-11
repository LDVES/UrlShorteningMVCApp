using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortenerMVCApp.Repositories;

namespace UrlShortenerMVCApp.Controllers
{
    public class RedirectController : Controller
    {
        private readonly IAddressesRepository _addressesRepository;

        public RedirectController(IAddressesRepository addressesRepository)
        {
            _addressesRepository = addressesRepository;
        }
        

        // GET: Redirect/RedirectToUrl/5
        public async Task<RedirectResult> RedirectToUrl(int id)
        {
            var result = await _addressesRepository.GetAddress(id);
            return Redirect(result.Url);
        }


    }
}
