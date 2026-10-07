using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class ProductOrder
{
    public int IdProduct { get; set; }

    public int IdOrder { get; set; }

    public int IdSize { get; set; }

    public int Count { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Size IdSizeNavigation { get; set; } = null!;
}
