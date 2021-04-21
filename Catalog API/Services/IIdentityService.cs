using System;
using System.Threading.Tasks;
using Catalog_API.Dtos;
using Catalog_API.Models;

namespace Catalog_API.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}