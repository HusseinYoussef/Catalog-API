using System;
using System.Collections.Generic;

namespace Catalog_API.Models
{
    public class AuthenticationResult
    { 
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}