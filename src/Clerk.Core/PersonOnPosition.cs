using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Core
{
    public class PersonOnPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Person? Person { get; set; } 
        public Position? Position { get; set; }
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
