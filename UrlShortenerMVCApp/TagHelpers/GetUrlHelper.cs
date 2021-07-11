using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortenerMVCApp.Models;

namespace UrlShortenerMVCApp.TagHelpers
{
    public class GetUrlHelper: Address
    {
        public string GeneratedUrl { get; set; }

        public GetUrlHelper(Address address, string baseUrl)
        {
            Id = address.Id;
            Name = address.Name;
            Url = address.Url;
            GeneratedUrl = "https://"+baseUrl +"/Addresses/Redirect/"+Id;
        }
    }
}
