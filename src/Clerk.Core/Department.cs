using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Core
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Це поле не може бути пустим")]
        [MinLength(3, ErrorMessage = "Мінімальна довжина поля 3 символи")]
        [Display(Name = "Назва відділу")]
        public string? Title { get; set; }        
        public virtual ICollection<Position>? Positions { get; set; }        
    }
}
