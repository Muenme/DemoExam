using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Size
{
    public int IdSize { get; set; }

    public decimal Name { get; set; }

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
