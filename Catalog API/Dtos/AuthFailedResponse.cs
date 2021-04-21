using System;
using System.Collections.Generic;

namespace Catalog_API.Dtos
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; } 
    }
}