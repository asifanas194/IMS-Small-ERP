using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? CategoryId { get; set; }

    public int? VendorId { get; set; }

    public int? Quantity { get; set; }

    public decimal? CostPrice { get; set; }

    public decimal? SalePrice { get; set; }
}
