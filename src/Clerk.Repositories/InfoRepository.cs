using Clerk.Core;
using Clerk.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Repositories
{
    public class InfoRepository
    {
        private readonly ClerkContext _ctx;
        public InfoRepository(ClerkContext ctx)
        {
            _ctx = ctx;
        }

        public AnalyticInfo GetInfo(int avgFilter, int maxFilter)
        {
            var info = new AnalyticInfo
            {
                DepartmentsCount = _ctx.Departments.Count(),
                EmployeesCount = _ctx.Positions.Count()
            };

            info.FilterList.AddRange(_ctx.MonthlyPayments
                .Select(x => x.Date)
                .Distinct()
                .OrderBy(x => x.Value)
                .Select(x => new KeyValuePair<int, string>(x.Value.Month, x.Value.ToString("MMMM", new CultureInfo("uk-UA")))));

            info.AvgFilter = info.FilterList.First(x => x.Key == avgFilter);
            info.MaxFilter = info.FilterList.First(x => x.Key == maxFilter);

            if (avgFilter > 0)
                info.Avg = Math.Round(_ctx.MonthlyPayments
                    .Where(x => x.Date.Value.Month == avgFilter)
                    .GroupBy(x => x.PersonOnPosition.Id)
                    .Select(x => new { x.Key, Sum = x.Sum(y => y.Amount) })
                    .Average(x => x.Sum), 0);
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    info.Avg += Math.Round(_ctx.MonthlyPayments
                        .Where(x => x.Date.Value.Month == i)
                        .GroupBy(x => x.PersonOnPosition.Id)
                        .Select(x => new { x.Key, Sum = x.Sum(y => y.Amount) })
                        .Average(x => x.Sum), 0);
                }

                info.Avg = Math.Round(info.Avg / 12, 0);
            }

            if (maxFilter > 0)
            {
                // var maxSalary = info.Avg = Math.Round(_ctx.MonthlyPayments.Where(x => x.Date.Value.Month == avgFilter)
                //   .GroupBy(x => x.PersonOnPosition.Id)
                //  .Select(x => new { x.Key, Sum = x.Sum(y => y.Amount) })

                //  .Max(x => x.Sum), 0); ;
            }
            else
            {

            }


            return info;

        }

    }
}
