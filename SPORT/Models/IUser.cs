using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Win32;
using SPORT.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace SPORT.Models
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterDTO registered, ModelStateDictionary modelstate);

            public Task<UserDTO> Authenticate(string UserName, string Password);
        public Task<UserDTO> GetUser(ClaimsPrincipal principal);

            } 
}

