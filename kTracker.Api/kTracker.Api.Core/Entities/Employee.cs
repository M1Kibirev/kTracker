using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kTracker.Api.Core.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Surname { get; set; } = string.Empty;

        [Required]
        public string Patronymic { get; set; } = string.Empty;

        [Required]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public DateTimeOffset? UpdateTime { get; set; }

        public string? RefreshToken { get; set; } = null!;

        public User User { get; set; } = null!;

        public ICollection<Shift> Shifts { get; set; } = [];

        public string GetFullName() => $"{Surname} {Name} {Patronymic}";
    }
}
