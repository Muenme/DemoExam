using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Stock
{
    public int IdStock { get; set; }

    public int? IdProduct { get; set; }

    public int? IdSize { get; set; }

    public int Count { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Size? IdSizeNavigation { get; set; }
}
