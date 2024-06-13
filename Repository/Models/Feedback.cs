using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{

    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }
        public int OrderDetailID { get; set; }
        public int UserID { get; set; }
        public string FeedbackText { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("OrderDetailID")]
        public virtual OrderDetail OrderDetail { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
