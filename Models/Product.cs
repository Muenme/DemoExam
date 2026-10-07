using System;
using System.Collections.Generic;

namespace Demo.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public int? IdCategory { get; set; }

    public int? IdSubcategory { get; set; }

    public string Image { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? IdManufacture { get; set; }

    public string Description { get; set; } = null!;

    public string Structure { get; set; } = null!;

    public double Price { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual Manufacture? IdManufactureNavigation { get; set; }

    public virtual Subcategory? IdSubcategoryNavigation { get; set; }

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
