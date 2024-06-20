using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
<<<<<<< HEAD
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
=======
	public class Role
	{
		//[Key]
		//public int RoleID { get; set; }
		//public string RoleName { get; set; }

		//public virtual ICollection<User> User { get; set; }
	}
>>>>>>> c91c109d5753a80fdb3cf7a2134a38ae363fcadf
}
