using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Repositories.Models
{
    public class DepartmentInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<KeyValuePair<string, string>>? PostionAndPerson { get; set; }
    }
}
