using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Core
{
    public class AttachedFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public AttachedMonthlyFiles? MonthlyFiles { get; set; }
        public string? Title { get; set; }
        public string? Path { get; set; }
        public string? Extension { get; set; }

    }
}
