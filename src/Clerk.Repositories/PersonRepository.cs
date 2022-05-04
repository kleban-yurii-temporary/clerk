using Clerk.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Repositories
{
    public class PersonRepository
    {
        private readonly ClerkContext _ctx;

        public PersonRepository(ClerkContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Person> GetAsync(int id)
        {
            return await _ctx.Persons.FirstAsync(x => x.Id == id);
        }

        public async Task<string> GetAvatarPathAsync(int id)
        {
            return (await GetAsync(id)).AvatarPath ?? "/uploads/person/no-avatar.png";
        }

        public async Task UpdateAvatarAsync(int id, string path)
        {
            var person = await GetAsync(id);
            person.AvatarPath = path;
            await _ctx.SaveChangesAsync();
        }
    }
}
