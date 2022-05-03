using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Core
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SurName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? AvatarPath { get; set; } = "/user/noavatar";
        public Gender? Gender { get; set; } = Core.Gender.NotDefined;
        public virtual ICollection<PersonOnPosition>? OnPositions { get; set; }
    }

    public enum Gender
    {
        NotDefined = 0,
        Male = 1,
        Female = 2
    }
}
