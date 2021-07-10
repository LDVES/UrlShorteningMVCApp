using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortenerMVCApp.Models
{
    public class ApplicationUser: IdentityUser
    {
        public List<Address> Addresses { get; set; }
    }
}
