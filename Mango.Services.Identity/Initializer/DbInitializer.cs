using IdentityModel;
using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(SD.Admin).Result != null) return;
            
            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "serjola",
                Email = "teste@teste.com",
                EmailConfirmed = true,
                PhoneNumber = "999999999",
                FirstName = "Ben",
                LastName = "Ten"
            };

            _userManager.CreateAsync(adminUser, "Jaspion.123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

            var userAdmin = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin)
            }).Result;
            
            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "luizola",
                Email = "teste2@teste2.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111",
                FirstName = "jorgen",
                LastName = "jor"
            };

            _userManager.CreateAsync(customerUser, "He-Man.25").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

            var userCustomer = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{customerUser.FirstName} {customerUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Customer)
            }).Result;
        }
    }
}
