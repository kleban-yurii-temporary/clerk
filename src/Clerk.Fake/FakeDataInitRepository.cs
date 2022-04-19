using Clerk.Core;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Clerk.Fake
{
    public class FakeDataInitRepository
    {
        private readonly ClerkContext _ctx;
        public FakeDataInitRepository(ClerkContext ctx)
        {
            _ctx = ctx;
        }

        private async Task cleanDataBase()
        {
            _ctx.MonthlyPayments.RemoveRange(_ctx.MonthlyPayments);
            await _ctx.SaveChangesAsync();

            _ctx.PersonsOnPositions.RemoveRange(_ctx.PersonsOnPositions);
            await _ctx.SaveChangesAsync();
            
            // Departments
            _ctx.Positions.RemoveRange(_ctx.Positions);
            await _ctx.SaveChangesAsync();
            _ctx.Departments.RemoveRange(_ctx.Departments);

            // Persons
            _ctx.Persons.RemoveRange(_ctx.Persons);
            await _ctx.SaveChangesAsync();

            // SalaryComponents
            _ctx.SalaryComponents.RemoveRange(_ctx.SalaryComponents);
            await _ctx.SaveChangesAsync();
        }

        private async Task initDataBase(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var r = new Random();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var ws = package.Workbook.Worksheets[1];

                // Persons
                for (int i = 1; i <= 50; i++)
                {
                    await _ctx.Persons.AddAsync(new Person
                    {
                        DateOfBirth = new DateTime(r.Next(1980, 2001), r.Next(1, 12), r.Next(1, 29)),
                        FirstName = ws.Cells[i, 1].Value.ToString(),
                        LastName = ws.Cells[i, 2].Value.ToString(),
                        SurName = ws.Cells[i, 3].Value.ToString()
                    });
                }
                await _ctx.SaveChangesAsync();

                ws = package.Workbook.Worksheets[0];

                // SalaryComponents
                for (int i = 3; i <= 12; i++)
                {
                    await _ctx.SalaryComponents.AddAsync(new SalaryComponent
                    {
                        IsActive = true,
                        Order = i - 2,
                        Title = ws.Cells[1, i].Value.ToString().Trim()
                    });
                }

                // Departments + Position
                for (int i = 5; i <= 25; i++)
                {
                    var depTitle = ws.Cells[i, 14].Value.ToString().Trim();
                    Department department = _ctx.Departments.FirstOrDefault(d => d.Title == depTitle);

                    if (department == null)
                    {
                        department = _ctx.Departments.Add(new Department
                        {
                            Title = depTitle
                        }).Entity;
                        await _ctx.SaveChangesAsync();
                    }

                    var posTitle = ws.Cells[i, 2].Value.ToString().Trim();
                    var position = _ctx.Positions.Add(new Position
                    {
                        Department = department,
                        Title = posTitle
                    }).Entity;
                    await _ctx.SaveChangesAsync();

                    _ctx.PersonsOnPositions.Add(new PersonOnPosition
                    {
                        Person = _ctx.Persons.First(x => x.Id == _ctx.Persons.Min(x => x.Id) + i),
                        Position = position,
                        StartDate = new DateTime(2021, 1, 1)
                    });
                }

                await _ctx.SaveChangesAsync();

                // Data
                for (int i = 3; i <= 12; i++)
                {
                    for (int j = 5; j <= 25; j++)
                    {
                        decimal value = ws.Cells[j, i].Value == null ? 0 : decimal.Parse(ws.Cells[j, i].Value.ToString().Trim());

                        var salaryComponentTitle = ws.Cells[1, i].Value.ToString().Trim();
                        var positionTitle = ws.Cells[j, 2].Value.ToString().Trim();

                        var payment = new MonthlyPayment
                        {
                            Amount = value,
                            SalaryComponent = _ctx.SalaryComponents.First(x => x.Title == salaryComponentTitle),
                            Date = new DateTime(2021, 1, 1),
                            PersonOnPosition = _ctx.PersonsOnPositions.Include(x => x.Position).First(x => x.Position.Title == positionTitle)
                        };
                        await _ctx.MonthlyPayments.AddAsync(payment);                        
                    } //14  
                }

                await _ctx.SaveChangesAsync();

                foreach (var pay in _ctx.MonthlyPayments.Include(x => x.PersonOnPosition).Include(x => x.SalaryComponent))
                {
                    for (int i = 1; i <= 11; i++)
                    {
                        var paySum = pay.Amount;
                        var p10 = (int)(paySum * 0.2m);

                        paySum = paySum + r.Next(-1*p10, p10);                        

                        await _ctx.MonthlyPayments.AddAsync(new MonthlyPayment()
                        {
                            Amount = paySum,
                            Date = pay.Date.Value.AddMonths(i),
                            PersonOnPosition = pay.PersonOnPosition,
                            SalaryComponent = pay.SalaryComponent
                        });                    
                    }
                }                
            }
        }

        public async Task StartInit(string filePath)
        {
            await cleanDataBase();
            await initDataBase(filePath);
        }

    }
}