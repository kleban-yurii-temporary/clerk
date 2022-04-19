using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Core
{
    public class MonthlyPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? Date { get; set; }        
        public decimal Amount { get; set; }
        public PersonOnPosition? PersonOnPosition { get; set; }
        public SalaryComponent? SalaryComponent { get; set; }
    }
}
