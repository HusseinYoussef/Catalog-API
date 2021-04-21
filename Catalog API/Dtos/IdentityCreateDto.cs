using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog_API.Dtos
{
    public class IdentityCreateDto
    { 
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}