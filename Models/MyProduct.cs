using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Demo.Models
{
    public partial class Product//добавление вычисляемых полей
    {
        [NotMapped]
        public int total { get; set; }
        [NotMapped]
        public Avalonia.Media.IBrush brush { get; set; }
        [NotMapped]
        public double? priceSale { get; set; }

    }
}
