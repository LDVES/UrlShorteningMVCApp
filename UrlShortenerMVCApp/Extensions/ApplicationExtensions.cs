using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortenerMVCApp.Data;
using UrlShortenerMVCApp.Repositories;
using UrlShortenerMVCApp.Services;

namespace UrlShortenerMVCApp.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DockerDbConnection")));

            services.AddScoped<IAddressesRepository, AddressesRepository>();
            services.AddScoped<IAddressesService, UserAddressesService>();

            return services;
        }
    }
}
