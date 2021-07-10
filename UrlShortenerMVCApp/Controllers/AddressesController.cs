using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrlShortenerMVCApp.Data;
using UrlShortenerMVCApp.Models;
using UrlShortenerMVCApp.Repositories;
using UrlShortenerMVCApp.Services;

namespace UrlShortenerMVCApp.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAddressesRepository _addressesRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddressesService _userAddressesService;
        

        
        public AddressesController(ApplicationDbContext context, IAddressesRepository addressesRepository, UserManager<ApplicationUser> userManager, IAddressesService addressesService)
        {
            _context = context;
            _addressesRepository = addressesRepository;
            _userManager = userManager;
            _userAddressesService = addressesService;
            
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var result = _userAddressesService.GetAddresses(currentUserId);
            return View(await result);
        }

        
        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int id)
        {
           
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var result = await _userAddressesService.GetAddressDetails(currentUserId, id);
            if (result == null)
                return NotFound();
            return View(result);
            
        }
        
        // GET: Addresses/Create
        public IActionResult Create() => View();


        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url")] Address address)
        {


            if (ModelState.IsValid)
            {
                var currentUserId = _userManager.GetUserId(HttpContext.User);
                var result = await _userAddressesService.CreateAddress(currentUserId, address);
                return RedirectToAction(nameof(Index));
            }

            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var address = await _userAddressesService.GetAddressDetails(currentUserId, id);
            if (address == null)
                return NotFound();

            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(HttpContext.User);
            address.ApplicationUserId = currentUserId;

            if (ModelState.IsValid)
            {
                var result = await _userAddressesService.UpdateAddress(currentUserId, id, address);
                if (result == null)
                    return NotFound();
                return RedirectToAction(nameof(Index));
            }

            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var result = await _userAddressesService.GetAddressDetails(currentUserId, id);
            if (result == null)
                return NotFound();
            return View(result);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var result = await _userAddressesService.DeleteAddress(currentUserId, id);
            if (result == null)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
