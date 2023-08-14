using JWTEmployeeLoginPortal.DAL.Entities;
using JWTLogin.DAL.DBContexts;
using JWTLogin.DAL.Entities;
using JWTLogin.DAL.Repositories.Interfaces;
using JWTLogin.Models.RequestViewModels;
using Microsoft.EntityFrameworkCore;

namespace JWTLogin.DAL.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        
        public EmployeeRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }
        public async Task<Registration> RegistrationAsync(Registration registration)
        {
            await _context.AddAsync(registration);
            await _context.SaveChangesAsync();
            return registration;
        }
        public async Task<Registration?> LoginAsync(LoginRequestViewModel reg)
        {
            Registration response = await _context.registrations
                                .Where(r => r.Email == reg.Email)
                                .FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            return null;
        }
        public async Task<Secret?> GetSecretKey()
        {
            Secret response = await _context.secretKey
                                           .Where(r => r.Id == 1)
                                           .FirstOrDefaultAsync();
            if (response != null)
            {
                
                return response;
            }
            return null;
        }

        
        
    }
}
