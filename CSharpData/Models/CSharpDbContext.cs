using CSharpData.Models.UserTab;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpData.Models
{
    public class CSharpDbContext : IdentityDbContext
    {
        public DbSet<CreateUserTab> CreateUserTabs { get; set; }
        public CSharpDbContext(DbContextOptions<CSharpDbContext> opt) : base (opt)
        {

        }
    }
}
