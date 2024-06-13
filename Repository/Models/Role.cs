using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

<<<<<<< HEAD
        public virtual ICollection<User> Users { get; set; }
=======
        public virtual ICollection<User> User { get; set; }
>>>>>>> 275bb62d07d918df0bffe2a6de9523233ebe2f41
    }
}
