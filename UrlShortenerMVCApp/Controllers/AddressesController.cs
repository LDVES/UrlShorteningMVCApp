using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrlShortenerMVCApp.Data;
using UrlShortenerMVCApp.Models;
using UrlShortenerMVCApp.Repositories;


namespace UrlShortenerMVCApp.Controllers
{
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAddressesRepository _addressesRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddressesController(ApplicationDbContext context, IAddressesRepository addressesRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _addressesRepository = addressesRepository;
            _userManager = userManager;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            string currentUserId = _userManager.GetUserId(HttpContext.User);
            return View(await _addressesRepository.GetAddresses(currentUserId));
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int id)
        {

            Address address = await _addressesRepository.GetAddress(id);

            if (address == null)
                return NotFound();

            return View(address);
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
                address.ApplicationUserId = _userManager.GetUserId(HttpContext.User);
                await _addressesRepository.CreateAddress(address);
                return RedirectToAction(nameof(Index));
            }

            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Address address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

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

            address.ApplicationUserId = _userManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid)
            {
                try
                {
                    await _addressesRepository.UpdateAddress(id, address);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Address address = await _addressesRepository.GetAddress(id);
            if (address == null)
                return NotFound();
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _addressesRepository.DeleteAddress(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
