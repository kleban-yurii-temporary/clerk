using Clerk.Core;
using Clerk.Repositories.Models;
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

        public IEnumerable<DepartmentInfo> GetStructure()
        {
            var list = new List<DepartmentInfo>();

            var departments = _ctx.Departments.Include(x => x.Positions).ToList();

            foreach(var d in departments)
            {
                var di = new DepartmentInfo
                {
                    Id = d.Id,
                    Title = d.Title,
                    PostionAndPerson = new List<KeyValuePair<string, string>>()
                };

                 foreach (var p in d.Positions)
                  {
                    var px = _ctx.PersonsOnPositions.Include(x=> x.Person).Include(x=> x.Position).First(x => x.Position.Id == p.Id);

                    di.PostionAndPerson.Add(new KeyValuePair<string, string>(px.Position.Title, 
                        $"{px.Person.LastName} {px.Person.FirstName[0]}. {px.Person.SurName[0]}."));
                }

                list.Add(di);
            }

            return list;
        }

        public async Task<DepartmentInfo> GetInfoAsync(int id)
        {
            return GetStructure().FirstOrDefault(x=> x.Id == id);
        }

        public async Task EditAsync(DepartmentInfo model)
        {
            _ctx.Departments.First(x => x.Id == model.Id).Title = model.Title;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _ctx.Departments.Remove(await _ctx.Departments.FirstAsync(x => x.Id == id));
            await _ctx.SaveChangesAsync();
        }

        public async Task<int> PositionsCountAsync(int id)
        {
            return (await _ctx.Departments.Include(x=> x.Positions).FirstAsync(x=> x.Id == id)).Positions.Count();
        }     

        public async Task CreateAsync(Department model)
        {
            await _ctx.Departments.AddAsync(model);
            await _ctx.SaveChangesAsync();
        }
    }
}
