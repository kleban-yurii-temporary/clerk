using Clerk.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Repositories
{
    public class DepartmentRepository
    {
        private readonly ClerkContext _ctx;
        public DepartmentRepository(ClerkContext ctx)
        {
            _ctx = ctx;
        }
        
        public Department Get(int id)
        {
            return _ctx.Departments.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _ctx.Departments.Include(x=> x.Positions).ToList();
        }
    }
}
