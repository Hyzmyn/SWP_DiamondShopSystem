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
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int RoleID { get; set; }
        public bool UserStatus { get; set; }
        public string NiSize { get; set; }
        public DateTime CreatedAt { get; set; }

        //[ForeignKey("RoleID")]
        //public virtual Role Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

