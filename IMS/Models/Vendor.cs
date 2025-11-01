using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string? VendorName { get; set; }

    public string? ContactNo { get; set; }

    public string? Address { get; set; }
}
