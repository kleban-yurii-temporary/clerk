using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Repositories.Models
{
    public class AnalyticInfo
    {
        public List<KeyValuePair<int, string>> FilterList { get; set; } = new List<KeyValuePair<int, string>>
        {
            new KeyValuePair<int, string>(0, "Весь 2021 рік")
        };
        public int? DepartmentsCount { get; set; }
        public int? EmployeesCount { get; set; }
        public KeyValuePair<int, string> AvgFilter { get; internal set; }
        public KeyValuePair<int, string> MaxFilter { get; internal set; }
        public decimal Avg { get; internal set; }     
        public decimal Max { get; internal set; }
        public string MaxPerson { get; internal set; }
    }
}
