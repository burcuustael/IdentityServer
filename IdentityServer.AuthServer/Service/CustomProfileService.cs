using IdentityServer.AuthServer.Repository;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IdentityServer.AuthServer.Service
{
    public class CustomProfileService : IProfileService
    {
        private readonly ICustomerUserRepository _customerUserRepository;

        public CustomProfileService(ICustomerUserRepository customerUserRepository)
        {
            _customerUserRepository = customerUserRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subId = context.Subject.GetSubjectId();

            var user = await _customerUserRepository.FindById(int.Parse(subId));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("name", user.UserName),
                new Claim("city",user.City)
            };

            if (user.Id==1)
            {
                claims.Add(new Claim("role", "admin"));
            }

            else
            {
                claims.Add(new Claim("role", "customer"));
            }

            context.AddRequestedClaims(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();

            var user = await _customerUserRepository.FindById(int.Parse(userId));

            context.IsActive = user != null ? true: false;
        }
    }
}
