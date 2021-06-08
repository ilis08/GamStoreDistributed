using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class AuthDbContext : IdentityDbContext<IdentityUser> 
    {
        public AuthDbContext() : base("AuthDbContext")
        {
           
        }
    }
}
