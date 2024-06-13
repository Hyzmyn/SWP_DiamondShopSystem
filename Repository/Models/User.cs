using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.Data;
=======
>>>>>>> 275bb62d07d918df0bffe2a6de9523233ebe2f41
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

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
<<<<<<< HEAD
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
=======
    }
}
>>>>>>> 275bb62d07d918df0bffe2a6de9523233ebe2f41
