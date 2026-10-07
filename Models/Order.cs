using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public DateOnly Date { get; set; }

    public string? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
