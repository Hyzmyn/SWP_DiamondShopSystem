using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class FeedBack
{
    public int FeedbackId { get; set; }

    public int? OrderDetailId { get; set; }

    public int? UserId { get; set; }

    public string? FeedbackText { get; set; }

    public DateTime Date { get; set; }

    public virtual OrderDetail? OrderDetail { get; set; }

    public virtual User? User { get; set; }
}
