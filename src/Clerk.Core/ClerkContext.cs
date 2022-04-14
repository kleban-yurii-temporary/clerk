using Clerk.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clerk.Core
{
    public class ClerkContext : IdentityDbContext<User>
    {
        public ClerkContext(DbContextOptions<ClerkContext> options)
            : base(options)
        {
        }
    }
}