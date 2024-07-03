
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SPORT.Models;
using System.Security.Claims;

namespace SPORT.Models
{
    public class IdentityUserService:IUser
    {
        private readonly UserManager<ApplicationUser> _manager;

        public IdentityUserService( UserManager<ApplicationUser> manager)
        {
           
            _manager = manager;
            
        }
        public async Task<UserDTO> Register(RegisterDTO registered, ModelStateDictionary modelstate)
        {
            var user = new ApplicationUser()
            {
                UserName = registered.UserName,
                Email = registered.Email,
                PhoneNumber = registered.PhoneNumber
            };
            
            var result= await _manager.CreateAsync(user,registered.Password);

            if (result.Succeeded)
            {
              await  _manager.AddToRoleAsync(user, registered.Roles);
                return new UserDTO { Id = user.Id, UserName = registered.UserName, Role = await _manager.GetRolesAsync(user) };
            }
                foreach (var error in result.Errors)
                {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(registered.Password) :
                      error.Code.Contains("Email") ? nameof(registered.Email) :
                        error.Code.Contains("UserName") ? nameof(registered.UserName) : "";
                modelstate.AddModelError(errorKey, error.Description);

                };
            return null;

        }
        public async Task<UserDTO> Authenticate(string UserName, string Password)
        {
           var user = await _manager.FindByNameAsync(UserName);
            bool validPassword = await _manager.CheckPasswordAsync(user ,Password);
            if (validPassword)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = UserName,
                  Role=  await _manager.GetRolesAsync(user)
                };
                
            }
            return null;
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _manager.GetUserAsync(principal);
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName
             
            };
        }

    }
}
