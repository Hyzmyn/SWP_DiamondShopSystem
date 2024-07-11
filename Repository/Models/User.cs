using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Phone number must be numeric")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        [Range(1, 5)]
        public int RoleID { get; set; }
        public bool UserStatus { get; set; }

        [Range(6, 20)]
        public string? NiSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

